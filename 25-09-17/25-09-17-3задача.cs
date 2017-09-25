using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество элементов в массиве:");
            int n = Convert.ToInt32(Console.ReadLine());
            int plus = 0;
            int minus = 0;
            int[] array = new int[n];
            for (int i = 0; i < n; i++)
            {
                array[i] = int.Parse(Console.ReadLine());
            }
            for (int i = 0; i <n; i++)
            {
                if (array[i] > 0)
                {
                    plus++;
                }
                else if (array[i] < 0) 
                {
                    minus ++;
                }
            }
            Console.WriteLine("Положительных: " + plus + ", отрицательных: " + minus);
            Console.ReadKey();
        }
    }
}
