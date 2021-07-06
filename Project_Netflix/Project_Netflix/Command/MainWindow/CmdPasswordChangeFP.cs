using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project_Netflix.Command.MainWindow
{
	class CmdPasswordChangeFP : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		ForgotPasswordViewModel vm;
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			PasswordBox p = parameter as PasswordBox;
			vm.PasswordForgot = p.Password;
		}

		public CmdPasswordChangeFP(ForgotPasswordViewModel vm)
		{
			this.vm = vm;
		}
	}
}
