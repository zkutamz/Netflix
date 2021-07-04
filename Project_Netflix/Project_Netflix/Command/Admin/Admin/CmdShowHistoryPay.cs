using Project_Netflix.model;
using Project_Netflix.View.Admin;
using Project_Netflix.viewmodel.Admin.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace Project_Netflix.Command.Admin.Admin
{
	class CmdShowHistoryPay : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminAccount vm;
		public bool CanExecute(object parameter)
		{
			var user = (ACCOUNT)parameter;
			if (user != null)
				return true;
			return false;
		}
		public void Execute(object parameter)
		{
			vm.showHistoryPay();
		}
		public CmdShowHistoryPay(AdminAccount vm)
		{
			this.vm = vm;
		}
	}
}
