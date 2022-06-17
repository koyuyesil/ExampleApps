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

namespace WPFUygulamasi
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //int a=1, b=5, toplam=10;
            //toplam = a + b+toplam;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Sinif.Statikmethod();
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Sinif ornek = new Sinif();
            ornek.OrnekMethod();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Sinif ornek = new Sinif("örsan", "akciyer");
            ornek.OrnekMethod();
        }
    }
}
