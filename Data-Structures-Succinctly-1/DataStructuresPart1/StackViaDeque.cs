using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    // Making a stack with a Deque method is simple, allows for code
    // re-usage, and if you optimize the Deque to be implemented with
    // an array it can give some performance gains.
    public class StackViaDeque<T>
    {
        Deque<T> _items = new Deque<T>();
        public int Count { get { return _items.Count; } }

        public void Push (T value)
        {
            _items.EnqueueFirst(value);
        }

        public T Pop()
        {
            return _items.DequeueFirst();
        }

        public T Peek()
        {
            return _items.PeekFirst();
        }
    }
}
