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
            Console.Write("Введите значение первого члена арифмитической прогрессии: ");
            int first = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите значение второго члена арифмитической прогрессии: ");
            int second = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите номер члена арифмитической прогрессии, который нужно найти: ");
            int k = Convert.ToInt32(Console.ReadLine());
            int difference = second - first;
            int answer = first + (k - 1) * difference;
            Console.WriteLine("Ответ: " + answer);
            Console.ReadKey();
        }
    }
}
