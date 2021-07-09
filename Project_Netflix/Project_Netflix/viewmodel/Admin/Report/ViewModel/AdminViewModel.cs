using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Netflix.viewmodel.Admin.Report.ViewModel
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
            using (var db = new NETFLIX_DBEntities())
            {
                DSViewMovie = new ObservableCollection<MOVIE>(db.MOVIEs.ToList());
            }
        }
    }
    
   
}

