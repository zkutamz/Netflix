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
using Project_Netflix.viewmodel;
using Project_Netflix.viewmodel.Admin;
using Project_Netflix.View.Admin.Account;
using Project_Netflix.View.Admin.Movie;
using Project_Netflix.View.Admin.Category;
using Project_Netflix.View.Admin;
using Project_Netflix.View.Admin.Report;

namespace Project_Netflix.View
{
    /// <summary>
    /// Interaction logic for Admin_Management.xaml
    /// </summary>
    public partial class Admin_Management : Window
    {
        AdminManagementViewModel vm = new AdminManagementViewModel();
        public Admin_Management()
        {
            InitializeComponent();
           
            this.DataContext = vm;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Admin_Account account = new Admin_Account();
            account.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Admin_Movie movie = new Admin_Movie();
            movie.Show();
        }

		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
            AdminCategory category = new AdminCategory();
            category.Show();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e)
		{
            AdminReport adminReport = new AdminReport();
            adminReport.ShowDialog();
		}
	}
}
   