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

namespace Project_Netflix.View
{
    /// <summary>
    /// Interaction logic for PagePay.xaml
    /// </summary>
    public partial class PagePay : Window
    {
        public PagePay()
        {
            InitializeComponent();
        }
        void setColorPage(string color, string type)
        {
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFromString(color);
            switch (type.ToLower())
            {
                case "basic":
                    PhimHDBasic.Background = brush;
                    GioiHanPhimBasic.Background = brush;
                    TinhNang1Basic.Background = brush;
                    TinhNang2Basic.Background = brush;
                    HuyThueBasic.Background = brush;
                    break;
                case "standard":
                    PhimHDStandard.Background = brush;
                    GioiHanPhimStandard.Background = brush;
                    TinhNang1Standard.Background = brush;
                    TinhNang2Standard.Background = brush;
                    HuyThueStandard.Background = brush;
                    break;
                case "premium":
                    PhimHDPremium.Background = brush;
                    GioiHanPhimPremium.Background = brush;
                    TinhNang1Premium.Background = brush;
                    TinhNang2Premium.Background = brush;
                    HuyThuePremium.Background = brush;
                    break;
            }
        }
        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFromString("#F40612");
            color.Freeze();
            Basic.Background = color;
            color = (Brush)bc.ConvertFromString("#FA7967");
            Standard.Background = color;
            Premium.Background = color;
            setColorPage("#FA7967", "Basic");
            setColorPage("#FFF7F7F7", "Standard");
            setColorPage("#FFF7F7F7", "Premium");
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFromString("#F40612");
            color.Freeze();
            Standard.Background = color;
            color = (Brush)bc.ConvertFromString("#FA7967");
            Basic.Background = color;
            Premium.Background = color;
            setColorPage("#FFF7F7F7", "Basic");
            setColorPage("#FA7967", "Standard");
            setColorPage("#FFF7F7F7", "Premium");
        }

        private void TextBlock_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Brush color = (Brush)bc.ConvertFromString("#F40612");
            color.Freeze();
            Premium.Background = color;
            color = (Brush)bc.ConvertFromString("#FA7967");
            Standard.Background = color;
            Basic.Background = color;
            setColorPage("#FFF7F7F7", "Basic");
            setColorPage("#FFF7F7F7", "Standard");
            setColorPage("#FA7967", "Premium");
        }
    }
}
