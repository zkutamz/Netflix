using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Project_Netflix.Command.MainWindow
{
	class CmdPasswordChangeDK : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		DangKyViewModel vm;
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			PasswordBox p = parameter as PasswordBox;
			vm.Password = p.Password;
		}

		public CmdPasswordChangeDK(DangKyViewModel vm)
		{
			this.vm = vm;
		}
	}
}
