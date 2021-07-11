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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Netflix.View
{
	/// <summary>
	/// Interaction logic for UserInfo.xaml
	/// </summary>
	public partial class UserInfo : UserControl
	{
		public UserInfo()
		{
			InitializeComponent();
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
					HistoryPay historyPay = new HistoryPay();
					historyPay.ShowDialog();
					break;
				case 2:
					DangNhapViewModel.IsLogin = false;
					DangNhapViewModel.User = null;
					DanhNhap danhNhap = new DanhNhap();
					DangNhapViewModel.main.Close();
					danhNhap.Show();
					break;
			}

		}
		private void Chip_Click(object sender, RoutedEventArgs e)
		{
			ProfileInformation profile = new ProfileInformation();
			profile.ShowDialog();
		}
	}
}
