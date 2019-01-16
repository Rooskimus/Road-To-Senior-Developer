using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class LinkedListNode<T>
    {
        public LinkedListNode(T value)  // Constructs new node with given value
        {
            Value = value;
        }
        public T Value { get; internal set; } // Node value
        public LinkedListNode<T> Next { get; internal set; } // Reference to next node in sequence
    }
}
