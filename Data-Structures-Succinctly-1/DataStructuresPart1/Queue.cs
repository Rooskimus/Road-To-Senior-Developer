using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class Queue<T>
    {
        public DoublyLinkedList<T> _items = new DoublyLinkedList<T>();
        public int Count { get { return _items.Count; } }
    
        public void Enqueue(T value)
        {
            _items.AddFirst(value);
        }

        public T Dequeue()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            T last = _items.Tail.Value;
            _items.RemoveLast();
            return last;
        }

        public T Peek()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            return _items.Tail.Value;
        }
    }
}
