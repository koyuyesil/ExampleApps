using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagonalDifference
{
    internal class Program
    {

        public static int diagonalDifference(List<List<int>> arr) => Math.Abs(arr.Select((line, index) => line[index] - line[line.Count - 1 - index]).Sum());
        static void Main(string[] args)
        {
            Console.WriteLine("Diagonal Difference");
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<List<int>> arr = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                arr.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList());
            }

            int result = diagonalDifference(arr);
            Console.WriteLine(result);
            Console.ReadKey();

        }
    }
}



