using CliWrap;
using CliWrap.Buffered;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;

namespace WPFUygulamasiNET6
{
    public class CommandInvoker
    {
        private readonly List<ICommand> _commands;
        System.Windows.Controls.RichTextBox _richTextBox;

        public CommandInvoker(System.Windows.Controls.RichTextBox logBox)
        {
            _commands = new List<ICommand>();
            _richTextBox = logBox;
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public async Task RunAsync()
        {
            foreach (var command in _commands)
            {
             var result  = await command.ExecuteAsync();
                Serilog.Log.Information("Komut çıktısı: {Output}", result);
                var message = new Paragraph();
                message.Inlines.Add(new Run(result));

                var brush = new SolidColorBrush(Colors.Green);
                message.Foreground = brush;
                _richTextBox.Document.Blocks.Add(message);
            }
            
            _richTextBox.ScrollToEnd();

        }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbDevices.SelectedIndex = -1;
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
                    cmbDevices.Items.Add(s);
                    var ss = s.Split("\t");
                    deviceList.Add(new Dictionary<string, string>() { { "DeviceID", ss[0] }, { "Status", ss[1] } });
                });
            }

            //var ssss = deviceList[0]["DeviceID"];
        }
        private void cmbDevices_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            cmbDevices.Items.Clear();
        }
        private async void cmbDevices_DropDownOpened(object sender, EventArgs e)
        {
            cmbDevices.Items.Clear();
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
        private void tbxMenuItemLogsClear_Click(object sender, RoutedEventArgs e)
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

        private async void Button_ClickAsync(object sender, RoutedEventArgs e)
        {
            var invoker = new CommandInvoker(logBox);
            invoker.AddCommand(new Command("adb","devices"));
            invoker.AddCommand(new Command("adb","getprop product"));
            invoker.AddCommand(new Command("adb","shell dumpsys battery"));
            await invoker.RunAsync();
        }





        //private static bool IsEmpty(string s)
        //{
        //    return s == "";
        //}
        //devices.RemoveAll(isEmpty); ornek kullanım
        //devices.RemoveAll(s => s == "");ornek kullanım2
        //string tmp = adbResult.StandardOutput.Trim();boşları sil
        //if (tbxLogs.Text != string.Empty)
        //{
        //// tbxLogs.Text += Environment.NewLine;
        //}
    }
}
