using Project_Netflix.viewmodel.Admin.Report.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Netflix.View.Admin.Report.ViewModel
{
	/// <summary>
	/// Interaction logic for ViewModel.xaml
	/// </summary>
	public partial class ViewModel : UserControl
	{
		public ViewModel()
		{
			InitializeComponent();
			AdminViewModel vm = new AdminViewModel();
			DataContext = vm;
			
		}
	}
}
