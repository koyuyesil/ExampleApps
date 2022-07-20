using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

            plusMinus(arr);
        }

        private static void plusMinus(List<int> arr)
        {
            var array = arr;
            var len = arr.Count;
            double plus = 0;
            double minus = 0;
            double zeros = 0;

            foreach (var item in array)
            {
                if (item > 0) { plus++; }
                else if (item < 0) { minus++; }
                else { zeros++; }

            }
            Console.WriteLine((plus / len).ToString());
            Console.WriteLine((minus / len).ToString());
            Console.WriteLine((zeros / len).ToString());
            Console.ReadKey();

        }
    }
}

