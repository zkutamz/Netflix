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
    /// Interaction logic for WatchMovieView.xaml
    /// </summary>
    public partial class WatchMovieView : Window
    {
        DispatcherTimer t;
        WatchMovieViewModel vm;
        public WatchMovieView(int id)
        {
            InitializeComponent(); 
            vm = new WatchMovieViewModel(id);
            DataContext = vm;
            t = new DispatcherTimer();            
            t.Tick += new EventHandler(TimeTick);
            t.Interval = TimeSpan.FromMilliseconds(1);     
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
            time_tick.Maximum = video.NaturalDuration.TimeSpan.TotalSeconds;            
            video.Position = TimeSpan.FromSeconds(time_tick.Value);
        }

        private void hideninfo_Click(object sender, RoutedEventArgs e)
        {
            layout.MaxWidth = 30;
            hideninfo.Visibility = Visibility.Hidden;
            showinfo.Visibility = Visibility.Visible;
            showinfo.Margin = new Thickness(0);
        }

        private void showinfo_Click(object sender, RoutedEventArgs e)
        {
            layout.MaxWidth = 400;
            showinfo.Visibility = Visibility.Hidden;
            hideninfo.Visibility = Visibility.Visible;
        }
        private void FullScreen(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }
    }
}
