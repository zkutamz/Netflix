using Project_Netflix.model;
using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.Command.MainWindow
{
	class CmdForgotPassword : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		ForgotPasswordViewModel vm;
		public bool CanExecute(object parameter)
		{
			return vm.checkePassword();
		}

		public void Execute(object parameter)
		{
			var windon = (Window)parameter;
			if (vm.Password.Length > 0)
			{
				using (var db = new NETFLIX_DBEntities())
				{
					db.ACCOUNTs.Single(x => x.EMAIL == vm.Email).PASSWORD = vm.HashPassword(vm.Password);
					db.SaveChanges();
					var p = (Window)parameter;
					MessageBox.Show("Đổi mật khẩu thành công");
					windon.Close();
				}
			}
		}
		public CmdForgotPassword(ForgotPasswordViewModel vm)
		{
			this.vm = vm;
		}
	}
}
