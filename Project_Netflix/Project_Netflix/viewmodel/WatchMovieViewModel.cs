using Project_Netflix.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.viewmodel
{
    public class WatchMovieViewModel : DependencyObject 
    {
        public static readonly DependencyProperty UserIDProperty;
        public static readonly DependencyProperty MovieIDProperty;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<MOVIE> _Movie;
        public ObservableCollection<MOVIE> Movie { get => _Movie; set { _Movie = value; OnPropertyChanged(); } }     
        
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public static ACCOUNT User { get; set; }

        static WatchMovieViewModel()
        {
            UserIDProperty = DependencyProperty.Register("UserID", typeof(string), typeof(DangKyViewModel));
            MovieIDProperty = DependencyProperty.Register("MovieID", typeof(string), typeof(DangKyViewModel));
        }
        public int UserID { get => (int)GetValue(UserIDProperty); set => SetValue(UserIDProperty, value); }
		public int MovieID { get => (int)GetValue(MovieIDProperty); set => SetValue(MovieIDProperty, value); }

        public WatchMovieViewModel(int id)
        {
            var db = new NETFLIX_DBEntities(); 
            Movie = new ObservableCollection<MOVIE>(db.MOVIEs.Where(m => m.ID == id).ToList());
            AddCommand = new RelayCommand<object>((p) =>
            {
                var list = new ObservableCollection<FAVOURITE_MOVIES>(db.FAVOURITE_MOVIES.Where(f => f.MOVIE_ID == id && f.USER_ID == DangNhapViewModel.User.ID));
                MessageBox.Show("as");
                if (list.Count() == 0)
                {
                    MessageBox.Show("This");
                    return false;
                }                    
                return true;
            }, (p) =>
            {
                var favorite = new FAVOURITE_MOVIES()
                {
                    MOVIE_ID = MovieID,
                    USER_ID = DangNhapViewModel.User.ID
                };
                db.FAVOURITE_MOVIES.Add(favorite);
                db.SaveChanges();
                MessageBox.Show("ok");
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                var list = new ObservableCollection<FAVOURITE_MOVIES>(db.FAVOURITE_MOVIES.Where(f => f.MOVIE_ID == id && f.USER_ID == DangNhapViewModel.User.ID));
                if (list == null || list.Count() != 0)
                    return false;
                return true;
            }, (p) =>           
            {
                var favorite = new FAVOURITE_MOVIES()
                {
                    MOVIE_ID = MovieID,
                    USER_ID = DangNhapViewModel.User.ID
                };
                db.FAVOURITE_MOVIES.Attach(favorite);
                db.FAVOURITE_MOVIES.Remove(favorite);
                db.SaveChanges();
            });
        }       
    }
}
