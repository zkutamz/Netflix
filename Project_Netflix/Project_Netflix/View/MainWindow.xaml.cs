using Project_Netflix.model;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
        }

		private void Chip_Click(object sender, RoutedEventArgs e)
		{
            ProfileInformation profile = new ProfileInformation();
            profile.ShowDialog();
		}

		private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
            ProfileInformation profile = new ProfileInformation();
            profile.Show();
        }

		private void ListBoxItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
            ProfileInformation profile = new ProfileInformation();
            profile.ShowDialog();
        }




		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			switch (UserControl.SelectedIndex)
			{
                case 0:
					ProfileInformation profile = new ProfileInformation();
					profile.ShowDialog();
					break;
                case 1:
                    MessageBox.Show("1");
                    break;
                case 2:
                    DangNhapViewModel.IsLogin = false;
                    DangNhapViewModel.User = null;
                    DanhNhap danhNhap = new DanhNhap();
                    this.Close();
                    danhNhap.Show();
                    break;
			}

        }
	}
}
