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
	public class CmdHuy : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		UserProfileViewModel vm;
		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			var window = (Window)parameter;
			window.Close();
		}
		public CmdHuy(UserProfileViewModel vm)
		{
			this.vm = vm;
		}
	}
}
