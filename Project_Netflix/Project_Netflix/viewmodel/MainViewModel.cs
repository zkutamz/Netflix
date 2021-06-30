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
    class MainViewModel : BaseViewModel
    {
        
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand MyListViewCommand { get; set; }
        public RelayCommand FilmsViewCommand { get; set; }
        public RelayCommand OriginalsViewCommand { get; set; }
        /// <summary>
        /// /////////////////////////////////////////
        /// </summary>
        public ACCOUNT User { get; set; }
        /// <summary>
        /// //////////////////////////////////////////
        /// </summary>
        public HomeViewModel HomeVM { get; set; }
        public MyListViewModel MyListVM { get; set; }

        public FilmsViewModel FilmsVM { get; set; }
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
            FilmsVM = new FilmsViewModel();
            OriginalsVM = new OriginalsViewModel();
            User = DangNhapViewModel.User;
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o=>
            {
                CurrentView = HomeVM;
            });
            MyListViewCommand = new RelayCommand(o =>
            {
                CurrentView = MyListVM;
            });
            FilmsViewCommand = new RelayCommand(o =>
            {
                CurrentView = FilmsVM;
            });
            OriginalsViewCommand = new RelayCommand(o =>
            {
                CurrentView = OriginalsVM;
            });
        }        
    }
}
