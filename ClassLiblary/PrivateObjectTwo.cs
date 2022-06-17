using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLiblary
{
    class PrivateObjectTwo
    {
        PrivateObjectTwo()
        {

        }
        //fieldler değikenlerden oluşur class yapısının içinde eklenir constuctor veya metodların içinde değildir.
        static string field1= "string tipinde bir field";
        int field2 = 5;
        static double field3 = 15.50;
        static float field4 = 15.50F;
        static int Propertyteksatir { get; set; }
        static private int myVar;
        static public int Propertytamsatir
        {
            get { return myVar; }
            set { myVar = value; }
        }


        public PrivateObjectTwo(int a)//constructor methodu
        {
            field1 = "field1";
        }

        public void Normalmethod()
        {

        }
    }
}
