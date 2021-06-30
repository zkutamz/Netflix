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
	public class CmdUpdateUser : ICommand
	{
		UserProfileViewModel vm;
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			return vm.checkUpdate();
		}

		public void Execute(object parameter)
		{
			vm.UpdateUser();
			var window = (Window)parameter;
			window.Close();
		}
		public CmdUpdateUser(UserProfileViewModel vm)
		{
			this.vm = vm;
		}
	}
}
