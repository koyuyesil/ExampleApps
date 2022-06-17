using System;
using System.IO;

namespace CmdLineGrabber
{
    class Program
    {
        static string path;
        static void Main(string[] args)
        {
            path = AppDomain.CurrentDomain.BaseDirectory + " " + "sniffer.txt";
            //path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            StreamWriter sw;
            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (sw = (StreamWriter)File.CreateText(path))
                {
                    sw.WriteLine(string.Join(" ", args));
                }
            }
            else
            {
                using (sw = (StreamWriter)File.AppendText(path))

                {
                    sw.WriteLine(string.Join(" ", args));
                }
            }
        }
    }
}
