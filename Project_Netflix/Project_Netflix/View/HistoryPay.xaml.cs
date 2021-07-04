using Project_Netflix.viewmodel;
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
using System.Windows.Shapes;

namespace Project_Netflix.View
{
	/// <summary>
	/// Interaction logic for HistoryPay.xaml
	/// </summary>
	public partial class HistoryPay : Window
	{
		HistoryPayViewModel vm = new HistoryPayViewModel();
		public HistoryPay()
		{
			InitializeComponent();
			DataContext = vm;
		}
	}
}
