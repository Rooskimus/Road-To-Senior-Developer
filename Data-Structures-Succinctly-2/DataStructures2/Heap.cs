using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures2
{
    public class Heap<T> // We are building a max heap.
        where T: IComparable<T>
    {
        T[] _items;
        int _count;
        const int DEFAULT_LENGTH = 100;
        public Heap()
            : this(DEFAULT_LENGTH)
        {

        }

        public Heap (int length)
        {
            _items = new T[length];
            _count = 0;
        }

        public void Add(T value) // O(log n)
        {
            // Add value to the end of backing array (growing if necessary)
            // Swap the value with its paretn until the heap is properly satisfied.
            if (_count >= _items.Length)
            {
                GrowBackingArray();
            }
            _items[_count] = value;
            int index = _count;
            while (index < 0 && _items[index].CompareTo(_items[Parent(index)]) > 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }

            _count++;
        }

        private void GrowBackingArray()
        {
            T[] newItems = new T[_items.Length * 2];
            for (int i = 0; i < _items.Length; i++)
            {
                newItems[i] = _items[i];
            }
            _items = newItems;
        }

        public T Peek() // O(1)
        {
            if (Count > 0)
            {
                return _items[0];
            }
            throw new InvalidOperationException();
        }

        public T RemoveMax() //O(log n)
        {
            if (Count <= 0)
            {
                throw new InvalidOperationException();
            }
            T max = _items[0];
            _items[0] = _items[_count - 1];
            _count--;
            int index = 0;
            while (index < _count)
            {
                // Get the left and right child indices.
                int left = (2 * index) + 1;
                int right = (2 * index) + 2;
                // Make sure we are still within the heap.
                if (left >= _count)
                {
                    break;
                }
                // To avoid having to swap twice, we swap with the largest value.
                // Find the largest child.
                int maxChildIndex = IndexOfMaxChild(left, right);
                if (_items[index].CompareTo(_items[maxChildIndex]) > 0)
                {
                    // The current item is larger than its children and the heap is satisfied.
                    break;
                }
                Swap(index, maxChildIndex);
                index = maxChildIndex;
            }
            return max;
        }

        private int IndexOfMaxChild(int left, int right)
        {
            // Find the index of the child with the largest value.
            int maxChildIndex = -1;
            if (right >= _count)
            {
                // No right child.
                maxChildIndex = left;
            }
            else
            {
                if (_items[left].CompareTo(_items[right]) > 0)
                {
                    maxChildIndex = left;
                }
                else maxChildIndex = right;
            }
            return maxChildIndex;
        }

        public int Count { get { return _count; } }

        public void Clear() // O(1)
        {
            _count = 0;
            _items = new T[DEFAULT_LENGTH];
        }

        private int Parent(int index)

        {

            return (index - 1) / 2;

        }

        private void Swap(int left, int right)

        {

            T temp = _items[left];

            _items[left] = _items[right];

            _items[right] = temp;

        }
    }
}
