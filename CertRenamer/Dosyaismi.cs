using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace CertRenamer
{
    class Dosyaismi
    {
              
        public string Name { get; set; }
        public string Model { get; set; }
        public string SerNo { get; set; }
        public string IMEI { get; set; }
        public string ImeiSign { get; set; }
        public string PubKey { get; set; }
        public string PubKeySign { get; set; }
        public string IMEI2 { get; set; }
        public string ImeiSign2 { get; set; }
        public string PubKey2 { get; set; }
        public string PubKeySign2 { get; set; }

        public List<Dosyaismi> ReadCertFile(string path)
        {
            var users = new List<Dosyaismi>();

            var dict = File.ReadAllLines(path).Where(l => !l.Contains(";") && !l.Contains("["))
                .Select(l => l.Split(new[] { '=' }))
                .ToDictionary(s => s[0].Trim(), s => s[1].Trim());  // read the entire file into a dictionary.

            MessageBox.Show(dict.ContainsKey("Model") ? dict["Model"] : "Not Found");
            users.Add(new Dosyaismi
            {
                Model = dict.ContainsKey("Model") ? dict["Model"] : "Not Found",
                SerNo = dict.ContainsKey("SerNo") ? dict["SerNo"] : "Not Found",
                IMEI = dict.ContainsKey("IMEI") ? dict["IMEI"] : "Not Found",
                IMEI2 = dict.ContainsKey("IMEI2") ? dict["IMEI2"] : "Not Found",
                ImeiSign = dict.ContainsKey("ImeiSign") ? dict["ImeiSign"] : "Not Found",
                ImeiSign2 = dict.ContainsKey("ImeiSign2") ? dict["ImeiSign2"] : "Not Found",
                PubKey = dict.ContainsKey("PubKey") ? dict["PubKey"] : "Not Found",
                PubKey2 = dict.ContainsKey("PubKey2") ? dict["PubKey2"] : "Not Found",
                PubKeySign = dict.ContainsKey("PubKeySign") ? dict["PubKeySign"] : "Not Found",
                PubKeySign2 = dict.ContainsKey("PubKeySign2") ? dict["PubKeySign2"] : "Not Found"

            });
            //tekdüze satırları da bu kısımdan okuyordu şimdi okumuyor.            
        return users;
        }
        

    }
}
