﻿using Project_Netflix.ModelTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Netflix.viewmodel
{
    class MainViewModel : BaseViewModel
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand MyListViewCommand { get; set; }
        public RelayCommand FilmsViewCommand { get; set; }
        public RelayCommand OriginalsViewCommand { get; set; }

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