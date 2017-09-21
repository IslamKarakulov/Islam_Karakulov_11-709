using System;


namespace ConsoleApp9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите 4-х зачное число: ");
            int n = int.Parse(Console.ReadLine());
            int a = n % 10 + n / 1000 + n / 100 % 10 + n / 10 % 10;
            Console.WriteLine(a);
            Console.ReadKey();
        }   
    }
}
