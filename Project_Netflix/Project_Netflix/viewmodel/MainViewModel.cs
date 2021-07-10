using Project_Netflix.Command.MainWindow;
using Project_Netflix.model;
using Project_Netflix.ModelTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project_Netflix.viewmodel
{
    public class MainViewModel : BaseViewModel
    {        
        public ICommand HomeViewCommand { get; set; }
        public ICommand MyListViewCommand { get; set; }
        public ICommand OriginalsViewCommand { get; set; }
        /// <summary>
        /// /////////////////////////////////////////
        /// </summary>
        public ACCOUNT User { get; set; }
        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>
        public HomeViewModel HomeVM { get; set; }
        public MyListViewModel MyListVM { get; set; }

        public OriginalsViewModel OriginalsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set {
                _currentView = value;
                OnPropertyChanged();
            }
        }
        
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();            
            MyListVM = new MyListViewModel();
            OriginalsVM = new OriginalsViewModel();
            User = DangNhapViewModel.User;
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand<object>(o=> { return true; },
            (o) =>
            {
                CurrentView = HomeVM;
            });
            MyListViewCommand = new RelayCommand<object>(o => { return true; },
            (o) =>
            {
                CurrentView = MyListVM;
            });            
            OriginalsViewCommand = new RelayCommand<object>(o => { return true; },
            (o) =>
            {
                CurrentView = OriginalsVM;
            });
        }  
        public void LoadUser()
        {
            User = DangNhapViewModel.User;
        }
    }
}
