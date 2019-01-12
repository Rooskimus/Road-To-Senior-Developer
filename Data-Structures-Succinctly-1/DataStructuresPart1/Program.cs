using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating a linked list of nodes with a basic node class.
            Node first = new Node { Value = 3 };
            Node middle = new Node { Value = 5 };
            first.Next = middle;
            Node last = new Node { Value = 7 };
            middle.Next = last;

            //Print values of nodes in order; must feed in first node.
            LinkedListOperations.PrintList(first);
            Console.ReadLine();
        }

        public class LinkedListNode<T>
        {
            public LinkedListNode(T value)  // Constructs new node with given value
            {
                Value = value;
            }
            public T Value { get; internal set; } // Node value
            public LinkedListNode<T> Next { get; internal set; } // Reference to next node in sequence
        }

        public class LinkedList<T> :
            System.Collections.Generic.ICollection<T>
        {
            public int Count { get; private set; }
            private LinkedListNode<T> _head;
            private LinkedListNode<T> _tail;

            public bool IsReadOnly => throw new NotImplementedException();

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
                throw new NotImplementedException();
            }

            public bool Contains(T item)
            {
                throw new NotImplementedException();
            }

            public void CopyTo(T[] array, int arrayIndex)
            {
                throw new NotImplementedException();
            }

            public IEnumerator<T> GetEnumerator()
            {
                throw new NotImplementedException();
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

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
