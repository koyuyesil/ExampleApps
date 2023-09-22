using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Collections;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace CertRenamer
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ArrayList yeniliste = new ArrayList();
            List<Cert> certlist = new List<Cert>();
            Cert c1 = new Cert();
            c1.IMEI = "131321316";
            c1.IMEI2 = "ASDASDASD";

            Cert c2 = new Cert();
            c2.IMEI = "131321316";
            c2.IMEI2 = "SDADDASD";

            Cert c3 = new Cert();
            c3.IMEI = "C31";
            c3.IMEI2 = "C32";

            Cert c4 = new Cert();
            c4.IMEI = "C41";
            c4.IMEI2 = "C42";

            yeniliste.Add(c1);
            yeniliste.Add(c2);
            certlist.Add(c1);
            certlist.Add(c2);
            certlist.Add(c3);
            certlist.Add(c4);
          //  yeniliste2[0].Yazdir();


            foreach (Cert a in yeniliste)
            {
                Cert cert = a;
                Console.WriteLine(cert.IMEI);
                Console.WriteLine(cert.IMEI2);
                

            }

         

           
         }











        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //klasör seçme foms kütüphanesine referans zorunlu çünkü wpf projesi
            //var dialog = new System.Windows.Forms.FolderBrowserDialog();
            //System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            //txtgozat.Text = dialog.SelectedPath;
            

            //tek tek çoklu dosya seçme
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Dosyaismi isimal = new Dosyaismi();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Cert|*.cert|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); 
            openFileDialog.InitialDirectory = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Samples");
            openFileDialog.RestoreDirectory = true;
            if (openFileDialog.ShowDialog() == true)
            {
                txtgozat.Text=System.IO.Path.GetDirectoryName(openFileDialog.FileName);//kullanımı doğru mu bilmiyorum çoklu seçimde
                foreach (string filename in openFileDialog.FileNames)
                {
                   dg.ItemsSource = isimal.ReadCertFile(filename);                   
                    //dosya yolunu yazar
                    //listbox.Items.Add(filename);
                    //dosya adını filtreler
                    //listbox.Items.Add(System.IO.Path.GetFileName(filename));
                }     
            }

            ////tek dosya seçme içeriğini okuma
            //OpenFileDialog openFileDialog2 = new OpenFileDialog();
            //openFileDialog2.Filter = "Cert|*.cert|All files|*.*";
            //openFileDialog2.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //if (openFileDialog2.ShowDialog() == true)
            //{
            //    listbox.Items.Add(File.ReadAllText(openFileDialog2.FileName));               
            //}
 

        }
    }
}
