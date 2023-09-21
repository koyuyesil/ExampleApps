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

namespace DebugTip
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            //BORDER DENE
            Point canvasMosuePoint = Mouse.GetPosition(canvas1);
            if (!debugTip.IsOpen) { debugTip.IsOpen = true; }
            debugTip.HorizontalOffset = canvasMosuePoint.X + 20;
            debugTip.VerticalOffset = canvasMosuePoint.Y;
            tbxTip.Text = "Position X:" + canvasMosuePoint.X + ", Y:" + canvasMosuePoint.Y;
        }

        private void Canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            debugTip.IsOpen = false;
        }
    }
}
