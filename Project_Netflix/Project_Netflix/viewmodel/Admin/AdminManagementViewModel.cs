using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Project_Netflix.model;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Project_Netflix.View.Admin.Account;
using Project_Netflix.View.Admin.Movie;
using Project_Netflix.View.Admin.Category;
using Project_Netflix.View.Admin.Report;
using Project_Netflix.View;
using Project_Netflix.Command.Admin;
using Project_Netflix.View.Admin;

namespace Project_Netflix.viewmodel.Admin
{
	class AdminManagementViewModel : BaseViewModel
	{
		private object _CurrentAdminManager;
		public object CurrentAdminManager { get => _CurrentAdminManager; set { _CurrentAdminManager = value; OnPropertyChanged(); } }
		private ACCOUNT _Admin;
		public ACCOUNT Admin { get => _Admin; set { _Admin = value; OnPropertyChanged(); } }
		public ICommand CmdDashboard { get; set; }
		public ICommand CmdAccount { get; set; }
		public ICommand CmdMovie { get; set; }
		public ICommand CmdCategory { get; set; }
		public ICommand CmdReport { get; set; }
		public ICommand CmdLogout { get; set; }
		public Admin_Account admin_Account { get; set; }
		public Admin_Movie adminMovie { get; set; }
		public AdminCategory adminCategory { get; set; }
		public AdminReport adminReport { get; set; }
		public Dashboard dashboard { get; set; }
		public AdminManagementViewModel()
		{
			CurrentAdminManager = new Dashboard();
			CmdAccount = new RelayCommand<object>((o) =>true, (o) => { CurrentAdminManager = new Admin_Account(); });
			CmdMovie = new RelayCommand<object>((o) => true, (o) => { CurrentAdminManager = new Admin_Movie(); });
			CmdCategory = new RelayCommand<object>((o) => true, (o) => { CurrentAdminManager = new AdminCategory(); });
			CmdReport = new RelayCommand<object>((o) => true, (o) => { CurrentAdminManager = new AdminReport(); });
			CmdDashboard = new RelayCommand<object>((o) => true, (o) => { CurrentAdminManager = new Dashboard(); });
			CmdLogout = new CmdLogout(this);
		}

		public void Logout(Window window)
		{
			DanhNhap danhNhap = new DanhNhap();
			window.Close();
			danhNhap.Show();
		}
	}
}


