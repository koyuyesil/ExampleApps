using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSolution
{

    public class LinqSolution
    {
        public static void countApplesAndOranges(int s, int t, int a, int b, List<int> apples, List<int> oranges)
        {
            var applesFallingOnHouse = 0;
            var orangesFallingOnHouse = 0;
            foreach (var distFromTreeForApple in apples)
            {
                var appleGroundPos = a + distFromTreeForApple;
                if (appleGroundPos >= s && appleGroundPos <= t)
                    ++applesFallingOnHouse;
            }

            foreach (var distFromTreeForOrange in oranges)
            {
                var orangeGroundPos = b + distFromTreeForOrange;
                if (orangeGroundPos >= s && orangeGroundPos <= t)
                    ++orangesFallingOnHouse;
            }

            Console.WriteLine(applesFallingOnHouse);
            Console.WriteLine(orangesFallingOnHouse);

        }
        public static Dictionary<string, int> AverageAgeForEachCompany(List<Employee> employees)
        {
            var totals = (
                from e in employees 
                group new { e.FirstName, e.LastName, e.Company, e.Age }
                by e.Company into g
                select
                (g.First().Company, avarage: g.Average(z => z.Age)))
                .ToDictionary(t => t.Company, t => Convert.ToInt32(t.avarage));
            return totals;

        }

        public static Dictionary<string, int> CountOfEmployeesForEachCompany(List<Employee> employees)
        {
            var totals = (
                from e in employees
                group new { e.FirstName, e.LastName, e.Company, e.Age }
                by e.Company into g
                select
                (g.First().Company, count: g.Count()))
                .ToDictionary(t => t.Company, t => Convert.ToInt32(t.count));
            return totals;
        }

        //public static Dictionary<string, Employee> OldestAgeForEachCompany(List<Employee> employees)
        //{
        //    var totals = (
        //        from e in employees
        //        group new { e.FirstName, e.LastName, e.Company, e.Age }
        //        by e.Company into g
        //        select
        //        (g.First().Company, oldest: g.Max().Age)).Max();
        //        ;
        //    return totals;
        //}

        public static void Main()
        {
#pragma warning disable CS8604 // Olası null başvuru bağımsız değişkeni.
            int countOfEmployees = int.Parse(Console.ReadLine());
#pragma warning restore CS8604 // Olası null başvuru bağımsız değişkeni.

            var employees = new List<Employee>();

            for (int i = 0; i < countOfEmployees; i++)
            {
#pragma warning disable CS8600 // Null sabit değeri veya olası null değeri, boş değer atanamaz türe dönüştürülüyor.
                string str = Console.ReadLine();
#pragma warning restore CS8600 // Null sabit değeri veya olası null değeri, boş değer atanamaz türe dönüştürülüyor.
#pragma warning disable CS8602 // Olası bir null başvurunun başvurma işlemi.
                string[] strArr = str.Split(' ');
#pragma warning restore CS8602 // Olası bir null başvurunun başvurma işlemi.
                employees.Add(new Employee
                {
                    FirstName = strArr[0],
                    LastName = strArr[1],
                    Company = strArr[2],
                    Age = int.Parse(strArr[3])
                });
            }

            foreach (var emp in AverageAgeForEachCompany(employees))
            {
                Console.WriteLine($"The average age for company {emp.Key} is {emp.Value}");
            }

            foreach (var emp in CountOfEmployeesForEachCompany(employees))
            {
                Console.WriteLine($"The count of employees for company {emp.Key} is {emp.Value}");
            }

            //foreach (var emp in OldestAgeForEachCompany(employees))
            //{
            //    //Console.WriteLine($"The oldest employee of company {emp.Key} is {emp.Value.FirstName} {emp.Value.LastName} having age {emp.Value.Age}");
            //}
        }
    }

    public class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Company { get; set; }
    }
}