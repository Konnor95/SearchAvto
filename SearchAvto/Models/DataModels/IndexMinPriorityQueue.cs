using System;
using System.Collections;
using System.Collections.Generic;

namespace SearchAvto.Models.DataModels
{
    class NullAble<T>
    {
        public T Value;

        public NullAble(T value)
        {
            Value = value;
        }
    }
    public class IndexMinPriorityQueue<T> : IEnumerable<int> where T : IComparable
    {
        private readonly int nmax; // maximum number of elements on PQ
        private readonly int[] pq; // binary heap using 1-based indexing
        private readonly int[] qp; // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
        private readonly NullAble<T>[] keys; // keys[i] = priority of i

        /// <summary>
        /// Number of elements in indexed priority queue
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Initializes an empty indexed priority queue with indices between 0 and length-1
        /// </summary>
        /// <param name="length">The queue length</param>
        public IndexMinPriorityQueue(int length)
        {
            if (length < 0) throw new ArgumentException();
            nmax = length;
            keys = new NullAble<T>[length + 1]; // make this of length NMAX??
            for (int i = 0; i < keys.Length; i++)
                keys[i] = new NullAble<T>(default(T));
            pq = new int[length + 1];
            qp = new int[length + 1]; // make this of length NMAX??
            for (int i = 0; i <= length; i++) qp[i] = -1;
        }

        /// <summary>
        /// Returns true if the queue is empty; false otherwise
        /// </summary>
        public bool IsEmpty { get { return Size == 0; } }


        ///<summary>
        /// Returns true if the index is inside of the queue, flase otherwise
        /// </summary>
        /// <param name="index">An index</param>
        public bool Contains(int index)
        {
            if (index < 0 || index >= nmax) throw new IndexOutOfRangeException();
            return qp[index] != -1;
        }

        /// <summary>
        /// Associates key with index.
        /// </summary>
        /// <param name="index">The key to associate with index</param>
        /// <param name="key">An index</param>
        public void Insert(int index, T key)
        {
            if (index < 0 || index >= nmax) throw new IndexOutOfRangeException();
            if (Contains(index)) throw new ArgumentException("Index is already in the priority queue");
            Size++;
            qp[index] = Size;
            pq[Size] = index;
            keys[index].Value = key;
            Swim(Size);
        }

        /// <summary>
        /// Returns an index associated with a minimum key.
        /// </summary>
        /// <returns>An index associated with a minimum key</returns>
        public int MinIndex()
        {
            if (Size == 0) throw new NullReferenceException("Priority queue underflow");
            return pq[1];
        }

        /// <summary>
        /// Returns a minimum key.
        /// </summary>
        /// <returns>A minimum key</returns>
        public T MinKey()
        {
            if (Size == 0) throw new NullReferenceException("Priority queue underflow");
            return keys[pq[1]].Value;
        }

        /// <summary>
        /// Removes a minimum key and returns its associated index.
        /// </summary>
        /// <returns>An index associated with a minimum key</returns>
        public int DelMin()
        {
            if (Size == 0) throw new NullReferenceException("Priority queue underflow");
            int min = pq[1];
            Exch(1, Size--);
            Sink(1);
            qp[min] = -1; // delete
            keys[pq[Size + 1]] = null; // to help with garbage collection
            pq[Size + 1] = -1; // not needed
            return min;
        }


        /// <summary>
        /// Returns the key associated with index
        /// </summary>
        /// <param name="index">the index of the key to return</param>
        /// <returns>the key associated with index</returns>
        public T KeyOf(int index)
        {
            if (index < 0 || index >= nmax) throw new IndexOutOfRangeException();
            if (!Contains(index)) throw new ArgumentException("Index is not in the priority queue");
            return keys[index].Value;
        }

        /// <summary>
        /// Changes the key associated with index to the specified value.
        /// </summary>
        /// <param name="index">the index of the key to change</param>
        /// <param name="key">change the key assocated with index to this key</param>
        public void ChangeKey(int index, T key)
        {
            if (index < 0 || index >= nmax) throw new IndexOutOfRangeException();
            if (!Contains(index)) throw new ArgumentException("Index is not in the priority queue");
            keys[index].Value = key;
            Swim(qp[index]);
            Sink(qp[index]);
        }

        /// <summary>
        /// Decreases the key associated with index i to the specified value.
        /// </summary>
        /// <param name="index">the index of the key to decrease</param>
        /// <param name="key">decrease the key assocated with index to this key</param>
        public void DecreaseKey(int index, T key)
        {
            if (index < 0 || index >= nmax) throw new IndexOutOfRangeException();
            if (!Contains(index)) throw new ArgumentException("Index is not in the priority queue");
            if (keys[index].Value.CompareTo(key) <= 0)
                throw new ArgumentException(
                    "Calling DecreaseKey() with given argument would not strictly decrease the key");
            keys[index].Value = key;
            Swim(qp[index]);
        }

        /// <summary>
        /// Increases the key associated with index to the specified value.
        /// </summary>
        /// <param name="index">the index of the key to increase</param>
        /// <param name="key">increase the key assocated with index i to this key</param>
        public void IncreaseKey(int index, T key)
        {
            if (index < 0 || index >= nmax) throw new IndexOutOfRangeException();
            if (!Contains(index)) throw new ArgumentException("Index is not in the priority queue");
            if (keys[index].Value.CompareTo(key) >= 0)
                throw new ArgumentException(
                    "Calling increaseKey() with given argument would not strictly increase the key");
            keys[index].Value = key;
            Sink(qp[index]);
        }

        /// <summary>
        /// Removes the key associated with index i.
        /// </summary>
        /// <param name="index">the index of the key to remove</param>
        public void Delete(int index)
        {
            if (index < 0 || index >= nmax) throw new IndexOutOfRangeException();
            if (!Contains(index)) throw new ArgumentException("Index is not in the priority queue");
            index = qp[index];
            Exch(index, Size--);
            Swim(index);
            Sink(index);
            keys[index] = null;
            qp[index] = -1;
        }


        #region Helper functions

        private bool Greater(int i, int j)
        {
            return keys[pq[i]].Value.CompareTo(keys[pq[j]].Value) > 0.0;
        }

        private void Exch(int i, int j)
        {
            int swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
            qp[pq[i]] = i;
            qp[pq[j]] = j;
        }

        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exch(k, k / 2);
                k = k / 2;
            }
        }

        private void Sink(int k)
        {
            while (2 * k <= Size)
            {
                int j = 2 * k;
                if (j < Size && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exch(k, j);
                k = j;
            }
        }

        #endregion

        public IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>)pq).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
