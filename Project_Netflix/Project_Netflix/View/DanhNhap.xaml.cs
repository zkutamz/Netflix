using Project_Netflix.model;
using Project_Netflix.viewmodel;
using System;
using System.Collections.Generic;
using System.Data;
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


namespace Project_Netflix.View
{
    /// <summary>
    /// Interaction logic for DanhNhap.xaml
    /// </summary>
    public partial class DanhNhap : Window
    {
        DangNhapViewModel vm = new DangNhapViewModel();
        public DanhNhap()
        {
            InitializeComponent();
            DataContext = vm;
        }
        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            forgotPassword.FontWeight = FontWeights.Bold;
        }

        private void forgotPassword_MouseLeave(object sender, MouseEventArgs e)
        {
            forgotPassword.FontWeight = FontWeights.Normal;
        }

        private void register_MouseEnter(object sender, MouseEventArgs e)
        {
            register.Foreground = Brushes.CadetBlue;
        }

        private void register_MouseLeave(object sender, MouseEventArgs e)
        {
            register.Foreground = Brushes.White;
        }

        private void forgotPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ForgotPassword forgotPassword = new ForgotPassword();
            forgotPassword.Show();
        }

        private void register_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DangKy dangKy = new DangKy();
            dangKy.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}