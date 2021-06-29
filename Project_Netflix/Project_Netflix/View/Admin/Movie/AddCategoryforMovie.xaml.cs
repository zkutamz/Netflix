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

namespace Project_Netflix.View.Admin.Movie
{
	/// <summary>
	/// Interaction logic for AddCategoryforMovie.xaml
	/// </summary>
	public partial class AddCategoryforMovie : Window
	{
		CategoryViewModel vm = new CategoryViewModel();
		ListBox listBox = null;
		public AddCategoryforMovie(ListBox lb)
		{
			InitializeComponent();
			DataContext = vm;
			listBox = lb;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			bool add = true;
			ListBoxItem listBoxItemName = new ListBoxItem();
			listBoxItemName.Content = vm.ShowSelection().NAME;
			foreach(ListBoxItem item in listBox.Items)
			{
				if(item.Content == listBoxItemName.Content)
				{
					add = false;
					break;
				}
			}
			if(add)
				listBox.Items.Add(listBoxItemName);
			//this.Close();
		}
	}
}
