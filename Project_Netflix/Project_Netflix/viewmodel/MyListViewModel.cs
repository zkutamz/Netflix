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
        private int _UserID;
        public int UserID { get => _UserID; set { _UserID = value; OnPropertyChanged(); } }

        public MyListViewModel()
        {
            var db = new NETFLIX_DBEntities();
            Mylist = new ObservableCollection<FAVOURITE_MOVIES>(db.FAVOURITE_MOVIES.Where(x => x.USER_ID == UserID).ToList());
        }
    }
}
