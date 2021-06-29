using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Project_Netflix.model;
using System.Windows;
using System.Collections.ObjectModel;

namespace Project_Netflix.viewmodel.Admin
{
    class AdminManagementViewModel : DependencyObject
	{
		public static readonly DependencyProperty AdminUserProperty;
		static AdminManagementViewModel()
		{
			AdminUserProperty = DependencyProperty.Register("DSAccount", typeof(ObservableCollection<ACCOUNT>), typeof(AdminManagementViewModel));
		}
		public ObservableCollection<ACCOUNT> DSAccount
		{
			get => (ObservableCollection<ACCOUNT>)GetValue(AdminUserProperty);
			set => SetValue(AdminUserProperty, value);
		}
		public AdminManagementViewModel()
		{
			using (var db = new NETFLIX_DBEntities())
			{
				DSAccount = new ObservableCollection<ACCOUNT>(db.ACCOUNTs.Include("ACCOUNT_TYPE").Include("USER_INFORMATION").ToList());
			}
		}
	}
}

