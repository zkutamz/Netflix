using Project_Netflix.model;
using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.Command
{
	public class CmdDangNhap : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		DangNhapViewModel vm;
		public bool CanExecute(object parameter)
		{
			return vm.allowButton();
		}

		public void Execute(object parameter)
		{
			var window = (Window)parameter;
			vm.Login(window);
		}

		public CmdDangNhap(DangNhapViewModel vm)
		{
			this.vm = vm;
		}
	}
}
