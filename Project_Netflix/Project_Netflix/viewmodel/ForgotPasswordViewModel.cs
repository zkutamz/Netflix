using Project_Netflix.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Project_Netflix.model;
using Project_Netflix.Command.MainWindow;

namespace Project_Netflix.viewmodel
{
	public class ForgotPasswordViewModel : DependencyObject
	{
		public static readonly DependencyProperty EmailProperty;
		public static readonly DependencyProperty PasswordProperty;
		static ForgotPasswordViewModel()
		{
			EmailProperty = DependencyProperty.Register("EmailForGot", typeof(string), typeof(DangKyViewModel));
			PasswordProperty = DependencyProperty.Register("PasswordForgot", typeof(string), typeof(DangKyViewModel));
		}
		public string EmailForGot { get => (string)GetValue(EmailProperty); set => SetValue(EmailProperty, value); }
		public string PasswordForgot { get => (string)GetValue(PasswordProperty); set => SetValue(PasswordProperty, value); }
		public ICommand CmdPasswordChange { get; set; }
		public ICommand CmdForgot { get; set; }
		public ForgotPasswordViewModel()
		{
			EmailForGot = "";
			PasswordForgot = "";
			CmdPasswordChange = new CmdPasswordChangeFP(this);
			CmdForgot = new CmdForgotPassword(this);
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
		public bool checkInput()
		{
			Regex emailCheck = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
			Match match = emailCheck.Match(EmailForGot);
			bool isEmail = (match.Success) ? true : false;
			if (EmailForGot.Length == 0)
				return false;
			else if (!isEmail)
			{
				return false;
			}
			else
			{
				using (var db = new NETFLIX_DBEntities())
				{
					if (db.ACCOUNTs.Where(x => x.EMAIL.Trim() == EmailForGot.Trim()).Count() == 0)
					{
						MessageBox.Show("Email khong ton tai");
						return false;
					}
					else { return true; }
				}
			}
		}
		public bool checkePassword()
		{
			if (PasswordForgot.Length >= 8) return true;
			return false;
		}
	}
}

