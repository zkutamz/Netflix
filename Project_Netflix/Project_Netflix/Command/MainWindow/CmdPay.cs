
using Project_Netflix.View;
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
	public class CmdPay : ICommand
	{
		PayViewModel vm;
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter)
		{
			if(PagePay.choose != null)
			{
				return true;
			}
			return false;
		}

		public void Execute(object parameter)
		{
			var window = (Window)parameter;
			vm.Pay(window);
		}
		public CmdPay(PayViewModel vm)
		{
			this.vm = vm;
		}
	}
}
