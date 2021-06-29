using Project_Netflix.ModelTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project_Netflix.viewmodel
{
    public class MyListViewModel : DependencyObject
    {
        public static readonly DependencyProperty MyListProperty;
        static MyListViewModel()
        {
            MyListProperty = DependencyProperty.Register("MyList", typeof(IList<Movie>), typeof(MainViewModel));
        }

        public IList<Movie> MyList
        {
            get => (IList<Movie>)GetValue(MyListProperty);
            set => SetValue(MyListProperty, value);
        }
        public MyListViewModel()
        {
            MyList = Enumerable.Range(0, 10).Select(i => new Movie()).ToList();
        }
    }
}
