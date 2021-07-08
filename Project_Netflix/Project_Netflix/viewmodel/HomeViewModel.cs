using Project_Netflix.model;
using Project_Netflix.ModelTest;
using Project_Netflix.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Project_Netflix.viewmodel
{
    public class HomeViewModel : DependencyObject
    {
        private string _MovieID;
        public string MovieID { get => _MovieID; set { _MovieID = value; OnPropertyChanged(); } }

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
        }
    }
}
