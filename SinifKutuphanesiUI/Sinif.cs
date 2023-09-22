using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SinifKutuphanesiUI
{
    class Sinif
    {
        public Sinif()//sınıf ile aynı isimli methodlar constuructor methoddur sınıf üretilince çalışır.
        {
            //constructorlerin geri dönüş tipi olmaz
            MessageBox.Show("Merhaba Bu bir contsructordür sınıf oluşturulduğunda çalıştırılır");
            //Sinif isim= new Sinif();

            Sinif.Topla(1, 1);//private methodlar sadece kendi sınıfının constuructorlerinden birinde çalışabilir.
            //topla metodu geriye 2 değeri döndürecek ama çöp olarak kalacak. garbaga collection temizledi.
            int a = Topla(1, 1); //burada ise topla metodundan döndürülen değer bir değişkene atandı.
            

        }

        public Sinif(string ad, string soyad)//aynı methodun farklı parametrelerle oluşturulması method aşırı yuklemesi denir.
        {
            MessageBox.Show("Merhaba :" + ad + soyad + " eğer constructor methodu aşırı yuklemesine göre oluşturulduğunda çalışır");
            //Sinif isim= new Sinif("örsan","akciyer");
        }

        private void Method() //private method sadece içerden erişilir
        {
            //geri donus tipi yok(void) ve erişim belirteci yok ise private sayılır. private void yazılabilir
            MessageBox.Show("Nesneden üretilen private method çalıştırıldı");
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
        public void OrnekMethod(bool b)
        {
            if (b)
            {
                MessageBox.Show("Nesneden üretilen overload method çalıştırıldı");
                //Sinif isim= new Sinif();
                //isim.method();
            }

        }

        //2 parametreli geriye değer döndüren method
        private static int Topla(int a, int b)
        {
            return a + b;
        }

    }
}

