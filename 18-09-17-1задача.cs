using System;


namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите х: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите y: ");
            int y = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ответ: ");
            Console.WriteLine(2 * x + 134 * x - x * x);
            Console.ReadKey();
        }
    }
}
