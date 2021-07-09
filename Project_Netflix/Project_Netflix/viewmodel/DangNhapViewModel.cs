using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Project_Netflix.Command;
using Project_Netflix.Command.MainWindow;
using Project_Netflix.model;
using Project_Netflix.View;

namespace Project_Netflix.viewmodel
{
    public class DangNhapViewModel : DependencyObject
    {
        DangKyViewModel dk = new DangKyViewModel();
        public static readonly DependencyProperty UserNameProperty;
        public static readonly DependencyProperty PasswordProperty;
        public ICommand CmdDangNhap { get; set; }
        public ICommand CmdPasswordChange { get; set; }
        public static bool IsLogin { get; set; }
        public static ACCOUNT User { get; set; }
        static DangNhapViewModel()
        {
            UserNameProperty = DependencyProperty.Register("UserName", typeof(string), typeof(DangNhapViewModel));
            PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(DangNhapViewModel));
        }

        public string UserName { get => (string)GetValue(UserNameProperty); set => SetValue(UserNameProperty, value); }
        public string Password { get => (string)GetValue(PasswordProperty); set => SetValue(PasswordProperty, value); }
        public DangNhapViewModel()
        {
            CmdDangNhap = new CmdDangNhap(this);
            CmdPasswordChange = new CmdPasswordChange(this);
            IsLogin = false;
            UserName = "";
            Password = "";
        }
        public void Login(Window window)
        {
            if (window == null) return;
            using (var db = new NETFLIX_DBEntities())
            {
				if (db.ACCOUNTs.Where(x => x.EMAIL == "admin@gmail.com").Count() == 0)
				{
                    var user = new USER_INFORMATION()
                    {
                        NAME = "ADMIN",
                    };
                    db.USER_INFORMATION.Add(user);
                    var account = new ACCOUNT()
                    {
                        EMAIL = "admin@gmail.com",
                        PASSWORD = dk.HashPassword("123456"),
                        TYPE = 2,
                        INFORMATION = user.ID,
                    };
                    db.ACCOUNTs.Add(account);
                    db.SaveChanges();
                }
                var getPassword = db.ACCOUNTs.Where(x => x.EMAIL.Trim() == UserName.Trim());
                if (getPassword.Count() != 0)
                {
                    if (checkPassword(Password.Trim(), getPassword.Single().PASSWORD.Trim()))
                    {
                        if (getPassword.Single().TYPE == 2)
                        {
                            MessageBox.Show("Dang nhap thanh cong");
                            Admin_Management admin = new Admin_Management();
                            window.Close();
                            admin.ShowDialog();
                        }
                        else if (getPassword.Single().TYPE == 1)
                        {
                            IsLogin = true;
                            MessageBox.Show("Dang nhap thanh cong");
                            User = getPassword.Single();
                            var puch = User.PURCHASEs.ToList().Last();
                            if (puch.OUTOFDATE < DateTime.Now)
                                User.ACTIVE = 0;
                            if (User.ACTIVE == 1)
                            {
                                MainWindow main = new MainWindow();
                                window.Close();
                                main.ShowDialog();
                            }
							else
							{
                                PagePay page = new PagePay();
                                window.Close();
                                page.ShowDialog();
							}
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email khong ton tai hoac mat khau sai.");
                    }
                }
                else
                {
                    MessageBox.Show("Email khong ton tai hoac mat khau sai.");
                }
            }
        }
        bool checkPassword(string password, string PW)
        {
            var strPw = password;
            var sha = new SHA1CryptoServiceProvider();
            var arrBytePw = ASCIIEncoding.ASCII.GetBytes(strPw);
            //giá trị hash lenght ứng với giải thuật hash đã chọn
            var hashLen = 20;
            // sử dụng để chuyển đổi giá trị HEX đã lưu
            List<char> lHex = new List<char> { 'e', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
            // lấy giá trị salt đã lưu
            var arrChar = PW.ToCharArray();
            var arrBytepw = ASCIIEncoding.ASCII.GetBytes(strPw);
            var arrByte = new byte[PW.Length / 2 - hashLen];
            for (int i = hashLen * 2, j = 0; i < arrChar.Length; i += 2)
            {
                arrByte[j++] = (byte)(lHex.IndexOf(arrChar[i]) * 16 + lHex.IndexOf(arrChar[i + 1]));
            }
            // ghép password với salt 
            var arrPwSalt = new byte[arrBytepw.Length + arrByte.Length];
            Array.Copy(arrBytePw, arrPwSalt, arrBytepw.Length);
            Array.Copy(arrByte, 0, arrPwSalt, arrBytePw.Length, arrByte.Length);
            // thực hiện hash 
            var arrPwHashed = sha.ComputeHash(arrPwSalt);
            var arrPwSaltHashed = new byte[arrPwHashed.Length + arrByte.Length];
            Array.Copy(arrPwHashed, arrPwSaltHashed, arrPwHashed.Length);
            Array.Copy(arrByte, 0, arrPwSaltHashed, arrPwHashed.Length, arrByte.Length);
            var strPwHashed = BitConverter.ToString(arrPwSaltHashed).Replace("-", "");
            // So sánh kết quả 
            if (strPwHashed == PW)
                return true;
            else
                return false;
        }
        public bool allowButton()
        {
            Regex emailCheck = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = emailCheck.Match(UserName);
            bool isEmail = (match.Success) ? true : false;
            if (match.Success && Password.Length > 0) return true;
            else return false;
        }
    }
}
