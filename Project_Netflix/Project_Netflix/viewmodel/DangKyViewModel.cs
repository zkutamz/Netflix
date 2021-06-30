using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Project_Netflix.Command;
using Project_Netflix.Command.MainWindow;

namespace Project_Netflix.viewmodel
{
	public class DangKyViewModel : DependencyObject
	{
		public static readonly DependencyProperty EmailProperty;
		public static readonly DependencyProperty PasswordProperty;
		public static readonly DependencyProperty NameProperty;
		public static readonly DependencyProperty PhoneProperty;
		static DangKyViewModel()
		{
			EmailProperty = DependencyProperty.Register("Email", typeof(string), typeof(DangKyViewModel));
			PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(DangKyViewModel));
			NameProperty = DependencyProperty.Register("Name", typeof(string), typeof(DangKyViewModel));
			PhoneProperty = DependencyProperty.Register("Phone", typeof(string), typeof(DangKyViewModel));
		}
		public string Email { get => (string)GetValue(EmailProperty); set => SetValue(EmailProperty, value); }
		public string Password { get => (string)GetValue(PasswordProperty); set => SetValue(PasswordProperty, value); }
		public string Name { get => (string)GetValue(NameProperty); set => SetValue(NameProperty, value); }
		public string Phone { get => (string)GetValue(PhoneProperty); set => SetValue(PhoneProperty, value); }
		public ICommand CmdPasswordChange { get; set; }
		public ICommand CmdDangKy { get; set; }
		public DangKyViewModel()
		{
			Email = "";
			Password = "";
			Name = "";
			Phone = "";
			CmdPasswordChange = new CmdPasswordChangeDK(this);
			CmdDangKy = new CmdDangKy(this);
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
		public bool allowButton()
		{
			Regex phoneCheck = new Regex(@"^(0 ?)(3[2 - 9] | 5[6 | 8 | 9] | 7[0 | 6 - 9] | 8[0 - 6 | 8 | 9] | 9[0 - 4 | 6 - 9])[0 - 9]{ 7}$");
			Match match = phoneCheck.Match(Phone.Trim());
			if (match.Success && Name.Length > 0) return true;
			return false;
		}
	}
}
