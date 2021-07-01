using Project_Netflix.viewmodel.Admin.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Netflix.Command.Admin.Category
{
	class CmdAddCategory : ICommand
	{
		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}
		AdminCategoryViewModel vm;
		public bool CanExecute(object parameter)
		{
			//if(parameter != null)
			return true;
			//else
			//	return false;
		}
		public void Execute(object parameter)
		{
			
			vm.addCategory();
		}
		public CmdAddCategory(AdminCategoryViewModel vm)
		{
			this.vm = vm;
		}
	}
}
