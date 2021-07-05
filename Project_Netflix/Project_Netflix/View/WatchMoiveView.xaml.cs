using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project_Netflix.View
{
    /// <summary>
    /// Interaction logic for WatchMoiveView.xaml
    /// </summary>
    public partial class WatchMoiveView : Window
    {        
        DispatcherTimer t;
        WatchMovieViewModel vm;

        public WatchMoiveView() { }
        public WatchMoiveView(int id)
        {
            InitializeComponent();
            vm = new WatchMovieViewModel(id);
            DataContext = vm; 
            t = new DispatcherTimer();
            t.Tick += new EventHandler(TimeTick);
            t.Interval = TimeSpan.FromMilliseconds(300);
            video.Play();
            t.Start();
        }
        void TimeTick(object sender, EventArgs e)
        {
            time_tick.Value = video.Position.TotalSeconds;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            video.Play();
            btnPlay.Visibility = Visibility.Hidden;
            btnPause.Visibility = Visibility.Visible;
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            video.Pause();
            btnPause.Visibility = Visibility.Hidden;
            btnPlay.Visibility = Visibility.Visible;
        }        

        private void volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            video.Volume = (double)volume.Value;
        }

        private void time_tick_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            video.Position = TimeSpan.FromSeconds(time_tick.Value);
        }
    }
}
