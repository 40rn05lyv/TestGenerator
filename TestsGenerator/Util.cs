using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestsGenerator
{
    static class Util
    {
        //
        //http://stackoverflow.com/questions/375351/most-efficient-way-to-randomly-sort-shuffle-a-list-of-integers-in-c-sharp
        //
        public static T[] RandomPermutation<T>(T[] array)
        {
            T[] retArray = new T[array.Length];
            array.CopyTo(retArray, 0);

            Random random = new Random();
            for (int i = 0; i < array.Length; i += 1)
            {
                int swapIndex = random.Next(i, array.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
        }

        //Horribly ineffective because of copying only the beginning of array!!!
        public static T[] RandomPermutation<T>(T[] array, int size)
        {
            if (size > array.Length)
            {
                throw new ArgumentException("Size can't be larger than count of elements in SourceArray", "size");
            }
            if (size < 0)
            {
                // to be consistent with recursive version, maybe return a zero-size list?
                throw new ArgumentException("Size can't be negative", "size");
            }

            Random random = new Random();
            T[] retArray = new T[size];
            array.CopyTo(retArray, 0);

            if (size > 0)
            {
                for (int i = 0; i < size - 1; i++)
                {
                    int swapPosition = random.Next(i + 1, array.Length);
                    T swap = retArray[swapPosition];
                    retArray[swapPosition] = retArray[i];
                    retArray[i] = swap;
                }
            }

            return retArray;
        }

        //My implementation. Modified a bit
        //TODO: compare efficiency of my method and previous one
        public static T[] RandomPermutation2<T>(T[] array)
        {
            T[] retArray = new T[array.Length];
            array.CopyTo(retArray, 0);

            Random random = new Random();
            for (int i = 0; i < array.Length; ++i)
            {
                int swapIndex = random.Next(i + 1, array.Length);
                //Swapping
                T temp = retArray[i];
                retArray[i] = retArray[swapIndex];
                retArray[swapIndex] = temp;
            }

            return retArray;
        }

        public static int[] RandomizeRange(int range)
        {
            int[] arr = new int[range];
            for (int i = 0; i < range; ++i)
                arr[i] = i;
            return RandomPermutation<int>(arr);
        }

    }
}
