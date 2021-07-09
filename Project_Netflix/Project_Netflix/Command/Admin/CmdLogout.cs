using Project_Netflix.viewmodel.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.Command.Admin
{
	class CmdLogout : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminManagementViewModel vm;
		public bool CanExecute(object parameter)
		{
			//if(parameter != null)
			return true;
			//else
			//	return false;
		}
		public void Execute(object parameter)
		{
			var window = (Window)parameter;
			vm.Logout(window);
		}
		public CmdLogout(AdminManagementViewModel vm)
		{
			this.vm = vm;
		}
	}
}
