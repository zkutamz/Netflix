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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_Netflix.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        HomeViewModel hv;
        public HomeView()
        {
            InitializeComponent();
            hv = new HomeViewModel();
        }
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            var video = sender as Grid;
            var me = video.FindName("video") as MediaElement;
            me?.Play();
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            var video = sender as Grid;
            var me = video.FindName("video") as MediaElement;
            me?.Stop();
        }

        private void btnWatch_Click(object sender, RoutedEventArgs e)
        {
            var movie = sender as Button;
            var id = movie.FindName("txtID") as TextBlock;
            WatchMovieView watch = new WatchMovieView(int.Parse(id.Text));
            watch.ShowDialog();
        }
    }
}
