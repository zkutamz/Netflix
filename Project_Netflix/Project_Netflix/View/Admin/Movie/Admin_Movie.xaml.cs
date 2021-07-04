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

namespace Project_Netflix.View.Admin.Movie
{
	/// <summary>
	/// Interaction logic for Admin_Movie.xaml
	/// </summary>
	public partial class Admin_Movie : Window
	{
		public static AdminMovie vm = new AdminMovie();
		public Admin_Movie()
		{
			InitializeComponent();
			DataContext = vm;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			InformationMovie addmovie = new InformationMovie();
			addmovie.Show();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			UpdateMovie updateMovie = new UpdateMovie();
			updateMovie.Show();
		}

		private void item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			MessageBox.Show(vm.SelectedMovie.NAME);
		}
	}
}
