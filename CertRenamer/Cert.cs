using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CertRenamer
{
    class Cert
    {
        public static string Name { get; set; }
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

        public Cert ReadFromFile(string path)
        {
            Cert cert = new Cert();


            return cert;
        }

        public void WriteCert(Cert cert)
        {
         //dosyaya cert yaz
        }



    }
}
