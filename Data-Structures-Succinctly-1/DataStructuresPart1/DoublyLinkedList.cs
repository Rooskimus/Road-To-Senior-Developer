using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class DoublyLinkedList<T> : ICollection<T>
    {
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }
        private DoublyLinkedListNode<T> _head;
        private DoublyLinkedListNode<T> _tail;
        public DoublyLinkedListNode<T> Head { get { return _head; } }
        public DoublyLinkedListNode<T> Tail { get { return _tail; } }
        // By exposing the Head and Tail, we can now enumerate the list manually, which allows
        // for reverse enumeration and search.  Also lets us make a deque class.

        public void Add(T value)
        {
            AddLast(value);  // Only left for compatibility with old linked list.
        }

        public void AddFirst(T value)
        {
            DoublyLinkedListNode<T> node = new DoublyLinkedListNode<T>(value);
            DoublyLinkedListNode<T> temp = _head;
            _head = node;                    // Make our new node the new head
            _head.Next = temp;               // Point the next to the old head.
            if (Count == 0) _tail = _head;   // If list was empty, head and tail are the same.
            else temp.Previous = _head;      // If not, the old head's previous points to new head.
            Count++;                         // Either way, our count goes up.
        }

        public void AddLast(T value)
        {
            DoublyLinkedListNode<T> node = new DoublyLinkedListNode<T>(value);
            if (Count == 0) _head = node;
            else
            {
                _tail.Next = node;        // The old tail's next points to new node.
                node.Previous = _tail;    // The new node's previous points to old tail.
            }
            _tail = node;                 // The new node will always become the new tail.
            Count++;                      // Our count rises.
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            DoublyLinkedListNode<T> current = _head; // Start at head.
            while (current != null) //Stop looking when you hit the end of the list.
            {
                if (current.Value.Equals(item)) return true; // If match is found, stop and return true.
                current = current.Next; // else move to next value and try again.
            }
            return false; // If value not found, return false.
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            DoublyLinkedListNode<T> current = _head; // Start from the head.
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            DoublyLinkedListNode<T> current = _head;
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
            DoublyLinkedListNode<T> previous = null; // Heads have nothing before them!
            DoublyLinkedListNode<T> current = _head; // Head is our starting point.

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
                        else
                        {
                            current.Next.Previous = previous; // The only new addition vs single linked list.
                            // When a middle link is removed the previous from the following node needs to point
                            // to the node before the one being removed: 3 <-> 5 <-> 7; 5 was removed and reference
                            // to new next added above: 3 ----> 7; Then this step makes 3 <-----> 7;
                        }
                    }

                    else  // Previous was a null, so we found a match on the head.
                    {
                        RemoveFirst(); // This was changed to avoid repeated code;
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

        public void RemoveFirst()
        {
            if (Count != 0)
            {
                _head = _head.Next;           // 2nd item becomes new head.
                Count--;                      // Count decreases.
                if (Count == 0) _tail = null;  // If there are no items, tail is now null too.
                else _head.Previous = null;    // new head's Previous should point to null
                                               // I wondered why this was in the if/else rather than simply being above Count--
                                               // but I think it's because this operation is not necessary if the item had been
                                               // a singleton and spares us one command in that case.
            }
        }

        public void RemoveLast()
        {
            if (Count != 0)
            {
                _tail = _tail.Previous;
                Count--;
                if (Count == 0) _head = null;
                else _tail.Next = null;

                // I don't know why, but the book broke pattern here and created a
                // if (Count == 1) case where _head and _tail are set to null.
                // but this is accounted for with the above code, keeping the same pattern
                // as our RemoveFirst method.  I.e., if there is one item in the list,
                // _tail.Previous is null so _tail becomes null.  When you do Count--
                // Count becomes 0, then _head is set to null, resulting in null/null.
            }
        }
    }
}
