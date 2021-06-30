using Project_Netflix.viewmodel;
using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Project_Netflix.Command
{
	public class CmdDangKy : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		DangKyViewModel vm;
		public bool CanExecute(object parameter)
		{
			//return vm.allowButton();
			return true;
		}
		public void Execute(object parameter)
		{
			using (var db = new NETFLIX_DBEntities())
			{
				var user = new USER_INFORMATION()
				{
					NAME = vm.Name,
					PHONE = vm.Phone,
					ADDRESS = "",
				};
				db.USER_INFORMATION.Add(user);
				var account = new ACCOUNT()
				{
					EMAIL = vm.Email,
					PASSWORD = vm.HashPassword(vm.Password),
					TYPE = 1,
					INFORMATION = user.ID,
				};
				db.ACCOUNTs.Add(account);
				db.SaveChanges();
				var p = (Window)parameter;
				MessageBox.Show("Dang ky thanh cong");
				p.Close();
			}
		}
		public CmdDangKy(DangKyViewModel vm)
		{
			this.vm = vm;
		}
	}
}
