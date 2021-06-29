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

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            Info.Visibility = Visibility.Collapsed;
            Sign.Visibility = Visibility.Hidden;
            DanhNhap danhNhap = new DanhNhap();
            //danhNhap.Show();
			//if (DangNhapViewModel.IsLogin)
			//{
   //             ChangeSignvsInfo();
   //         }
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
            DangKy dangKy = new DangKy();
            dangKy.Show();
		}
        

		
	}
}
