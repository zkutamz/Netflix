using Project_Netflix.Command.MainWindow;
using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Project_Netflix.View;

namespace Project_Netflix.viewmodel
{
	public class UserProfileViewModel : BaseViewModel
	{
		public ICommand CmdHuy { get; set; }
		public ICommand CmdUpdateUser { get; set; }
		private ACCOUNT _User;
		public ACCOUNT User { get => _User; set { _User = value; OnPropertyChanged();
				if (User != null) { Name = User.USER_INFORMATION.NAME; Phone = User.USER_INFORMATION.PHONE;DOB = (DateTime)User.USER_INFORMATION.DATEOFBIRTH; }
			} }
		private string _Name;
		public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
		private string _Phone;
		public string Phone { get => _Phone; set { _Phone = value; OnPropertyChanged(); } }
		private DateTime _DOB;
		public DateTime DOB { get => _DOB; set { _DOB = value; OnPropertyChanged(); } }
		public UserProfileViewModel() {
			if (DangNhapViewModel.IsLogin)
				User = DangNhapViewModel.User;
			CmdHuy = new CmdHuy(this);
			CmdUpdateUser = new CmdUpdateUser(this);
		}
		public void UpdateUser()
		{
			using(var db = new NETFLIX_DBEntities())
			{
				try
				{
					var user = db.ACCOUNTs.Where(x => x.ID == User.ID).Single();
					user.USER_INFORMATION.NAME = Name.Trim();
					user.USER_INFORMATION.PHONE = Phone.Trim();
					user.USER_INFORMATION.DATEOFBIRTH = DOB;
					db.SaveChanges();
					User.USER_INFORMATION.NAME = Name.Trim();
					User.USER_INFORMATION.PHONE = Phone.Trim();
					User.USER_INFORMATION.DATEOFBIRTH = DOB;
					DangNhapViewModel.User = user;
					MainWindow.vm.LoadUser();
					
				}
				catch(Exception e)
				{
					Console.WriteLine(e.Message);
					MessageBox.Show("Cap nhat that bai.");
				}
			
			}
		}
		public bool checkUpdate()
		{
			return true;
		}
	}
}
