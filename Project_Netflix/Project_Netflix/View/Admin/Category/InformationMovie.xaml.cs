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
	/// Interaction logic for InformationMovie.xaml
	/// </summary>
	public partial class InformationMovie : Window
	{

		public InformationMovie()
		{
			InitializeComponent();
			DataContext = AdminCategory.vm;
		}

		private void PosterFile_Click_1(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
			openFileDialog.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			Nullable<bool> result = openFileDialog.ShowDialog();
			if (result == true)
			{
				PosterText.Text = getFileName(openFileDialog.FileName);
				AdminCategory.vm.Poster = openFileDialog.FileName;
				
			}
		}
		string getFileName(string fileName)
		{
			string[] tokken = fileName.Split('\\');
			return tokken.Last();
		}
		private void TrailerFile_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
			openFileDialog.Filter = "Movie files(*.mp4; *.mov;)| *.mp4; *.mov; | All files(*.*) | *.* ";
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			Nullable<bool> result = openFileDialog.ShowDialog();
			if (result == true)
			{
				TrailerText.Text = getFileName(openFileDialog.FileName);
				AdminCategory.vm.Trailer = openFileDialog.FileName;
			}
		}
		private void MovieFile_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
			openFileDialog.Filter = "Movie files(*.mp4; *.mov;)| *.mp4; *.mov; | All files(*.*) | *.* ";
			openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			Nullable<bool> result = openFileDialog.ShowDialog();
			if (result == true)
			{
				MovieText.Text = getFileName(openFileDialog.FileName);
				AdminCategory.vm.Movie = openFileDialog.FileName;
			}
		}

		private void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
		{
			AdminCategory.vm.Rate = BasicRatingBar.Value;
		}
	}
}
