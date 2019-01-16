using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class LinkedList<T> :
         System.Collections.Generic.ICollection<T>
    {
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }
        private LinkedListNode<T> _head;
        private LinkedListNode<T> _tail;

        public void Add(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);
            if (_head == null)
            {
                _head = node;
                _tail = node;
            }

            else
            {
                _tail.Next = node; // Sets reference for old tail to point at new tail
                _tail = node; // Sets the new tail's value.
            }

            Count++;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            LinkedListNode<T> current = _head; // Start at head.
            while (current != null) //Stop looking when you hit the end of the list.
            {
                if (current.Value.Equals(item)) return true; // If match is found, stop and return true.
                current = current.Next; // else move to next value and try again.
            }
            return false; // If value not found, return false.
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            LinkedListNode<T> current = _head; // Start from the head.
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            LinkedListNode<T> current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)this).GetEnumerator(); // Casts object to IEnumberable<T> and calls IEnumerable<T> version of GetEnumerator.
        }

        public bool Remove(T item)
        {
            //Set ourselves up to analyze from the head:
            LinkedListNode<T> previous = null; // Heads have nothing before them!
            LinkedListNode<T> current = _head; // Head is our starting point.

            // In the case our list is empty the head would be null, we return false immediately - thus no changes on an empty list.
            while (current != null)
            {
                // Next we see if the search value is the same as the value we're looking at.
                // If not we grab the next value in our linked list.  If we hit the end (null)
                // without finding it, our while loop kicks us out, nothing changes, and a false is returned.

                if (current.Value.Equals(item))  // We found a matching value!
                {
                    if (previous != null)  // Previous is only null for heads, so we found a middle or last.
                    {
                        previous.Next = current.Next;  // Set the previous node to link to the next node.

                        if (current.Next == null) // if the next node is null, that means we're deleting the tail.
                        {
                            _tail = previous; // Assign new tail.
                        }
                    }

                    else  // Previous was a null, so we found a match on the head.
                    {
                        _head = _head.Next; // Assign new head

                        if (_head == null)  // Is our new head null?  That if so that means we've removed
                                            // the only item in a single-item list.
                        {
                            _tail = null;  // Set tail to be null as we now have an empty list.
                        }
                    }

                    // In all cases where a match was found, we need to reduce the Count and return a true.
                    Count--;
                    return true;
                }
                // As stated above, these move us to our next node when a matching value is not found
                previous = current;
                current = current.Next;
            }
            return false; //Report no changes were made and no matches were found.
        }

    }
}
