using Project_Netflix.View.Admin.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Netflix.viewmodel.Admin.Report
{
	 public class AdminReportViewModel : BaseViewModel
	{
		private object _CurrentPage;
		public object CurrentPage { get => _CurrentPage; set { _CurrentPage = value; OnPropertyChanged(); } }
		
		public Project_Netflix.View.Admin.Report.Revenue revenue { get; set; }
		public Project_Netflix.View.Admin.Report.ViewMovie.ViewMovie viewMovie { get; set; }
		
		public ICommand CmdRevenue { get; set; }
		public ICommand CmdViewMovie { get; set; }
		public ICommand CmdView { get; set; }

		public AdminReportViewModel()
		{

			revenue = new View.Admin.Report.Revenue();
			viewMovie = new View.Admin.Report.ViewMovie.ViewMovie();
			
			CurrentPage = null;
			CmdRevenue = new RelayCommand(o =>
			{
				CurrentPage = revenue;
			});
			CmdViewMovie = new RelayCommand(o =>
			{

				CurrentPage = viewMovie;
			});


		}
	}
}
