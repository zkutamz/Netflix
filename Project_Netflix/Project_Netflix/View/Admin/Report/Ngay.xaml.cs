using Project_Netflix.viewmodel.Admin.Report.Revenue;
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

namespace Project_Netflix.View.Admin.Report
{
	/// <summary>
	/// Interaction logic for Ngay.xaml
	/// </summary>
	public partial class Ngay : UserControl
	{
		public static NgayViewModel vm = new NgayViewModel();
		public Ngay()
		{
			InitializeComponent();
			DataContext = vm;
		}
	}
}
