using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for DangKy.xaml
    /// </summary>
    public partial class DangKy : Window
    {
        public static DangKyViewModel vmdk = new DangKyViewModel();

        public DangKy()
        {
            InitializeComponent();
            DataContext = vmdk;
        }
        Profile profile = new Profile();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Regex emailCheck = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = emailCheck.Match(EmailDangKy.Text);
            if (match.Success && FloatingPasswordBox.Password.Length >= 8)
            {
                profile.Show();
                this.Close();
            }
        }
    }
}