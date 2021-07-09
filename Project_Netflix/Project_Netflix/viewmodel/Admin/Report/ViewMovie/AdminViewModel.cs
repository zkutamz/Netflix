using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Netflix.viewmodel.Admin.Report.ViewMovie
{
	public class AdminViewModel : BaseViewModel
	{
		private ObservableCollection<MOVIE> _DSViewMovie;
		public ObservableCollection<MOVIE> DSViewMovie { get => _DSViewMovie; set { _DSViewMovie = value; OnPropertyChanged(); } }
		private double _Max;
		public double Max { get => _Max; set { _Max = value; OnPropertyChanged(); } }
		private double _Tick;
		public double Tick { get => _Tick; set { _Tick = value; OnPropertyChanged(); } }
		//private ObservableCollection<double> _process;
		//public ObservableCollection<double> process { get => _process; set { _process = value; OnPropertyChanged(); } }

		public AdminViewModel()
		{
			Max = 6000.0;
			using(var db = new NETFLIX_DBEntities())
			{
				DSViewMovie = new ObservableCollection<MOVIE>(db.MOVIEs.ToList());
			}
		}
	}
    public class SliderViewModel : ViewModelBase
    {
        private double _minimum;
        private double _maximum = 100.0;
        private double _tickFrequency = 10.0;
        private double _value = 50.0;

        public double Minimum
        {
            get => _minimum;
            set => SetProperty(ref _minimum, value);
        }

        public double Maximum
        {
            get => _maximum;
            set => SetProperty(ref _maximum, value);
        }

        public double TickFrequency
        {
            get => _tickFrequency;
            set => SetProperty(ref _tickFrequency, value);
        }

        public double Value
        {
            get => _value;
            set => SetProperty(ref _value, value);
        }
    }
}

