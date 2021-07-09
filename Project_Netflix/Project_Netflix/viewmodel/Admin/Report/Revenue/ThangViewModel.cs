using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Netflix.viewmodel.Admin.Report.Revenue
{
	public class ThangViewModel : BaseViewModel
	{
		private ObservableCollection<int> _DSYear;
		public ObservableCollection<int> DSYear { get => _DSYear; set { _DSYear = value; OnPropertyChanged(); } }
		private ObservableCollection<int> _DSMonth;
		public ObservableCollection<int> DSMonth { get => _DSMonth; set { _DSMonth = value; OnPropertyChanged(); } }
		private int _SelectedMonth;
		public int SelectedMonth { get => _SelectedMonth; set { _SelectedMonth = value; OnPropertyChanged(); } }
		private int _SelectedYear;
		public int SelectedYear { get => _SelectedYear; set { _SelectedYear = value; OnPropertyChanged(); } }

		public ThangViewModel()
		{
			DSYear = new ObservableCollection<int>();
			DSMonth = new ObservableCollection<int>();
			for (int i = 1; i <= 12; i++)
				DSMonth.Add(i);

			using (var db = new NETFLIX_DBEntities())
			{
				var pURCHASEs = db.PURCHASEs.ToList();
				var firstYear = (pURCHASEs.Count > 0) ? pURCHASEs[0].PURCHASED_DATE.Year : DateTime.Now.Year;
				var endYear = DateTime.Now.Year;
				for (int i = firstYear; i <= endYear; i++)
					DSYear.Add(i);
			}
		}
	}
}
