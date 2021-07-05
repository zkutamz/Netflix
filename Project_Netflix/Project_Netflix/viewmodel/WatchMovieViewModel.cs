using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Netflix.viewmodel
{
    public class WatchMovieViewModel : BaseViewModel
    {
        private ObservableCollection<MOVIE> _Movie;
        public ObservableCollection<MOVIE> Movie { get => _Movie; set { _Movie = value; OnPropertyChanged(); } }

        public WatchMovieViewModel(int id)
        {
            using (var db = new NETFLIX_DBEntities())
            {
                Movie = new ObservableCollection<MOVIE>(db.MOVIEs.Where(m => m.ID == id).ToList());
            }
        }
        public WatchMovieViewModel()
        {

        }
    }
}
