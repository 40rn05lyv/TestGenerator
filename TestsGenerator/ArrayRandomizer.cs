using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestsGenerator
{
    public sealed class ArrayRandomizer<T>
    {
        private List<T> m_sourceArray;
        // if cryptographically save random numbers are needed 
        // switch to System.Security.Cryptography.RandomNumberGenerator        
        private Random m_random;

        /// <summary>
        /// returns a copy of the source array that is used to generate random arrays from.
        /// </summary>
        public List<T> SourceArray
        {
            get { return new List<T>(m_sourceArray); }
        }

        /// <summary>
        /// Construct an array randomizer, drawing random numbers from sourceArray.
        /// </summary>
        /// <param name="sourceArray">the array the randomizer draws its numbers from</param>
        public ArrayRandomizer(List<T> sourceArray)
        {
            m_random = new Random();
            m_sourceArray = sourceArray;
        }

        #region Recursion

        /// <summary>
        /// returns a random draw of entries of SourceArray of random length. 
        /// Any specific entry in SourceArray can at most be used once in 
        /// the resulting random array. The returned array is construced 
        /// using a recursive approach.
        /// </summary>
        /// <returns>random draw of random length</returns>
        public List<T> GetRandomSubsetRecursion()
        {
            int size = m_random.Next(m_sourceArray.Count);
            return GetRandomSubsetRecursion(size);
        }

        /// <summary>
        /// returns a random draw of entries of SourceArray of length size. 
        /// Any specific entry in SourceArray can at most be used once in 
        /// the resulting random array. The returned array is construced 
        /// using a recursive approach.
        /// </summary>
        /// <param name="size">size of array to return.</param>
        /// <returns>random draw of length size</returns>
        public List<T> GetRandomSubsetRecursion(int size)
        {
            if (size > m_sourceArray.Count)
            {
                throw new ArgumentException("Size can't be larger than count of elements in SourceArray", "size");
            }
            List<T> target = new List<T>();
            GetRandomSubsetRecursion(SourceArray, target, size);
            return target;
        }


        private void GetRandomSubsetRecursion(List<T> source, List<T> target, int size)
        {
            if (size > 0)
            {
                int randomElement = m_random.Next(source.Count);
                T element = source[randomElement];
                source.RemoveAt(randomElement);
                target.Add(element);
                GetRandomSubsetRecursion(source, target, size - 1);
            }
        }

        #endregion

        #region FisherYatesShuffle

        /// <summary>
        /// returns a random draw of entries of SourceArray of random length. 
        /// Any specific entry in SourceArray can at most be used once in 
        /// the resulting random array. The returned array is construced 
        /// using the Fisher-Yates shuffle algorithm.
        /// http://www.nist.gov/dads/HTML/fisherYatesShuffle.html
        /// </summary>
        /// <returns>random draw of random length</returns>
        public List<T> GetRandomSubsetFisherYates()
        {
            int size = m_random.Next(m_sourceArray.Count);
            return GetRandomSubsetFisherYates(size);
        }

        /// <summary>
        /// returns a random draw of entries of SourceArray of random length. 
        /// Any specific entry in SourceArray can at most be used once in 
        /// the resulting random array. The returned array is construced 
        /// using the Fisher-Yates shuffle algorithm.
        /// http://www.nist.gov/dads/HTML/fisherYatesShuffle.html
        /// </summary>
        /// <param name="size">size of array to return.</param>
        /// <returns>random draw of length size</returns>
        public List<T> GetRandomSubsetFisherYates(int size)
        {
            int fullSize = m_sourceArray.Count;
            if (size > fullSize)
            {
                throw new ArgumentException("Size can't be larger than count of elements in SourceArray", "size");
            }
            if (size < 0)
            {
                // to be consistent with recursive version, maybe return a zero-size list?
                throw new ArgumentException("Size can't be negative", "size");
            }

            List<T> randomArray = SourceArray;
            
            if (size > 0)
            {
                for (int i = 0; i < size - 1; i++)
                {
                    int swapPosition = i + m_random.Next(fullSize - i);
                    T swap = randomArray[swapPosition];
                    randomArray[swapPosition] = randomArray[i];
                    randomArray[i] = swap;
                }
            }

            T[] targetArray = new T[size];
            randomArray.CopyTo(0, targetArray, 0, size);
            return new List<T>(targetArray);
        }

        #endregion

    }
}
