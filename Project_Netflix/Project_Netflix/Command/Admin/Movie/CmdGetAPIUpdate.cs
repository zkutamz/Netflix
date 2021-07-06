using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Netflix.Command.Admin.Movie
{
	public class CmdGetAPIUpdate : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminMovie vm;
		public bool CanExecute(object parameter)
		{
			//if (vm.Name.Length > 0)
			return true;
			//return false;
		}
		public void Execute(object parameter)
		{
			vm.GetAPI();
		}
		public CmdGetAPIUpdate(AdminMovie vm)
		{
			this.vm = vm;
		}
	}
}

