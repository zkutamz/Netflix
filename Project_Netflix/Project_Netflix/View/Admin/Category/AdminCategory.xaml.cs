using Project_Netflix.model;
using Project_Netflix.View.Admin.Movie;
using Project_Netflix.viewmodel.Admin.Category;
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

namespace Project_Netflix.View.Admin.Category
{
	/// <summary>
	/// Interaction logic for AdminCategory.xaml
	/// </summary>
	public partial class AdminCategory : UserControl
	{
		public static AdminCategoryViewModel vm = new AdminCategoryViewModel();
		public AdminCategory()
		{
			InitializeComponent();
			DataContext = vm;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			InformationMovie movie = new InformationMovie();
			movie.Show();
			
		}
	}
}
