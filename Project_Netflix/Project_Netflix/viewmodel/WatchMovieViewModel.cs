using Project_Netflix.Command.MainWindow;
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
        
        public ICommand CmdAddMyList { get; set; }
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

			CmdAddMyList = new RelayCommand<object>((p) =>
			{
				if (DangNhapViewModel.IsLogin)
				{
					var list = new ObservableCollection<FAVOURITE_MOVIES>(db.FAVOURITE_MOVIES.Where(f => f.MOVIE_ID == id && f.USER_ID == DangNhapViewModel.User.ID).ToList());
					if (list.Count() != 0)
					{
						return false;
					}
					return true;
				}
				return false;
			},
			(p) =>
					 {
						 if (DangNhapViewModel.IsLogin)
						 {
							 var favorite = new FAVOURITE_MOVIES()
							 {
								 MOVIE_ID = id,
								 USER_ID = DangNhapViewModel.User.ID
							 };
							 db.FAVOURITE_MOVIES.Add(favorite);
							 db.SaveChanges();
						 }
						 
					 });

			DeleteCommand = new RelayCommand<object>((p) =>
			{
				if (DangNhapViewModel.IsLogin)
				{
					var list = new ObservableCollection<FAVOURITE_MOVIES>(db.FAVOURITE_MOVIES.Where(f => f.MOVIE_ID == id && f.USER_ID == DangNhapViewModel.User.ID).ToList());
					if (list == null || list.Count() == 0)
						return false;
					return true;
				}return false;
			}, (p) =>
			{
				if (DangNhapViewModel.IsLogin)
				{
					var favorite = db.FAVOURITE_MOVIES.Where(f => f.MOVIE_ID == id && f.USER_ID == DangNhapViewModel.User.ID).Single();
					db.FAVOURITE_MOVIES.Attach(favorite);
					db.FAVOURITE_MOVIES.Remove(favorite);
					db.SaveChanges();
				}
			});
		}   
    }
}
