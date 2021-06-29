using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace Project_Netflix.Command
{
	class CmdPasswordChange : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		DangNhapViewModel vm;
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			PasswordBox p = parameter as PasswordBox;
			vm.Password = p.Password;
		}

		public CmdPasswordChange(DangNhapViewModel vm)
		{
			this.vm = vm;
		}
	}
}
