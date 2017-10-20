using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Часть4_18задача
{
    class Program
    {
        static void Main(string[] args)
        {
            int k = Convert.ToInt32(Console.ReadLine());
            int a = 0;
            int b = 0;
            int c = 0;
            while (k != b)
            {
                a++;
                c = 0;
                for (int i = 1; i <= a; i++)
                {
                    if (a % i == 0)
                    {
                        c++;
                    }
                }
                if (c == 2)
                {
                    b++;
                }
            }
            Console.WriteLine(a);
            Console.ReadKey();
        }
    }
}
