using CliWrap;
using CliWrap.Buffered;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;

namespace SimpleAdbClientNET6
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
                var result = await command.ExecuteAsync();
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

            //string ProcessPath = Environment.ProcessPath;
            string CurrentDirectory = Environment.CurrentDirectory;
            string Roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string Local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string ProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            string logDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), appName);
            string logFilePath = Path.Combine(logDirectory, $"{appName}_log.txt");
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug().WriteTo
            .File(logFilePath, rollingInterval: RollingInterval.Day)
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
            try
            {
                tbxLogs.AppendText("INFO : " + DateTime.Now + " : ADB Başlatılıyor...");
                var result = await Cli.Wrap(targetFilePath: "adb").WithArguments("devices").ExecuteBufferedAsync();
                tbxLogs.AppendText(" :OK" + Environment.NewLine);

                switch (result.ExitCode)
                {
                    case 0:
                        if (result.StandardOutput == Environment.NewLine)
                        {
                            tbxLogs.AppendText("ERROR : " + result.ExitTime.DateTime + " : Please Enable USB Debugging" + Environment.NewLine);
                        }
                        else
                        {
                            rbtnStatus.IsChecked = true;
                            List<Dictionary<string, string>> deviceList = new();
                            List<string> devices = result.StandardOutput.Split(Environment.NewLine).ToList();
                            devices.RemoveAll(s => s == string.Empty);
                            devices.RemoveAt(0);
                            devices.ForEach(s =>
                            {
                                var (devID, devStatus) = (s.Split("\t")[0], s.Split("\t")[1]);
                                tbxLogs.AppendText($"DeviceID: {devID} - Status: {devStatus}{Environment.NewLine}");
                                cmbDevices.Items.Add($"DeviceID: {devID} - Status: {devStatus}");
                                deviceList.Add(new Dictionary<string, string>() { { "DeviceID", devID }, { "Status", devStatus } });
                            });
                        }
                        break;

                    default:
                        tbxLogs.AppendText("ERROR : " + result.ExitTime.DateTime + " :" + result.StandardError + Environment.NewLine);
                        break;
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda buraya düşer.
                tbxLogs.AppendText("ERROR : " + DateTime.Now + " :" + ex.Message + Environment.NewLine);
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
            //DeviceListing();
            try
            {
                var result = await Cli.Wrap(targetFilePath: "adb").WithArguments("reconnect offline").ExecuteBufferedAsync();//offline ve unauthorized aygıtları zorlar
                switch (result.ExitCode)
                {
                    case 0:
                        tbxLogs.AppendText(result.ExitTime.DateTime + " : Refresh android devices" + result.StandardOutput);
                        break;
                    case 1:
                        tbxLogs.AppendText("ERROR : " + result.ExitTime.DateTime + " : Invalid arguments provided." + Environment.NewLine);
                        break;
                    case 2:
                        tbxLogs.AppendText("ERROR : " + result.ExitTime.DateTime + " : File not found." + Environment.NewLine);
                        break;
                    default:
                        tbxLogs.AppendText("ERROR : " + result.ExitTime.DateTime + " :" + result.StandardError + Environment.NewLine);
                        break;
                }
            }
            catch (Exception ex)
            {
                tbxLogs.AppendText("ERROR : " + DateTime.Now + " :" + ex.Message + Environment.NewLine);
            }
            DeviceListing();
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
            invoker.AddCommand(new Command("adb", "devices"));
            invoker.AddCommand(new Command("adb", "getprop product"));
            invoker.AddCommand(new Command("adb", "shell dumpsys battery"));
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
