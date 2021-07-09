using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Netflix.viewmodel.Admin.Report.Revenue
{
	public class QuyViewModel : BaseViewModel
	{
		private ObservableCollection<int> _DSYear;
		public ObservableCollection<int> DSYear { get => _DSYear; set { _DSYear = value; OnPropertyChanged(); } }
		private int _SelectedYear;
		public int SelectedYear { get => _SelectedYear; set { _SelectedYear = value; OnPropertyChanged(); } }
		private ObservableCollection<int> _DSQuy;
		public ObservableCollection<int> DSQuy { get => _DSQuy; set { _DSQuy = value; OnPropertyChanged(); } }
		private int _SelectedQuy;
		public int SelectedQuy { get => _SelectedQuy; set { _SelectedQuy = value; OnPropertyChanged(); } }

		public QuyViewModel()
		{
			DSYear = new ObservableCollection<int>();
			DSQuy = new ObservableCollection<int>();
			for (int i = 1; i <= 4; i++)
				DSQuy.Add(i);

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
