using Project_Netflix.model;
using Project_Netflix.viewmodel.Admin.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.Command.Admin.User
{
	class CmdDeleteUser : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminAccount vm;
		public bool CanExecute(object parameter)
		{
			//var user = (ACCOUNT)parameter;
			//if(user != null)
			return true;
			//return false;
		}

		public void Execute(object parameter)
		{
			vm.DeleteUser();
			//using (var db = new NETFLIX_DBEntities1())
			//{
			//	//var user = db.ACCOUNTs.Where(x => x.TYPE == 1).ToList();
			//	//foreach(var item in user)
			//	//{
			//	//	var account = new USER_INFORMATION() { ID = (int)item.INFORMATION };
			//	//	db.USER_INFORMATION.Remove(account);
			//	//	db.ACCOUNTs.Remove(item);
			//	//	db.SaveChanges();
			//	//}
			//	//var id = db.ACCOUNTs.Where(x => x.ID == idacc).Single().INFORMATION;
			//	var account = new ACCOUNT() { ID = db.ACCOUNTs.Where(x=>x.EMAIL == Email)}
			//	db.SaveChanges();


			//	MessageBox.Show("Tinh chat qua quan trong chua dam xoa");

			//}
		}
		public CmdDeleteUser(AdminAccount vm)
		{
			this.vm = vm;
		}
	}
}
