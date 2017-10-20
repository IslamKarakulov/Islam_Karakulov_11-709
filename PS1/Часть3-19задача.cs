using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Часть3_19задача
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = Convert.ToInt32(Console.ReadLine());
            int b;
            while (a != 0) 
            {
                if (a < 0)
                {
                    Console.WriteLine(a * (-1));
                }
                else
                {
                    b = 1;
                    while (b < a)
                    {
                        b = b * 2;
                    }
                    Console.WriteLine(" " + b);
                }
                a = Convert.ToInt32(Console.ReadLine());
            }
        }
    }
}
