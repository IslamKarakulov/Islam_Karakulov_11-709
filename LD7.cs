using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace LD7
{
    class Program
    {
        static void Main(string[] args)
        {
            string first;
            string second;
            while (true)
            {
                first = Console.ReadLine();
                second = Console.ReadLine();
                if (first.Length == second.Length)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите строки одинаковой длины!");
                }
            }
            Console.WriteLine(MaxSubstring(first,second));
            Console.ReadKey();
        }

        public static string MaxSubstring(string first, string second)
        {
            if (first.Length == 0)
            {
                return "0";
            }
            var a = new int[first.Length + 1, second.Length + 1];
            var v = 0;
            var u = 0;
            for (var i = 0; i < first.Length; i++)
            {
                for (var j = 0; j < second.Length; j++)
                {
                    if (first[i] == second[j])
                    {
                        a[i + 1, j + 1] = a[i, j] + 1;
                        if (a[i + 1, j + 1] > a[u, v]) 
                        {
                            u = i + 1;
                            v = j + 1;
                        }
                    }
                }
            }
            return first.Substring(u - a[u, v], a[u, v]);
        }
    }
}
