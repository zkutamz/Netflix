using Project_Netflix.ModelTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Netflix.viewmodel
{
    public class HomeViewModel : DependencyObject
    {
        public static readonly DependencyProperty DSMovieProperty;
        static HomeViewModel()
        {
            DSMovieProperty = DependencyProperty.Register("DSMovie", typeof(IList<Movie>), typeof(MainViewModel));
        }

        public IList<Movie> DSMovie
        {
            get => (IList<Movie>)GetValue(DSMovieProperty);
            set => SetValue(DSMovieProperty, value);
        }
        public HomeViewModel()
        {
            DSMovie = Enumerable.Range(0, 10).Select(i => new Movie()).ToList();
        }
    }
}
