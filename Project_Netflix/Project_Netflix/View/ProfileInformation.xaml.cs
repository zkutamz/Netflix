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
	/// Interaction logic for ProfileInformation.xaml
	/// </summary>
	public partial class ProfileInformation : Window
	{
		UserProfileViewModel vm = new UserProfileViewModel();
		ACCOUNT _user = null;
		public ProfileInformation(ACCOUNT user)
		{
			InitializeComponent();
			DataContext = vm;
			_user = user;
			vm.User = _user;
		}

		private void Flipper_OnIsFlippedChanged(object sender, RoutedPropertyChangedEventArgs<bool> e)
			=> System.Diagnostics.Debug.WriteLine($"Card is flipped = {e.NewValue}");
	}
}
