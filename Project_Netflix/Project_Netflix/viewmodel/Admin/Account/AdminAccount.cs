using Project_Netflix.Command.Admin.Admin;
using Project_Netflix.Command.Admin.User;
using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.viewmodel.Admin.Account
{
	public class AdminAccount : BaseViewModel
	{
		private ObservableCollection<ACCOUNT> _DSAccount;
		public ObservableCollection<ACCOUNT> DSAccount { get => _DSAccount; set { _DSAccount = value; OnPropertyChanged(); } }
		public ICommand CmdAddUser { get; set; }
		public ICommand CmdUpdateUser { get; set; }
		public ICommand CmdDeleteUser { get; set; }
		public ICommand CmdAddAdmin { get; set; }
		public ICommand CmdUpdateAdmin { get; set; }
		public ICommand CmdDeleteAdmin { get; set; }
		private ObservableCollection<ACCOUNT> _DSAdmin;
		public ObservableCollection<ACCOUNT> DSAdmin { get => _DSAdmin; set { _DSAdmin = value; OnPropertyChanged(); } }
		//public ObservableCollection<CATEGORY> DSAdmin { get => _DSAdmin; set { _DSAdmin = value; OnPropertyChanged(); } }
		private ACCOUNT _SelectedAccount;
		public ACCOUNT SelectedAccount
		{
			get => _SelectedAccount;
			set
			{
				_SelectedAccount = value;
				OnPropertyChanged();
				if (SelectedAccount != null) { Email = SelectedAccount.EMAIL; Name = SelectedAccount.USER_INFORMATION.NAME; Phone = SelectedAccount.USER_INFORMATION.PHONE; Address = SelectedAccount.USER_INFORMATION.ADDRESS; if (SelectedAccount.USER_INFORMATION.DATEOFBIRTH == null) Birth = DateTime.Parse("01/01/0001"); else Birth = (DateTime)SelectedAccount.USER_INFORMATION.DATEOFBIRTH; }
			}
		}
		private string _Email;
		public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
		private string _Name;
		public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
		private double _Balance;
		public double Balance { get => _Balance; set { _Balance = value; OnPropertyChanged(); } }
		private string _Phone;
		public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
		private DateTime _Birth;
		public DateTime Birth { get => _Birth; set { _Birth = value; OnPropertyChanged(); } }
		private string _Address;
		public string Address { get => _Address; set { _Address = value; OnPropertyChanged(); } }
		private ACCOUNT _SelectedAdmin;
		public ACCOUNT SelectedAdmin
		{
			get => _SelectedAdmin;
			set
			{
				_SelectedAdmin = value;
				OnPropertyChanged();
				if (SelectedAdmin != null) { EmailAdmin = SelectedAdmin.EMAIL; NameAdmin = SelectedAdmin.USER_INFORMATION.NAME; }
			}
		}
		private string _EmailAdmin;
		public string EmailAdmin { get => _EmailAdmin; set { _EmailAdmin = value; OnPropertyChanged(); } }
		private string _NameAdmin;
		public string NameAdmin { get => _NameAdmin; set { _NameAdmin = value; OnPropertyChanged(); } }
		public AdminAccount()
		{
			CmdAddUser = new CmdAddUser(this);
			CmdUpdateUser = new CmdUpdateUser(this);
			CmdDeleteUser = new CmdDeleteUser(this);
			CmdAddAdmin = new CmdAddAdmin(this);
			CmdUpdateAdmin = new CmdUpdateAdmin(this);
			CmdDeleteAdmin = new CmdDeleteAdmin(this);
			using (var db = new NETFLIX_DBEntities())
			{
				loadDBUser();
				loadDBAdmin();
			}
		}
		void loadDBUser()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				DSAccount = new ObservableCollection<ACCOUNT>(db.ACCOUNTs.Include("USER_INFORMATION").Where(x => x.TYPE == 1).ToList());
			}
		}
		void loadDBAdmin()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				DSAdmin = new ObservableCollection<ACCOUNT>(db.ACCOUNTs.Include("USER_INFORMATION").Where(x => x.TYPE == 2).ToList());
				//DSAdmin = new ObservableCollection<CATEGORY>(db.CATEGORies.ToList());
			}
		}
		public string HashPassword(string Password)
		{
			string pass = Password;
			//lấy password bản rõ 
			//sử dụng hỗ trợ hash SHA1 của .NET 
			var sha = new SHA1CryptoServiceProvider();
			//| vì các thuật toán hash có sẵn làm việc với mảng byte II nên cần chuyển sang mảng byte để xử lý
			//| sử dụng phương thức chuyển chuỗi sang mảng byte // có sẵn trong các lớp Encoding
			var arrBytePw = ASCIIEncoding.ASCII.GetBytes(pass);
			//| sử dụng thời gian hiện tại làm đại lượng salt | (để có được xác suất khác nhau cao)
			var strTimeNow = DateTime.Now.Millisecond.ToString();
			//| ghép salt vào với password
			var arrTimeNow = ASCIIEncoding.ASCII.GetBytes(strTimeNow);
			var arrPwSalt = new byte[arrBytePw.Length + arrTimeNow.Length];
			Array.Copy(arrBytePw, arrPwSalt, arrBytePw.Length);
			Array.Copy(arrTimeNow, 0, arrPwSalt, arrBytePw.Length, arrTimeNow.Length);
			//| thực hiện hash
			var arrPwHashed = sha.ComputeHash(arrPwSalt);
			//| ghép salt vào kết quả để lưu trữ salt xuống database
			var arrPwSaltHashed = new byte[arrPwHashed.Length + arrTimeNow.Length];
			Array.Copy(arrPwHashed, arrPwSaltHashed, arrPwHashed.Length); Array.Copy(arrTimeNow, 0, arrPwSaltHashed, arrPwHashed.Length, arrTimeNow.Length);
			//| chuyển đổi mảng byte sang dạng chuỗi HEX đề ghi xuống database
			var strPwHashed = BitConverter.ToString(arrPwSaltHashed).Replace("-", "");
			return strPwHashed;
		}
		public void CreateUser()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.ACCOUNTs.Where(x => x.EMAIL.Trim() == Email.Trim()).Count() == 0)
				{
					var user = new USER_INFORMATION()
					{
						NAME = Name.Trim(),
						PHONE = Phone.Trim(),
						ADDRESS = Address.Trim(),
						DATEOFBIRTH = Birth,
					};
					db.USER_INFORMATION.Add(user);
					var account = new ACCOUNT()
					{
						EMAIL = Email.Trim(),
						PASSWORD = HashPassword("123456"),
						TYPE = 1,
						INFORMATION = user.ID,
					};
					db.ACCOUNTs.Add(account);
					db.SaveChanges();
					MessageBox.Show("Dang ky thanh cong");
					loadDBUser();
				}
				else
				{
					MessageBox.Show("Email đã tồn tại.");
				}
			}
		}
		public void UpdateUser()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.ACCOUNTs.Where(x => x.EMAIL == Email).Count() == 1)
				{
					var id = db.ACCOUNTs.Where(x => x.EMAIL.Trim() == SelectedAccount.EMAIL.Trim()).Single().INFORMATION;
					var user = db.USER_INFORMATION.Where(x => x.ID == id).Single();
					user.NAME = Name.Trim();
					user.PHONE = Phone.Trim();
					user.ADDRESS = Address.Trim();
					user.DATEOFBIRTH = Birth;
					db.SaveChanges();
					MessageBox.Show("Sua thanh cong");
				}
				else
				{
					MessageBox.Show("Email khong ton tai");
				}

			}
			loadDBUser();
		}
		public void DeleteUser()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.ACCOUNTs.Where(x => x.EMAIL == Email).Count() == 1)
				{
					var account = new ACCOUNT() { ID = db.ACCOUNTs.Where(x => x.EMAIL == Email).Single().ID };
					var user = new USER_INFORMATION() { ID = (int)db.ACCOUNTs.Where(x => x.EMAIL == Email).Single().INFORMATION };
					db.USER_INFORMATION.Remove(user);
					db.ACCOUNTs.Remove(account);
					db.SaveChanges();
					MessageBox.Show("Xóa tài khoản thành công");
					loadDBUser();
				}
				else
				{
					MessageBox.Show("Tài khoản không tồn tại.");
				}
			}
		}
		public void CreateAdmin()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.ACCOUNTs.Where(x => x.EMAIL.Trim() == Email.Trim()).Count() == 0)
				{
					var user = new USER_INFORMATION()
					{
						NAME = NameAdmin.Trim(),
					};
					db.USER_INFORMATION.Add(user);
					var account = new ACCOUNT()
					{
						EMAIL = Email.Trim(),
						PASSWORD = HashPassword("123456"),
						TYPE = 2,
						INFORMATION = user.ID,
					};
					db.ACCOUNTs.Add(account);
					db.SaveChanges();
					MessageBox.Show("Dang ky thanh cong");
					loadDBAdmin();
				}
				else
				{
					MessageBox.Show("Email đã tồn tại.");
				}
			}
		}
		public void UpdateAdmin()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.ACCOUNTs.Where(x => x.EMAIL == Email).Count() == 1)
				{
					var id = db.ACCOUNTs.Where(x => x.EMAIL.Trim() == SelectedAccount.EMAIL.Trim()).Single().INFORMATION;
					var user = db.USER_INFORMATION.Where(x => x.ID == id).Single();
					user.NAME = NameAdmin.Trim();
					db.SaveChanges();
					MessageBox.Show("Sua thanh cong");
				}
				else
				{
					MessageBox.Show("Tài khoản không tồn tại.");
				}
			}
			loadDBAdmin();
		}
		public void DeleteAdmin()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				if (db.ACCOUNTs.Where(x => x.EMAIL == Email).Count() == 1)
				{
					var account = new ACCOUNT() { ID = db.ACCOUNTs.Where(x => x.EMAIL == Email).Single().ID };
					var user = new USER_INFORMATION() { ID = (int)db.ACCOUNTs.Where(x => x.EMAIL == Email).Single().INFORMATION };
					db.USER_INFORMATION.Remove(user);
					db.ACCOUNTs.Remove(account);
					db.SaveChanges();
					MessageBox.Show("Xóa tài khoản thành công");
					loadDBAdmin();
				}
				else
				{
					MessageBox.Show("Tài khoản không tồn tại.");
				}
			}
		}
	}
}

