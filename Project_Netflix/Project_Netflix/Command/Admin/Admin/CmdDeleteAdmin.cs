using Project_Netflix.viewmodel.Admin.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Netflix.Command.Admin.Admin
{
	class CmdDeleteAdmin : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminAccount vm;
		public bool CanExecute(object parameter)
		{
			//if(parameter != null)
			return true;
			//else
			//	return false;
		}
		public void Execute(object parameter)
		{
			vm.DeleteAdmin();
		}
		public CmdDeleteAdmin(AdminAccount vm)
		{
			this.vm = vm;
		}
	}
}
