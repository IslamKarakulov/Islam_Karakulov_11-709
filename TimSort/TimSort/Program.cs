using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace TimSort
{
    class IntComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (x < y) return -1;
            if (x > y) return 1;
            return 0;
        }

        public static void Main(string[] args)
        {
            int[] array = {23, 34, 56, 234, -67, -567, 45, 3, 0};
            var b = DateTime.Now;
            TimSort<int>.Sort(array, new IntComparer());
            var a = DateTime.Now - b;
            Console.WriteLine(a);
            foreach (var x in array)
            {
                Console.Write(x + " ");
            }
        }
    }
}