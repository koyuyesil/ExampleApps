using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            var a=GetUrlContentLengthAsync();
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
            var publicKey = "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE+aiebBbj+8caQtC5ZH4ynlEq93+Re2MSszc+QdrNxcwMY57WtQE+hDxFtmqdceru4rEytCTBswBokObaBamCdg==";
            var validationFailures = license.Validate()
                                .ExpirationDate()
                                .When(lic => lic.Type == LicenseType.Standard)
                                .And()
                                .Signature(publicKey)
                                .AssertValidLicense();
            foreach (var failure in validationFailures) 
            { 
                MessageBox.Show(failure.GetType().Name + ": " + failure.Message + " - " + failure.HowToResolve,"License Error",MessageBoxButton.OK,MessageBoxImage.Error);  
            };
            
            if (validationFailures.Any())
            {
                MessageBox.Show("Licence Error", "Standard Licensing Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Dispatcher.Invoke(Application.Current.Shutdown);
            }
            else
            {
                MessageBox.Show("Licence OK", "Standard Licensing OK", MessageBoxButton.OK, MessageBoxImage.Hand);
            };


                

        }




        public async Task<int> GetUrlContentLengthAsync()
        {
            var client = new HttpClient();

            Task<string> getStringTask =
                client.GetStringAsync("https://docs.microsoft.com/dotnet");

            DoIndependentWork();

            string contents = await getStringTask;

            return contents.Length;
        }

        void DoIndependentWork()
        {
            Console.WriteLine("Working...");
        }





    }
}
