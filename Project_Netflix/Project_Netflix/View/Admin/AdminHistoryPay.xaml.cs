using Project_Netflix.model;
using Project_Netflix.viewmodel.Admin;
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

namespace Project_Netflix.View.Admin
{
	/// <summary>
	/// Interaction logic for AdminHistoryPay.xaml
	/// </summary>
	public partial class AdminHistoryPay : Window
	{
		
		public AdminHistoryPay(ACCOUNT user)
		{
			InitializeComponent();
			AdminHistoryViewModel vm = new AdminHistoryViewModel(user);
			DataContext = vm;
		}
	}
}
