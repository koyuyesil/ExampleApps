using CliWrap;
using CliWrap.Buffered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

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
            DeviceListing();
        }
        private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var result = await Cli.Wrap("adb").WithArguments("kill-server").ExecuteBufferedAsync();
            tbxLogs.AppendText("INFO : " + result.ExitTime.DateTime + " : ADB Server Killed" + Environment.NewLine);
        }
        private async void DeviceListing()
        {
            tbxLogs.AppendText("INFO : " + DateTime.Now + " : ADB Başlatılıyor...");
            var adbResult = await Cli.Wrap(targetFilePath: "adb").WithArguments("devices").ExecuteBufferedAsync();
            tbxLogs.AppendText(" :OK" + Environment.NewLine);
            if (adbResult.ExitCode != 0)
            {
                tbxLogs.AppendText(adbResult.StandardError + Environment.NewLine);
            }
            else
            {
                rbtnStatus.IsChecked = true;
                List<Dictionary<string, string>> deviceList = new();
                List<string> devices = adbResult.StandardOutput.Split(Environment.NewLine).ToList();
                devices.RemoveAll(s => s == string.Empty);
                devices.RemoveAt(0);
                devices.ForEach(s => tbxLogs.AppendText(s + Environment.NewLine));
                devices.ForEach(s =>
                {
                    drbxDevices.Items.Add(s);
                    var ss = s.Split("\t");
                    deviceList.Add(new Dictionary<string, string>() { { "DeviceID", ss[0] }, { "Status", ss[1] } });
                });
            }

            //var ssss = deviceList[0]["DeviceID"];
        }
        private void DrbxDevices_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            drbxDevices.Items.Clear();
        }
        private async void DrbxDevices_DropDownOpened(object sender, EventArgs e)
        {
            drbxDevices.Items.Clear();
            DeviceListing();
            try
            {
                var result = await Cli.Wrap(targetFilePath: "adb").WithArguments("reconnect offline").ExecuteBufferedAsync();//offline ve unauthorized aygıtları zorlar
                if (result.ExitCode != 0)
                {
                    tbxLogs.AppendText("ERROR : " + result.ExitTime.DateTime + " :" + result.StandardError + Environment.NewLine);
                }
                if (result.StandardOutput == Environment.NewLine)
                {
                    tbxLogs.AppendText("ERROR : " + result.ExitTime.DateTime + " : Please Enable USB Debugging" + Environment.NewLine);
                }
                else
                {
                    tbxLogs.AppendText(result.ExitTime.DateTime + " : " + result.StandardOutput);//output has new line
                }

            }
            catch (Exception ex)
            {
                tbxLogs.AppendText(ex.Message + Environment.NewLine);
            }
        }
        private void DrbxDevices_SelectionChangedAsync(object sender, SelectionChangedEventArgs e)
        {

        }
        private void TbxLogs_Clear_Click(object sender, RoutedEventArgs e)
        {
            tbxLogs.Clear();
        }
        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            DeviceListing();
            rbtnStatus.IsChecked = true;
        }
        private async void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = await Cli.Wrap(targetFilePath: "adb").WithArguments("kill-server").ExecuteBufferedAsync();
                rbtnStatus.IsChecked = false;
                tbxLogs.AppendText("INFO : " + result.ExitTime.DateTime + " : ADB Server Killed" + Environment.NewLine);
            }
            catch (Exception ex)
            {
                tbxLogs.AppendText("ERROR : " + DateTime.Now + " : " + ex.Message + Environment.NewLine);
            }

        }
        private async void GetProps_Click(object sender, RoutedEventArgs e)
        {
            //TRY CATCH
            var adbResult = await Cli.Wrap(targetFilePath: "adb").WithArguments("shell getprop").ExecuteBufferedAsync();
            if (adbResult.ExitCode != 0)
            {
                tbxLogs.AppendText(adbResult.StandardError);
            }
            List<Dictionary<string, string>> propList = new();
            List<string> props = adbResult.StandardOutput.Split("\r\n").ToList();
            props.RemoveAll(s => s == "");
            //devices.RemoveAt(0);//listofdevices string
            props.ForEach(s => tbxLogs.AppendText(s + "\r\n"));
            var pattern = @"\[(.*?)\]";
            props.ForEach(s =>
            {
                var matches = Regex.Matches(s, pattern);
                string s1 = matches[0].Groups[1].ToString();
                string s2 = matches[1].Groups[1].ToString();
                propList.Add(new Dictionary<string, string>() { { s1, s2 } });
            });

        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            progressBar.Value = slider.Value;
        }
#pragma warning disable IDE0051 // Kullanılmayan özel üyeleri kaldır
        private static bool IsEmpty(string s)
        {
            return (s == "");
        }
#pragma warning restore IDE0051 // Kullanılmayan özel üyeleri kaldır
        //devices.RemoveAll(isEmpty); ornek kullanım
        //devices.RemoveAll(s => s == "");ornek kullanım2
        //string tmp = adbResult.StandardOutput.Trim();boşları sil
        //if (tbxLogs.Text != string.Empty)
        //{
        //// tbxLogs.Text += Environment.NewLine;
        //}
    }
}
