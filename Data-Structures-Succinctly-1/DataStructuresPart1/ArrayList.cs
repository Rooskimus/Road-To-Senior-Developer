using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class ArrayList<T> : System.Collections.Generic.IList<T>
    {
        T[] _items;

        public ArrayList() : this(0)
        {

        }

        public ArrayList(int length)
        {
            if (length < 0)
            {
                throw new ArgumentException("length");
            }
            _items = new T[length];
        }

        public T this[int index]
        {
            get
            {
                if (index < Count)
                {
                    return _items[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index < Count)
                {
                    _items[index] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get { return false; } }

        public void Add(T item)
        {
            if (_items.Length == Count)
            {
                GrowArray();
            }
            _items[Count++] = item;
        }

        public void Clear()
        {
            _items = new T[0];
            Count = 0;
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
            // Able to use != because IndexOf returns -1 if the item is not found
            // and our statement becomes false in that case.
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_items, 0, array, arrayIndex, Count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < Count; i++)
            {
                yield return _items[i];
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (_items[i].Equals(item)) return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            if (_items.Length == this.Count)
            {
                this.GrowArray();
            }

            //Shift all the items following the index one slot to the right.
            Array.Copy(_items, index, _items, index + 1, Count - index);
            // Copies the array starting from the first given index to the array at the second given index.
            // Count - index gives the number of items to copy.
            _items[index] = item;
            Count++;

        }

        public bool Remove(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if ( _items[i].Equals(item))
                {
                    RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count)
            {
                throw new IndexOutOfRangeException();
            }
            int shiftStart = index + 1;
            if (shiftStart < Count)
            {
                Array.Copy(_items, shiftStart, _items, index, Count - shiftStart);
            }
            Count--;
            // When deleting last index, no shift occurs but the decrease in Count
            // accounts for the deletion as no gets or sets outside of the Count size are allowed.
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void GrowArray()
        {
            int newLength = _items.Length == 0 ? 16 : _items.Length << 1;
            // << performs a binary shift left, essentially doubling your number.
            T[] newArray = new T[newLength];
            _items.CopyTo(newArray, 0);
            _items = newArray;
        }

    }
}
