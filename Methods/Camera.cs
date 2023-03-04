using System;
using System.Collections.Generic;
using System.Text;

namespace Methods
{
    public class Camera

    {
        public int xdef = 1920;
        public int ydef = 1080;

        public static void ResimKaydet()
        {
            Console.WriteLine("Fotoğraf Yakalandı");
            Console.Beep();


        }
        public static void VideoKaydet()
        {
            Console.WriteLine("Video Kaydediliyor");
            Console.Beep();
        }

        public static void VideoKaydiDurdur()
        {
            Console.WriteLine("Dosyaya Kaydediliyor");
            Console.Beep();
        }

    }
}
