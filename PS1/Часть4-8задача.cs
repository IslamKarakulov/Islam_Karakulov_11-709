using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Часть4_8задача
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            for (int i = 1000; i < 9999; i++)
            {
                int a = i % 10;
                int b = i / 10 % 10;
                int c = i / 100 % 10;
                int d = i / 1000;
                if (i == Math.Pow(a, 4) + Math.Pow(b, 4) + Math.Pow(c, 4) + Math.Pow(d, 4))
                {
                    sum = sum + i;
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine(sum);
            Console.ReadKey();
        }
    }
}
