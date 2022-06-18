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
using Standard.Licensing;
using Standard.Licensing.Validation;


namespace LicensingActivation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bool fileExist = System.IO.File.Exists("Lisans.xml");
            if (fileExist)
            {
                CheckLicence();
            }
            else
            {
                MessageBox.Show("Lisans Dosyası Bulunamadı Program Kapatılıyor!", "Licence File Check");
                System.Windows.Application.Current.Shutdown();
            }
        }
        public void CheckLicence()
        {
            string xmlString = System.IO.File.ReadAllText("Lisans.lic");
            License license = License.Load(xmlString);
            var publicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE4KMRfXymlom/pQmuBjfxLBB+5zkOXuEKxsMXjjnRLSvUlB1X8zeHjMjzir6zZfqWl4IuZ4mGnczElbLH3erNHQ==";
            var validationFailures = license.Validate()
                                .ExpirationDate()
                                .When(lic => lic.Type == LicenseType.Trial)
                                .And()
                                .Signature(publicKey)
                                .AssertValidLicense();
            foreach (var failure in validationFailures) 
            { 
                MessageBox.Show(failure.GetType().Name + ": " + failure.Message + " - " + failure.HowToResolve,"License Error",MessageBoxButton.OK,MessageBoxImage.Error);
              
            }
            
            if (validationFailures.Any())
            {
                MessageBox.Show("Licence OK", "Standard Licensing OK", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
            else
            {
                MessageBox.Show("Licence Error", "Standard Licensing Error", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown();
            }


                

        }
    }
}
