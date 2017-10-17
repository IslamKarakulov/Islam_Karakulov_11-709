using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите длину первого отрезка: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите длину второго отрезка: ");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите длину третьего отрезка: ");
            int z = Convert.ToInt32(Console.ReadLine());
            if (x + y > z && x + z > y && y + z > x)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }
            Console.ReadKey();
        }
    }
}
