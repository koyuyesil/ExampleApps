using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFUygulamasi
{
    class Sinif
    {
        void islev()
        {
            //geri donus tipi yok(void) ve erişim belirteci yok ise private sayılır. private void yazılabilir
        }


      public Sinif()//sınıf ile aynı isimli methodlar constuructor methoddur sınıf üretilince çalışır.
        {
            MessageBox.Show("Merhaba Bu bir contsructordür sınıf oluşturulduğunda çalıştırılır");
            //Sinif isim= new Sinif();

            Sinif.Topla(1,1);//private methodlar sadece kendi sınıfının constuructorlerinden birinde çalışabilir.
            //topla metodu geriye 2 değeri döndürecek ama çöp olarak kalacak. garbaga collection temizledi.
            int a = Topla(1,1); //burada ise topla metodundan döndürülen değer bir değişkene atandı.
            //constructorlerin geri dönüş tipi olmaz
        }

      public Sinif(string ad, string soyad)//aynı methodun farklı parametrelerle oluşturulması method aşırı yuklemesi denir.
        {
            MessageBox.Show("Merhaba :"+ ad + soyad +" eğer constructor methodu aşırı yuklemesine göre oluşturulduğunda çalışır");
            //Sinif isim= new Sinif("örsan","akciyer");
        }


       public static void Statikmethod()
        {
            MessageBox.Show("Statik method çalıştırıldı");
            //Statikmethod(); kendi sınıfında çalıştırırken
            //Sinif.Statikmethod(); aynı namespace de başka sınıflardan çalıştırırken
        }

       public void OrnekMethod()
        {
            MessageBox.Show("Nesneden üretilen method çalıştırıldı");
            //Sinif isim= new Sinif();
            //isim.method();
        }

        //2 parametreli geriye değer döndüren method
        private static int Topla(int a, int b)
        {
            return a + b;
        }

    }
}

