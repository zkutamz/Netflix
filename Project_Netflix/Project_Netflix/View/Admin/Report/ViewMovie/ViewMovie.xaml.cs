using Project_Netflix.viewmodel.Admin.Report.ViewMovie;
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

namespace Project_Netflix.View.Admin.Report.ViewMovie
{
	/// <summary>
	/// Interaction logic for ViewMovie.xaml
	/// </summary>
	public partial class ViewMovie : UserControl
	{
		
		public ViewMovie()
		{
			InitializeComponent();
			AdminViewModel vm = new AdminViewModel();
			DataContext = vm;
			vm.Max = 60000;
		}
	}
}
