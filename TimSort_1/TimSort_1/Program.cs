using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimSort
{
    class Program
    {
        public static int MinRun(List<int>Array)
        {
            int result = 1;
            if (Array[0] > Array[1])
            {
                for (int i = 0; i < Array.Count - 1; i++)
                {
                    if (Array[i] > Array[i + 1])
                    {
                        result++;
                        continue;
                    }
                    break;
                }
                Array.Reverse(0, result);
            }
            else
            {
                for (int i = 0; i < Array.Count - 1; i++)
                {
                    if (Array[i] < Array[i + 1])
                    {
                        result++;
                        continue;
                    }
                    break;
                }
            }  
            return result;
        }
        public static void InsertSort(List<int>Array, int FirstIndex)
        {
            for (int i = FirstIndex; i < Array.Count; i++) 
            {
                for (int j = i; j >= 0; j--) 
                {
                    if (Array[i] <= Array[j])
                    {
                        if (j == 0)
                        {
                            Array.Insert(j, Array[i]);
                            Array.RemoveAt(i + 1);
                            break;
                        }
                        else
                            continue;
                    }
                    else
                    {
                        Array.Insert(j + 1, Array[i]);
                        Array.RemoveAt(i + 1);
                        break;
                    }
                }
            }
        }
        public static void FirstStep(List<int> Array, List<List<int>> Result)
        {
            int cur = 2;
            Result.Capacity = (int)Math.Log(Array.Count, 2);
            if ((int)Math.Log(Array.Count, 2) < (float)Math.Log(Array.Count, 2))
                Result.Capacity++;
            for (int i = 0; i < Result.Capacity; i++)
            {
                Result.Add(new List<int>());
                Result[i].Capacity = (int)Math.Pow(2, i);
            }
            if ((int)Math.Log(Array.Count, 2) < (float)Math.Log(Array.Count, 2))
                Result[Result.Count - 1].Capacity = Array.Count - (int)Math.Pow(2, Result.Count - 1);

            Result[0].Add(Array[0]);
            Result[0].Add(Array[1]);
            for (int i = 1; i < Result.Capacity; i++)
            {
                for (int j = 0; j < Result[i].Capacity; j++)
                {
                    Result[i].Add(Array[cur]);
                    if (cur + 1 != Array.Count)
                        cur++;
                }
            }
           
            for (int i = 0; i < Result.Count; i++)
            {
                InsertSort(Result[i], MinRun(Result[i]));
            }
        }
        public static List<int> Merge(List<int>a,List<int>b)
        {
            int i = 0, j = 0;
            List<int> Result = new List<int>();
            while (i < a.Count && j < b.Count) 
            {
                if (a[i] <= b[j])
                {
                    Result.Add(a[i]);
                    i++;
                }
                else
                {
                    Result.Add(b[j]);
                    j++;
                }
                
            }
            if(i == a.Count && j != b.Count)
            {
                for (; j < b.Count; j++)
                    Result.Add(b[j]);
            }
            else if(i != a.Count && j == b.Count)
            {
                for (; i < a.Count; i++)
                    Result.Add(a[i]);
            }
            return Result;
        }
        public static List<int> SecondStep(List<List<int>>Array)
        {
            List<int> Result = new List<int>();
            Result = Array[0];
            for(int i = 1;i<Array.Count;i++)
            {
                Result = Merge(Result, Array[i]);
            }
            return Result;
        }
        public static List<int> TimSort(List <int> array)
        {
            if (array.Count <= 1)
                return array;
            List<List<int>> Result = new List<List<int>>();
            FirstStep(array, Result);
            return SecondStep(Result);
        }
        static void Main(string[] args)
        {
            //Random rand = new Random();
            List<int> Array = new List<int>();

            Console.Write("Введите количество чисел: ");
            int n = Convert.ToInt32(Console.ReadLine());

            for (int a = 0; a < n; a++)
                Array.Add(Convert.ToInt32(Console.ReadLine()));
            //for (int a = 0; a <1; a++)
            //    Array.Add(rand.Next(0, 101));

            var b = DateTime.Now;
            Array = TimSort(Array);
            Console.WriteLine(DateTime.Now - b);

            Console.WriteLine("");
            foreach(int a in Array)
                Console.WriteLine(a);
            int i = 0;
            for(;i<Array.Count - 1;i++)
            {
                if (Array[i] <= Array[i + 1])
                    continue;
                else
                {
                    Console.WriteLine("false");
                    break;
                }
            }
            if (i == Array.Count - 1)
            {
                Console.WriteLine("true");
                Console.ReadKey();
            }
        }
    }
}
