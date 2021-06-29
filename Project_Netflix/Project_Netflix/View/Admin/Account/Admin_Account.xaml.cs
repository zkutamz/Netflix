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
using Project_Netflix.viewmodel.Admin.Account;

namespace Project_Netflix.View.Admin.Account
{
	/// <summary>
	/// Interaction logic for Admin_Account.xaml
	/// </summary>
	public partial class Admin_Account : Window
	{
		AdminAccount vmadmin = new AdminAccount();
		public Admin_Account()
		{
			InitializeComponent();
			DataContext = vmadmin;
		}
	}
}
