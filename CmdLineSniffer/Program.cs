using System;
using System.Diagnostics;
using System.IO;

namespace CmdLineSniffer
{
    class Program
    {
        /// <summary>
        /// SON OLARAK ÇIKTININ AYARLANMASI GEREKLİ
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //var logPath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName + "Log.txt";
            //var exePath = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName + "Org.exe";
            var logPath = Environment.CurrentDirectory + "\\" + Path.GetFileNameWithoutExtension(Environment.ProcessPath) + "Log.txt";
            var exePath = Environment.CurrentDirectory + "\\" + Path.GetFileNameWithoutExtension(Environment.ProcessPath) + "Org.exe";
            var s1 = Path.GetFileNameWithoutExtension(Environment.ProcessPath);
            var s2 = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var s3 = Environment.CurrentDirectory + "\\" + Process.GetCurrentProcess().ProcessName;

            //runner();
            StreamWriter sw;
            // This text is added only once to the file.
            if (!File.Exists(logPath))
            {
                // Create a file to write to.
                using (sw = (StreamWriter)File.CreateText(logPath))
                {
                    sw.WriteLine("Orjinal Dosyanın adını \"" + s1 + "Org\" olarak değiştirin");
                }
                using (sw = (StreamWriter)File.AppendText(logPath))

                {
                    sw.WriteLine(string.Join(" ", args));
                    Console.WriteLine();
                    Process.Start(exePath, args);
                }
            }
            else
            {
                using (sw = (StreamWriter)File.AppendText(logPath))

                {
                    sw.WriteLine(string.Join(" ", args));
                    Console.WriteLine();
                    Process.Start(exePath, args);
                }
            }
        }

        private static int runner()
        {
            Process process = new Process();
            process.StartInfo.FileName = @"C:\Program Files (x86)\Google\Chrome\Application\chrome.exe";
            process.StartInfo.Arguments = "google.com" + " --new-window";
            process.Start();
            return process.ExitCode;
        }
    }
}
