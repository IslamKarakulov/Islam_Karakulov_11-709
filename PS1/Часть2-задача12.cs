using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp16
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Введите первое число: ");
            int first = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите систему счисления, в которую хотите перевести первое число: ");
            int k = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите второе число: ");
            int second = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите систему счисления, в которую хотите перевести второе число: ");
            int m = Convert.ToInt32(Console.ReadLine());
            int x = 0;
            int y = 0;
            int i = 1;
            while (first > 0)
            {
                x += (first % k);
                first = first / 0b10;
            }
            Console.WriteLine(x.Reverse().ToArray());
            i = 1;
            while (second > 0)
            {
                y += (second % m);
                second = second / 0b10;
            }
            Console.WriteLine(y.Reverse().ToArray());
            Console.ReadKey();
        }
    }
}