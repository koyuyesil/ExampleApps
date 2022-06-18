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

        private void btnGenerateLicence_Click(object sender, RoutedEventArgs e)
        {
            var passPhrase = pwdBox.Password;
            var keyGenerator = Standard.Licensing.Security.Cryptography.KeyGenerator.Create();
            var keyPair = keyGenerator.GenerateKeyPair();
            var privateKey = keyPair.ToEncryptedPrivateKeyString(passPhrase);
            var publicKey = keyPair.ToPublicKeyString();
            tbxPublicKey.Text = publicKey;
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
                .CreateAndSignWithPrivateKey(privateKey, passPhrase);
            #pragma warning restore CS8629 // Boş değer atanabilir değer türü null olabilir.
            using (System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create("Lisans.xml"))
            {
                stdlicense.Save(writer);
            }
            System.IO.File.WriteAllText("Lisans.lic", stdlicense.ToString(), System.Text.Encoding.UTF8);
            System.IO.File.WriteAllText("Public.Key", publicKey, System.Text.Encoding.UTF8);
        }
    }
}
