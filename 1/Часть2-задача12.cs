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
            Console.Write("Введите количество элементов в массиве: ");
            int n = Convert.ToInt32(Console.ReadLine());
            double[] array = new double[n];
            bool is_prev_neg;
            for (int i = 0; i < n; i++)
            {
                array[i] = Convert.ToInt32(Console.ReadLine());
            }
            if (array[0] < 0)
            {
                is_prev_neg = true;
            }
            else
            {
                is_prev_neg = false;
            }
            for (int i = 1; i <= n; i++)
            {
                if ((array[i]>0) && (is_prev_neg = true))
                {
                    is_prev_neg = false;
                    continue;
                }
                else if ((array[i]<0) && (is_prev_neg = false))
                {
                    is_prev_neg = true;
                    continue;
                }
            }
        }
    }
}
