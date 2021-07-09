using Project_Netflix.viewmodel.Admin.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Netflix.Command.Admin.Category
{
	public class CmdGetAPI : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminCategoryViewModel vm;
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
		public CmdGetAPI(AdminCategoryViewModel vm)
		{
			this.vm = vm;
		}
	}
}

