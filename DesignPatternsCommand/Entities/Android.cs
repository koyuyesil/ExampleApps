using CliWrap;
using CliWrap.Buffered;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatternsCommand.Entities
{
    public class Android
    {
        private int channel;
        private string id;
        private string battery = "";
        private AndroidStatus state = AndroidStatus.offline;
        private Dictionary<string, string> properties;
        private Dictionary<string, string> variables;
        enum AndroidStatus
        {
            unauthorized, device, recovery, sideload, fastboot, offline
        }

        public Android()
        {

        }

        public void GetInfo()
        {

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

        public async void Reboot()
        {
            try
            {
                var path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var fastboot = await Cli.Wrap(targetFilePath: "fastboot").WithArguments("reboot bootloader").WithWorkingDirectory(path).ExecuteBufferedAsync();
                var currentVoltage = fastboot.StandardOutput;
                Console.WriteLine(DateTime.Now.ToLongTimeString()+" : Reboot Bootloader Started");
                Thread.Sleep(10000);
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " : Reboot Bootloader Finished");//result bolean olacak. command için

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void RebootRecovery()
        {

        }

        public void RebootBootloader()
        {

        }
        public string GetStatus() { return battery; 
        }
    }
}