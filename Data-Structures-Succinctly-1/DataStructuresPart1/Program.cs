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
                LinkedListNode<T> previous = null;
                LinkedListNode<T> current = _head;

                // 1: Empty list: Do nothing.
                // 2: Single node: Previous is null
                // 3: Many nodes:
                //  a: Node to remove is the first node.
                //  b: Node to remove is the middle or last.

                while (current != null)
                {
                    if (current.Value.Equals(item))
                    {
                        // It's a node in the middle or end.
                        if (previous != null)
                        {
                            // Case 3b
                            // Before: Head -> 3 -> 5 -> null
                            // After: Head -> 3 -------> null
                            previous.Next = current.Next;

                            if (current.Next == null)
                            {
                                _tail = previous;
                            }
                        }

                        else
                        {
                            // Case 2 or 3a.
                            // Before: Head -> 3 -> 5
                            // After:  Head ------> 5
                            // Head -> 3 -> null
                            // Head ------> null

                            _head = _head.Next;

                            // Is the list now empty?
                            if (_head == null)
                            {
                                _tail = null;
                            }
                        }

                        Count--;
                        return true;
                    }
                    previous = current;
                    current = current.Next;
                }
                return false;
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }
    }
}
