using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class Deque<T>
    {
        public DoublyLinkedList<T> _items = new DoublyLinkedList<T>();
        public int Count { get { return _items.Count; } }

        public void EnqueueFirst(T value)
        {
            _items.AddFirst(value);
        }

        public void EnqueueLast(T value)
        {
            _items.AddLast(value);
        }

        public T DequeueFirst()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            T temp = _items.Head.Value;
            _items.RemoveFirst();
            return temp;
        }

        public T DequeueLast()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            T temp = _items.Tail.Value;
            _items.RemoveLast();
            return temp;
        }

        public T PeekFirst()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            return _items.Head.Value;
        }

        public T PeekLast()
        {
            if (_items.Count == 0)
            {
                throw new InvalidOperationException("The queue is empty");
            }

            return _items.Tail.Value;
        }
    }
}

