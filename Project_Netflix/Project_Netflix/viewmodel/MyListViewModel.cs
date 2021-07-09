using Project_Netflix.model;
using Project_Netflix.ModelTest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Netflix.viewmodel
{
    public class MyListViewModel : BaseViewModel
    {
        private ObservableCollection<FAVOURITE_MOVIES> _Mylist;
        public ObservableCollection<FAVOURITE_MOVIES> Mylist { get => _Mylist; set { _Mylist = value; OnPropertyChanged(); } }
        
        public MyListViewModel()
        {
            var db = new NETFLIX_DBEntities();
            Mylist = new ObservableCollection<FAVOURITE_MOVIES>(db.FAVOURITE_MOVIES.Include("MOVIE").Where(x => x.USER_ID == DangNhapViewModel.User.ID).ToList());
      
        }
    }
}
