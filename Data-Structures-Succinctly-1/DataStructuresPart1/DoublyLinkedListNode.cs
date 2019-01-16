using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class DoublyLinkedListNode<T>
    {
        public DoublyLinkedListNode(T value) 
        {
            Value = value;
        }
        public T Value { get; internal set; } 
        public DoublyLinkedListNode<T> Next { get; internal set; }
        public DoublyLinkedListNode<T> Previous { get; internal set; }  // Only new addition vs singly linked
        // Previous null if first node.
    }
}

