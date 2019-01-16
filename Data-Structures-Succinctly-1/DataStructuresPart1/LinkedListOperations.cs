using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class LinkedListOperations
    {
        public static void PrintList<T>(LinkedListNode<T> node)
        {
            while (node != null)
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
        }

        /// <summary>
        /// An example of enumerating backwards manually.
        /// </summary>
        public static void PrintListBackwards()
        {
            DoublyLinkedList<int> list = new DoublyLinkedList<int>() { 1, 2, 3, 4 };
            DoublyLinkedListNode<int> current = list.Tail;
            while (current != null)
            {
                Console.WriteLine(current.Value);
                current = current.Previous;
            }
            Console.ReadLine();
        }
    }
}
