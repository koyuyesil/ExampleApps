using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SockMerchant
{
    internal class Program
    {
        static int sockMerchant(int[] socksPile)
        {
            List<int> a = new List<int>() ;
            var pairsFound = 0;
            var sockColorHash = new Dictionary<int, int>();

            foreach (var sock in socksPile)
            {
                if (sockColorHash.ContainsKey(sock))
                {
                    pairsFound++;
                    sockColorHash.Remove(sock);
                }
                else
                    sockColorHash.Add(sock, 1);
            }
            return pairsFound;
        }
        static void Main(string[] args)
        {
            
            var ar_temp = Console.ReadLine().Split(' ');
            var ar = Array.ConvertAll(ar_temp, Int32.Parse);
            var result = sockMerchant(ar);
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
