using Project_Netflix.model;
using Project_Netflix.ModelTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Project_Netflix.viewmodel
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<MOVIE> _Horror;
        private ObservableCollection<MOVIE> _ActionAventure;
        private ObservableCollection<MOVIE> _ScienceFiction;
        private ObservableCollection<MOVIE> _RomanceAndHistorical;
        private ObservableCollection<MOVIE> _Comedy;
        public ObservableCollection<MOVIE> Horror { get => _Horror; set { _Horror = value; OnPropertyChanged(); } }
        public ObservableCollection<MOVIE> ActionAventure { get => _ActionAventure; set { _ActionAventure = value; OnPropertyChanged(); } }
        public ObservableCollection<MOVIE> ScienceFiction { get => _ScienceFiction; set { _ScienceFiction = value; OnPropertyChanged(); } }
        public ObservableCollection<MOVIE> RomanceAndHistorical { get => _RomanceAndHistorical; set { _RomanceAndHistorical = value; OnPropertyChanged(); } }
        public ObservableCollection<MOVIE> Comedy { get => _Comedy; set { _Comedy = value; OnPropertyChanged(); } }

        public RelayCommand WatchMovieCommand { get; set; }

        public WatchMovieViewModel WatchMovieVM { get; set; }

        public HomeViewModel()
        {
            using (var db = new NETFLIX_DBEntities())
            {
                Horror = new ObservableCollection<MOVIE>(db.MOVIEs.Where(m => m.CATEGORY.NAME == "Kinh dị"));
                ActionAventure = new ObservableCollection<MOVIE>(db.MOVIEs.Where(m => m.CATEGORY.NAME == "Hành động" || m.CATEGORY.NAME == "Phiêu lưu"));
                ScienceFiction = new ObservableCollection<MOVIE>(db.MOVIEs.Where(m => m.CATEGORY.NAME == "Khoa học viễn tưởng"));
                RomanceAndHistorical = new ObservableCollection<MOVIE>(db.MOVIEs.Where(m => m.CATEGORY.NAME == "Sử thi" || m.CATEGORY.NAME == "Tình cảm"));
                Comedy = new ObservableCollection<MOVIE>(db.MOVIEs.Where(m => m.CATEGORY.NAME == "Hài"));
            }
            WatchMovieCommand = new RelayCommand(o =>
            {                
                WatchMovieVM = new WatchMovieViewModel();                
                
            });

        }
    }
}
