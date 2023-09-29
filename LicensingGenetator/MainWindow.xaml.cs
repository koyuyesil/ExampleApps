using Standard.Licensing.Security.Cryptography;
using System;
using System.Collections.Generic;
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

namespace LicensingGenetator
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
        
        string privateKey = "MHcwIwYKKoZIhvcNAQwBAzAVBBCxjANukVUV2PK1AEJ4dAktAgEKBFD3/leCdn1GFXj4/0BD8PQqt3elaAn+Yc/NcCwqeMRYDBkVXBSpx31SGd4yIBh2oIqAUOV0qwwVa52caadkEFWc1lSf0Kvorgam7DaKo1BmMA==";
        string publicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE+aiebBbj+8caQtC5ZH4ynlEq93+Re2MSszc+QdrNxcwMY57WtQE+hDxFtmqdceru4rEytCTBswBokObaBamCdg==";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbxPrivateKey.Text = privateKey;
            tbxPublicKey.Text = publicKey;
            //GenerateKeys();
        }

        private void GenerateKeys()
        {
            passPhrase.Password = "NebulaApps";
            var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase.Password);
            publicKey = keyPair.ToPublicKeyString();
            tbxPrivateKey.Text = privateKey;
            tbxPublicKey.Text = publicKey;
        }

        private void btnGenerateLicence_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS8629 // Boş değer atanabilir değer türü null olabilir.

            var stdlicense = Standard.Licensing.License.New()
                .WithUniqueIdentifier(Guid.NewGuid())
                .As(Standard.Licensing.LicenseType.Standard)
                .ExpiresAt(DateTime.Now.AddDays(30))
                .WithMaximumUtilization(5)
                .WithProductFeatures(new Dictionary<string, string>
                {
                    {"Qualcomm Module", ((bool)cbxQualcomm.IsChecked) ? "true" : "false"},
                    {"Mediatek Module", ((bool)cbxMediatek.IsChecked) ? "true" : "false"},
                    {"Repair Module", ((bool)cbxRepair.IsChecked) ? "true" : "false"}
                })
                .LicensedTo(tbxFullName.Text, tbxEmail.Text)
                .CreateAndSignWithPrivateKey(privateKey, passPhrase.Password);
#pragma warning restore CS8629 // Boş değer atanabilir değer türü null olabilir.
            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create("Lisans.xml"))
            {
                stdlicense.Save(writer);
            }
            System.IO.File.WriteAllText("Lisans.lic", stdlicense.ToString(), System.Text.Encoding.UTF8);
            System.IO.File.WriteAllText("Public.Key", publicKey, System.Text.Encoding.UTF8);
        }

        private void btnGenerateKeys_Click(object sender, RoutedEventArgs e)
        {
            GenerateKeys();
        }
    }
}
