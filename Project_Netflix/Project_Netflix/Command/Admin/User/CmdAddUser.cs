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
	public class CmdAddUser : ICommand
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

			
			vm.CreateUser();
		}
		public CmdAddUser(AdminAccount vm)
		{
			this.vm = vm;
		}
	}
}
