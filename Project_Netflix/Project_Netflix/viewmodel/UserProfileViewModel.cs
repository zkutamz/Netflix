using Project_Netflix.Command.MainWindow;
using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
namespace Project_Netflix.viewmodel
{
	public class UserProfileViewModel : BaseViewModel
	{
		public ICommand CmdHuy { get; set; }
		public ICommand CmdUpdateUser { get; set; }
		private ACCOUNT _User;
		public ACCOUNT User { get => _User; set { _User = value; OnPropertyChanged(); } }
		public UserProfileViewModel() { 
			
			CmdHuy = new CmdHuy(this);
			CmdUpdateUser = new CmdUpdateUser(this);

		}
		public void UpdateUser()
		{
			using(var db = new NETFLIX_DBEntities())
			{
				var user = db.ACCOUNTs.Where(x => x.ID == User.ID).Single();
				user.USER_INFORMATION.NAME = User.USER_INFORMATION.NAME;
				user.USER_INFORMATION.PHONE = User.USER_INFORMATION.PHONE;
				user.USER_INFORMATION.DATEOFBIRTH = User.USER_INFORMATION.DATEOFBIRTH;
				db.SaveChanges();
			}
		}
		public bool checkUpdate()
		{
			return true;
		}
	}
}
