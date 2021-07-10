using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Project_Netflix.viewmodel.Admin.Report.ViewModel
{
    public class AdminViewModel : BaseViewModel
    {
        private ObservableCollection<MOVIE> _DSViewMovie;
        public ObservableCollection<MOVIE> DSViewMovie { get => _DSViewMovie; set { _DSViewMovie = value; OnPropertyChanged(); } }
        private ObservableCollection<Object> _Items;
        public ObservableCollection<Object> Items { get => _Items; set { _Items = value; OnPropertyChanged(); } }
        private double _Tick;
        public double Tick { get => _Tick; set { _Tick = value; OnPropertyChanged(); } }
        private double _Maximum;
        public double Maximum { get => _Maximum; set { _Maximum = value; OnPropertyChanged(); } }
        //private ObservableCollection<double> _process;
        //public ObservableCollection<double> process { get => _process; set { _process = value; OnPropertyChanged(); } }
        public ICommand CmdLoad { get; set; }
        public AdminViewModel()
        {
            Tick = 10.0;
            Maximum = 1000.0;
            CmdLoad = new RelayCommand<Object>((o) => true, (o) => { Load(); });
            

        }
        public void Load()
		{
            if (Maximum < 0) Maximum = 1000.0;
            if (Tick < 0) Tick = 10.0;
            using (var db = new NETFLIX_DBEntities())
			{
				DSViewMovie = new ObservableCollection<MOVIE>(db.MOVIEs.ToList());
            Items = new ObservableCollection<Object>();
            foreach (var item in DSViewMovie)
                {
                    var view = new Slider { Name = item.NAME, View = (item.VIEWS != null) ? (int)item.VIEWS : 0, Max = Maximum, TickFrequency = Tick };
                    Items.Add(view);
                }
            }
        }
        public class Slider : BaseViewModel
		{
            private string _Name;
            public string Name { get => _Name; set { _Name = value; OnPropertyChanged(); } }
            private int _View;
            public int View { get=>_View;set { _View = value; OnPropertyChanged(); } }
            private double _Max;
            public double Max { get => _Max; set { _Max = value; OnPropertyChanged(); } }
            private double _TickFrequency;
            public double TickFrequency { get => _TickFrequency; set { _TickFrequency = value; OnPropertyChanged(); } }
        }
    }
    
   
}

