using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
 
namespace TimSort
{
    /// <typeparam name="T">sort type</typeparam>
    public class TimSort<T> //: IEnumerable<T>
    {
      
       
        private const int MinMerge = 32;
       
        private T[] a;
        
        private IComparer<T> c;
 
       
        private const int MinGallop = 7;
      
        private int _minGallop = MinGallop;
       
        private const int InitialTmpStorageLength = 256;
        
        private T[] _tmp; // Actual runtime type will be Object[], regardless of T
 
       
        private int _stackSize = 0;
        private int[] runBase;
        private int[] runLen;
        
        private TimSort(T[] a, IComparer<T> c)
        {
            this.a = a;
            this.c = c;
 
            
            var len = a.Length;
            var newArray = (T[])new T[len < 2 * InitialTmpStorageLength ?
                                            len >> 1 : InitialTmpStorageLength];
            _tmp = newArray;
 
            int stackLen = (len < 120 ? 5 :
                            len < 1542 ? 10 :
                            len < 119151 ? 19 : 40);
            runBase = new int[stackLen];
            runLen = new int[stackLen];
 
        }
        public static void Sort(T[] a, IComparer<T> c)
        {
            Sort(a, 0, a.Length, c);
        }
        public static void Sort(T[] a, int lo, int hi, IComparer<T> c)
        {
            if (c == null)
            {
                var work = a.ToList<T>();
                work.Sort();
                a = work.ToArray<T>();
                return;
            }
 
            rangeCheck(a.Length, lo, hi);
            int nRemaining = hi - lo;
            if (nRemaining < 2)
                return;  // Arrays of size 0 and 1 are always sorted
 
            if (nRemaining < MinMerge)
            {
                int initRunLen = countRunAndMakeAscending(a, lo, hi, c);
                BinarySort(a, lo, hi, lo + initRunLen, c);
                return;
            }
            var ts = new TimSort<T>(a, c);
            int minRun = minRunLength(nRemaining);
            do
            {
                int runLen = countRunAndMakeAscending(a, lo, hi, c);
 
                if (runLen < minRun)
                {
                    int force = nRemaining <= minRun ? nRemaining : minRun;
                    BinarySort(a, lo, lo + force, lo + runLen, c);
                    runLen = force;
                }
 
                ts.pushRun(lo, runLen);
                ts.mergeCollapse();
 
                lo += runLen;
                nRemaining -= runLen;
            } while (nRemaining != 0);
 
            Debug.Assert(lo == hi);
            ts.mergeForceCollapse();
            Debug.Assert(ts._stackSize == 1);
        }
        private static void BinarySort(T[] a, int lo, int hi, int start,
                                           IComparer<T> c)
        {
            Debug.Assert(lo <= start && start <= hi);
            if (start == lo)
                start++;
            for (; start < hi; start++)
            {
                var pivot = a[start];
 
                int left = lo;
                int right = start;
                Debug.Assert(left <= right);
                while (left < right)
                {
                    //int mid = (left + right) >>> 1;
                    int mid = (left + right) >> 1;
                    if (c.Compare(pivot, a[mid]) < 0)
                        right = mid;
                    else
                        left = mid + 1;
                }
                Debug.Assert(left == right);
                int n = start - left;  // The number of elements to move
                switch (n)
                {
                    case 2:
                        a[left + 2] = a[left + 1];
                        goto case 1;
                    case 1:
                        a[left + 1] = a[left];
                        break;
                    default:
                        Array.Copy(a, left, a, left + 1, n);
                        break;
                }
                a[left] = pivot;
            }
        }
 
        private static int countRunAndMakeAscending(T[] a, int lo, int hi,
                                                        IComparer<T> c)
        {
            Debug.Assert(lo < hi);
            int runHi = lo + 1;
            if (runHi == hi)
                return 1;
 
            if (c.Compare(a[runHi++], a[lo]) < 0)
            { // Descending
                while (runHi < hi && c.Compare(a[runHi], a[runHi - 1]) < 0)
                    runHi++;
                reverseRange(a, lo, runHi);
            }
            else
            {                              // Ascending
                while (runHi < hi && c.Compare(a[runHi], a[runHi - 1]) >= 0)
                    runHi++;
            }
 
            return runHi - lo;
        }
        private static void reverseRange(T[] a, int lo, int hi)
        {
            hi--;
            while (lo < hi)
            {
                var t = a[lo];
                a[lo++] = a[hi];
                a[hi--] = t;
            }
        }
        private static int minRunLength(int n)
        {
            Debug.Assert(n >= 0);
            int r = 0;      // Becomes 1 if any 1 bits are shifted off
            while (n >= MinMerge)
            {
                r |= (n & 1);
                n >>= 1;
            }
            return n + r;
        }
        private void pushRun(int runBase, int runLen)
        {
            this.runBase[_stackSize] = runBase;
            this.runLen[_stackSize] = runLen;
            _stackSize++;
        }
        private void mergeCollapse()
        {
            while (_stackSize > 1)
            {
                int n = _stackSize - 2;
                if (n > 0 && runLen[n - 1] <= runLen[n] + runLen[n + 1])
                {
                    if (runLen[n - 1] < runLen[n + 1])
                        n--;
                    mergeAt(n);
                }
                else if (runLen[n] <= runLen[n + 1])
                {
                    mergeAt(n);
                }
                else
                {
                    break; // Invariant is established
                }
            }
        }
        private void mergeForceCollapse()
        {
            while (_stackSize > 1)
            {
                int n = _stackSize - 2;
                if (n > 0 && runLen[n - 1] < runLen[n + 1])
                    n--;
                mergeAt(n);
            }
        }
 
        private void mergeAt(int i)
        {
            Debug.Assert(_stackSize >= 2);
            Debug.Assert(i >= 0);
            Debug.Assert(i == _stackSize - 2 || i == _stackSize - 3);
 
            int base1 = runBase[i];
            int len1 = runLen[i];
            int base2 = runBase[i + 1];
            int len2 = runLen[i + 1];
            Debug.Assert(len1 > 0 && len2 > 0);
            Debug.Assert(base1 + len1 == base2);
 
            runLen[i] = len1 + len2;
            if (i == _stackSize - 3)
            {
                runBase[i + 1] = runBase[i + 2];
                runLen[i + 1] = runLen[i + 2];
            }
            _stackSize--;
            int k = gallopRight(a[base2], a, base1, len1, 0, c);
            Debug.Assert(k >= 0);
            base1 += k;
            len1 -= k;
            if (len1 == 0)
                return;
 
            len2 = gallopLeft(a[base1 + len1 - 1], a, base2, len2, len2 - 1, c);
            Debug.Assert(len2 >= 0);
            if (len2 == 0)
                return;
 
            if (len1 <= len2)
                mergeLo(base1, len1, base2, len2);
            else
                mergeHi(base1, len1, base2, len2);
        }
 
        private static int gallopLeft(T key, T[] a, int basei, int len, int hint,
                                          IComparer<T> c)
        {
            Debug.Assert(len > 0 && hint >= 0 && hint < len);
            int lastOfs = 0;
            int ofs = 1;
            if (c.Compare(key, a[basei + hint]) > 0)
            {
                int maxOfs = len - hint;
                while (ofs < maxOfs && c.Compare(key, a[basei + hint + ofs]) > 0)
                {
                    lastOfs = ofs;
                    ofs = (ofs << 1) + 1;
                    if (ofs <= 0)   // int overflow
                        ofs = maxOfs;
                }
                if (ofs > maxOfs)
                    ofs = maxOfs;
 
                // Make offsets relative to base
                lastOfs += hint;
                ofs += hint;
            }
            else
            { // key <= a[base + hint]
                int maxOfs = hint + 1;
                while (ofs < maxOfs && c.Compare(key, a[basei + hint - ofs]) <= 0)
                {
                    lastOfs = ofs;
                    ofs = (ofs << 1) + 1;
                    if (ofs <= 0)   // int overflow
                        ofs = maxOfs;
                }
                if (ofs > maxOfs)
                    ofs = maxOfs;
 
                // Make offsets relative to base
                int tmp = lastOfs;
                lastOfs = hint - ofs;
                ofs = hint - tmp;
            }
            Debug.Assert(-1 <= lastOfs && lastOfs < ofs && ofs <= len);
 
            lastOfs++;
            while (lastOfs < ofs)
            {
                //int m = lastOfs + ((ofs - lastOfs) >>> 1);
                int m = lastOfs + ((ofs - lastOfs) >> 1);
 
                if (c.Compare(key, a[basei + m]) > 0)
                    lastOfs = m + 1;  // a[basei + m] < key
                else
                    ofs = m;          // key <= a[basei + m]
            }
            Debug.Assert(lastOfs == ofs);   // so a[basei + ofs - 1] < key <= a[base + ofs]
            return ofs;
        }
        private static int gallopRight(T key, T[] a, int basei, int len,
                                           int hint, IComparer<T> c)
        {
            Debug.Assert(len > 0 && hint >= 0 && hint < len);
 
            int ofs = 1;
            int lastOfs = 0;
            if (c.Compare(key, a[basei + hint]) < 0)
            {
                // Gallop left until a[b+hint - ofs] <= key < a[b+hint - lastOfs]
                int maxOfs = hint + 1;
                while (ofs < maxOfs && c.Compare(key, a[basei + hint - ofs]) < 0)
                {
                    lastOfs = ofs;
                    ofs = (ofs << 1) + 1;
                    if (ofs <= 0)   // int overflow
                        ofs = maxOfs;
                }
                if (ofs > maxOfs)
                    ofs = maxOfs;
 
                // Make offsets relative to b
                int tmp = lastOfs;
                lastOfs = hint - ofs;
                ofs = hint - tmp;
            }
            else
            { // a[b + hint] <= key
                // Gallop right until a[b+hint + lastOfs] <= key < a[b+hint + ofs]
                int maxOfs = len - hint;
                while (ofs < maxOfs && c.Compare(key, a[basei + hint + ofs]) >= 0)
                {
                    lastOfs = ofs;
                    ofs = (ofs << 1) + 1;
                    if (ofs <= 0)   // int overflow
                        ofs = maxOfs;
                }
                if (ofs > maxOfs)
                    ofs = maxOfs;
 
                // Make offsets relative to b
                lastOfs += hint;
                ofs += hint;
            }
            Debug.Assert(-1 <= lastOfs && lastOfs < ofs && ofs <= len);
 
            lastOfs++;
            while (lastOfs < ofs)
            {
                //int m = lastOfs + ((ofs - lastOfs) >>> 1);
                int m = lastOfs + ((ofs - lastOfs) >> 1);
 
                if (c.Compare(key, a[basei + m]) < 0)
                    ofs = m;          // key < a[b + m]
                else
                    lastOfs = m + 1;  // a[b + m] <= key
            }
            Debug.Assert(lastOfs == ofs);  // so a[b + ofs - 1] <= key < a[b + ofs]
            return ofs;
        }
        private void mergeLo(int base1, int len1, int base2, int len2)
        {
            Debug.Assert(len1 > 0 && len2 > 0 && base1 + len1 == base2);
 
            // Copy first run into temp array
            var a = this.a; // For performance
            var tmp = ensureCapacity(len1);
            Array.Copy(a, base1, tmp, 0, len1);
 
            int cursor1 = 0;       // Indexes into tmp array
            int cursor2 = base2;   // Indexes int a
            int dest = base1;      // Indexes int a
 
            // Move first element of second run and deal with degenerate cases
            a[dest++] = a[cursor2++];
            if (--len2 == 0)
            {
                Array.Copy(tmp, cursor1, a, dest, len1);
                return;
            }
            if (len1 == 1)
            {
                Array.Copy(a, cursor2, a, dest, len2);
                a[dest + len2] = tmp[cursor1]; // Last elt of run 1 to end of merge
                return;
            }
 
            var c = this.c;  // Use local variable for performance
            int minGallop = this._minGallop;    //  "    "       "     "      "
            outer:
            while (true)
            {
                int count1 = 0; // Number of times in a row that first run won
                int count2 = 0; // Number of times in a row that second run won
 
                do
                {
                    Debug.Assert(len1 > 1 && len2 > 0);
                    if (c.Compare(a[cursor2], tmp[cursor1]) < 0)
                    {
                        a[dest++] = a[cursor2++];
                        count2++;
                        count1 = 0;
                        if (--len2 == 0)
                            goto outer;
                    }
                    else
                    {
                        a[dest++] = tmp[cursor1++];
                        count1++;
                        count2 = 0;
                        if (--len1 == 1)
                            goto outer;
                    }
                } while ((count1 | count2) < minGallop);
 
                do
                {
                    Debug.Assert(len1 > 1 && len2 > 0);
                    count1 = gallopRight(a[cursor2], tmp, cursor1, len1, 0, c);
                    if (count1 != 0)
                    {
                        Array.Copy(tmp, cursor1, a, dest, count1);
                        dest += count1;
                        cursor1 += count1;
                        len1 -= count1;
                        if (len1 <= 1) // len1 == 1 || len1 == 0
                            goto outer;
                    }
                    a[dest++] = a[cursor2++];
                    if (--len2 == 0)
                        goto outer;
 
                    count2 = gallopLeft(tmp[cursor1], a, cursor2, len2, 0, c);
                    if (count2 != 0)
                    {
                        Array.Copy(a, cursor2, a, dest, count2);
                        dest += count2;
                        cursor2 += count2;
                        len2 -= count2;
                        if (len2 == 0)
                            goto outer;
                    }
                    a[dest++] = tmp[cursor1++];
                    if (--len1 == 1)
                        goto outer;
                    minGallop--;
                } while (count1 >= MinGallop | count2 >= MinGallop);
                if (minGallop < 0)
                    minGallop = 0;
                minGallop += 2;  // Penalize for leaving gallop mode
            }  // End of "outer" loop
            this._minGallop = minGallop < 1 ? 1 : minGallop;  // Write back to field
 
            if (len1 == 1)
            {
                Debug.Assert(len2 > 0);
                Array.Copy(a, cursor2, a, dest, len2);
                a[dest + len2] = tmp[cursor1]; //  Last elt of run 1 to end of merge
            }
            else if (len1 == 0)
            {
                throw new ArgumentException(
                    "Comparison method violates its general contract!");
            }
            else
            {
                Debug.Assert(len2 == 0);
                Debug.Assert(len1 > 1);
                Array.Copy(tmp, cursor1, a, dest, len1);
            }
        }
 
        private void mergeHi(int base1, int len1, int base2, int len2)
        {
            Debug.Assert(len1 > 0 && len2 > 0 && base1 + len1 == base2);
 
            // Copy second run into temp array
            T[] a = this.a; // For performance
            T[] tmp = ensureCapacity(len2);
            Array.Copy(a, base2, tmp, 0, len2);
 
            int cursor1 = base1 + len1 - 1;  // Indexes into a
            int cursor2 = len2 - 1;          // Indexes into tmp array
            int dest = base2 + len2 - 1;     // Indexes into a
 
            // Move last element of first run and deal with degenerate cases
            a[dest--] = a[cursor1--];
            if (--len1 == 0)
            {
                Array.Copy(tmp, 0, a, dest - (len2 - 1), len2);
                return;
            }
            if (len2 == 1)
            {
                dest -= len1;
                cursor1 -= len1;
                Array.Copy(a, cursor1 + 1, a, dest + 1, len1);
                a[dest] = tmp[cursor2];
                return;
            }
 
            var c = this.c;  // Use local variable for performance
            int minGallop = this._minGallop;    //  "    "       "     "      "
            outer:
            while (true)
            {
                int count1 = 0; // Number of times in a row that first run won
                int count2 = 0; // Number of times in a row that second run won
 
                do
                {
                    Debug.Assert(len1 > 0 && len2 > 1);
                    if (c.Compare(tmp[cursor2], a[cursor1]) < 0)
                    {
                        a[dest--] = a[cursor1--];
                        count1++;
                        count2 = 0;
                        if (--len1 == 0)
                            goto outer;
                    }
                    else
                    {
                        a[dest--] = tmp[cursor2--];
                        count2++;
                        count1 = 0;
                        if (--len2 == 1)
                            goto outer;
                    }
                } while ((count1 | count2) < minGallop);
 
                do
                {
                    Debug.Assert(len1 > 0 && len2 > 1);
                    count1 = len1 - gallopRight(tmp[cursor2], a, base1, len1, len1 - 1, c);
                    if (count1 != 0)
                    {
                        dest -= count1;
                        cursor1 -= count1;
                        len1 -= count1;
                        Array.Copy(a, cursor1 + 1, a, dest + 1, count1);
                        if (len1 == 0)
                            goto outer;
                    }
                    a[dest--] = tmp[cursor2--];
                    if (--len2 == 1)
                        goto outer;
 
                    count2 = len2 - gallopLeft(a[cursor1], tmp, 0, len2, len2 - 1, c);
                    if (count2 != 0)
                    {
                        dest -= count2;
                        cursor2 -= count2;
                        len2 -= count2;
                        Array.Copy(tmp, cursor2 + 1, a, dest + 1, count2);
                        if (len2 <= 1)  // len2 == 1 || len2 == 0
                            goto outer;
                    }
                    a[dest--] = a[cursor1--];
                    if (--len1 == 0)
                        goto outer;
                    minGallop--;
                } while (count1 >= MinGallop | count2 >= MinGallop);
                if (minGallop < 0)
                    minGallop = 0;
                minGallop += 2;  // Penalize for leaving gallop mode
            }  // End of "outer" loop
            this._minGallop = minGallop < 1 ? 1 : minGallop;  // Write back to field
 
            if (len2 == 1)
            {
                Debug.Assert(len1 > 0);
                dest -= len1;
                cursor1 -= len1;
                Array.Copy(a, cursor1 + 1, a, dest + 1, len1);
                a[dest] = tmp[cursor2];  // Move first elt of run2 to front of merge
            }
            else if (len2 == 0)
            {
                throw new ArgumentException(
                    "Comparison method violates its general contract!");
            }
            else
            {
                Debug.Assert(len1 == 0);
                Debug.Assert(len2 > 0);
                Array.Copy(tmp, 0, a, dest - (len2 - 1), len2);
            }
        }
 
        private T[] ensureCapacity(int minCapacity)
        {
            if (_tmp.Length < minCapacity)
            {
                // Compute smallest power of 2 > minCapacity
                int newSize = minCapacity;
                newSize |= newSize >> 1;
                newSize |= newSize >> 2;
                newSize |= newSize >> 4;
                newSize |= newSize >> 8;
                newSize |= newSize >> 16;
                newSize++;
 
                if (newSize < 0) // Not bloody likely!
                    newSize = minCapacity;
                else
                    //newSize = Math.Min(newSize, a.Length >>> 1);
                    newSize = Math.Min(newSize, a.Length >> 1);
 
                _tmp = new T[newSize];
            }
            return _tmp;
        }
 
        private static void rangeCheck(int arrayLen, int fromIndex, int toIndex)
        {
            if (fromIndex > toIndex)
                throw new ArgumentException("fromIndex(" + fromIndex +
                           ") > toIndex(" + toIndex + ")");
            if (fromIndex < 0)
                throw new ArgumentOutOfRangeException("fromIndex", fromIndex.ToString());
            if (toIndex > arrayLen)
                throw new ArgumentOutOfRangeException("toIndex", toIndex.ToString());
        }
 
 
    }
}