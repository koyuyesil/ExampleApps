
using Microsoft.VisualBasic.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows;
using CliWrap;
using CliWrap.Buffered;
using Serilog;
using Serilog.Events;
using System;

// Komut arayüzü
public interface ICommand
{
    Task<string> ExecuteAsync();
}

// Merhaba komutu sınıfı
public class MerhabaCommand : ICommand
{
    private readonly string _parameter;

    public MerhabaCommand(string parameter)
    {
        _parameter = parameter;
    }

    public async Task<string> ExecuteAsync()
    {
        var result = await Cli.Wrap("Merhaba")
            .WithArguments(_parameter)
            .ExecuteBufferedAsync();

        return result.StandardOutput;
    }
}

// Invoker sınıfı
public class CommandInvoker
{
    private readonly List<ICommand> _commands;

    public CommandInvoker()
    {
        _commands = new List<ICommand>();
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

            logBox.Document.Blocks.Add(message);
        }

        logBox.ScrollToEnd();
    }
}

public partial class Main : Window
{
    public Main()
    {
        InitializeComponent();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        var invoker = new CommandInvoker();

        invoker.AddCommand(new MerhabaCommand("Dünya!"));
        invoker.AddCommand(new MerhabaCommand("Mars!"));
        invoker.AddCommand(new MerhabaCommand("GPT!"));

        await invoker.RunAsync();
    }
    public async void GetVariables()
    {
        var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        var fastboot = await Cli.Wrap(targetFilePath: "fastboot").WithArguments("getvar battery-voltage").WithWorkingDirectory(path).ExecuteBufferedAsync();
        var currentVoltage = fastboot.StandardError.Split(':').ToList()[1].Split(new[] { "\r\n" }, StringSplitOptions.None)[0];


        if (double.Parse(currentVoltage) > 3600)
        {
            //double unitVoltage = 4450 - 3870;
            //double unitVoltage = 4000 - 3600;
            double unitVoltage = 4433 - 3600;//real
            unitVoltage = unitVoltage / 100;
            double diffVoltage = double.Parse(currentVoltage) - 3600;
            double res = diffVoltage / unitVoltage;
            battery = Convert.ToString(res);
        }
        else
        {
            battery = "info error";
        }




        //if (fastboot.ExitCode != 0)
        //{
        //    //tbxLogs.AppendText(fastboot.StandardError);
        //}
        //List<Dictionary<string, string>> propList = new List<Dictionary<string, string>>();
        //List<string> props = fastboot.StandardOutput.Split("\r\n").ToList();
        //props.RemoveAll(s => s == "");
        //devices.RemoveAt(0);//listofdevices string
        //props.ForEach(s => tbxLogs.AppendText(s + "\r\n"));
        //var pattern = @"\[(.*?)\]";
        //props.ForEach(s =>
        //{
        //    var matches = Regex.Matches(s, pattern);
        //    string s1 = matches[0].Groups[1].ToString();
        //    string s2 = matches[1].Groups[1].ToString();
        //    propList.Add(new Dictionary<string, string>() { { s1, s2 } });
        //});
    }
}