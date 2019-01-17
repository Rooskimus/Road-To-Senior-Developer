using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class DequeViaArray<T>
    {
        T[] _items = new T[0];
        int _size = 0; // Number of items in queue.
        int _head = 0; // Index of the oldest (first) item in queue.
        int _tail = -1; // Index of the newest (last) item in queue.
        public int Count { get { return _size; } }

        private void allocateNewArray(int startingIndex) // O(n)
        {
            int newLength = (_size == 0) ? 4 : _size * 2;
            T[] newArray = new T[newLength];
            if (_size > 0)
            {
                int targetIndex = startingIndex;
                // We want to copy the contents.  The default case assumes no wrapping
                // around the array to get to the head.  Just copy the valid range.
                // Else, we want to copy from the head to the end of the array then
                // from 0 to the tail of the array.
                if (_tail < _head) // If true, we've wrapped.
                {
                    // Copy head to end of array:
                    for (int index = _head; index < _items.Length; index++)
                    {
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                    // Copy from beginning of array to tail:
                    for (int index = 0; index <= _tail; index++) // Note that targetIndex carries over
                    {                                            // from the first for loop giving continuity.
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                }
                else // No wrap:
                {
                    // Copy from head to tail:
                    for(int index = _head; index <=_tail; index++)
                    {
                        newArray[targetIndex] = _items[index];
                        targetIndex++;
                    }
                }
                _head = startingIndex;
                _tail = targetIndex - 1; // Compensate for extra bump (+1 at end of for loops above);
            }
            else // Array is empty
            {
                _head = 0;
                _tail = -1;
            }

            _items = newArray;
        }

        public void EnqueueFirst(T item) // O(1) usually, O(n) when growth needed.
        {
            // If array needs to grow:
            if (_items.Length == _size)
            {
                allocateNewArray(1);
            }
            // Note we leave an empty space before the head.  If the array doesn't need
            // to grow, there also will be an empty space in front of the head.  So:
            if (_head > 0)
            {
                _head--;
            }
            else //In this case, the empty space would be at the end of the array.
            {
                _head = _items.Length - 1; // Index starts at 0, length starts at 1
            }
            _items[_head] = item;
            _size++;
        }

        public void EnqueueLast(T item)
        {
            // If the array needs to grow:
            if(_items.Length == _size)
            {
                allocateNewArray(0);
            }
            // If _tail is at the end of the array, we need to wrap around
            if (_tail == _items.Length - 1)
            {
                _tail = 0;
            }
            else // Else just go to next index
            {
                _tail++;
            }
            _items[_tail] = item;
            _size++;
        }

        public T DequeueFirst() // 0(1)
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("THe deque is empty");
            }
            T value = _items[_head];
            if(_head == _items.Length - 1) // If head is last index, wrap it around
            {
                _head = 0;
            }
            else // Move to the next slot
            {
                _head++;
            }
            _size--;
            return value;
        }

        public T DequeueLast() // 0(1)
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("THe deque is empty");
            }
            T value = _items[_tail];
            if (_tail == 0) // If tail is first index, wrap it around
            {
                _tail = _items.Length - 1;
            }
            else // Move to the previous slot
            {
                _tail--;
            }
            _size--;
            return value;
        }

        public T PeekFirst() // O(1)
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The deque is empty");
            }
            return _items[_head];
        }

        public T PeekLast() // O(1)
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The deque is empty");
            }
            return _items[_tail];
        }
    }
}
