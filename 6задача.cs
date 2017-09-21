using System;


namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите год: ");
            int n = int.Parse(Console.ReadLine());
            if (n % 4 == 0) Console.WriteLine("366 дней");
            else Console.WriteLine("365 дней");
            Console.ReadKey();
        }
    }
    
}
