using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using CliWrap;
using CliWrap.Buffered;

namespace WPFUygulamasiNET6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
             
        {
            drbxDevices.SelectedIndex = -1;

            deviceListing();

        }
        private bool isEmpty(string s)
        {
            return (s == "");
        }
        private async void deviceListing()
        {
            var adbResult = await Cli.Wrap(targetFilePath: "adb").WithArguments("devices").ExecuteBufferedAsync();
            if (adbResult.ExitCode != 0)
            {
                tbxLogs.AppendText(adbResult.StandardError);
            }

            //string tmp = adbResult.StandardOutput.Trim();
            List<string> devices = adbResult.StandardOutput.Split("\r\n").ToList();
            //devices.RemoveAll(isEmpty);
            devices.RemoveAll(s => s == "");
            devices.ForEach(s => tbxLogs.AppendText(s + "\r\n"));
            devices.ForEach(s => drbxDevices.Items.Add(s));

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            progressBar.Value = slider.Value;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            deviceListing();
            rbtnStatus.IsChecked = true;
        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            await Cli.Wrap(targetFilePath: "adb").WithArguments("kill-server").ExecuteBufferedAsync();
            rbtnStatus.IsChecked = false;
        }

        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            await Cli.Wrap(targetFilePath: "adb").WithArguments("kill-server").ExecuteBufferedAsync();
            rbtnStatus.IsChecked = false;
        }

        private void drbxDevices_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            drbxDevices.Items.Clear();
        }

        private void drbxDevices_DropDownOpened(object sender, EventArgs e)
        {
            drbxDevices.Items.Clear();
            deviceListing();
        }

        private async void drbxDevices_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {
            var selected = drbxDevices.SelectedIndex;
            if (selected != 0 && selected != -1)
            {
                try
                {
                    var result = await Cli.Wrap(targetFilePath: "adb").WithArguments("reboot").ExecuteBufferedAsync();
                    tbxLogs.Text = result.StandardError;
                    tbxLogs.AppendText(result.StandardOutput);
                    rbtnStatus.IsChecked = false;
                }
                catch (Exception DFSDF)
                {

                    tbxLogs.AppendText(DFSDF.Message);
                }
                
            }
        }
    }
}
