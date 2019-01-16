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
            LinkedListNode<int> first = new LinkedListNode<int>(3);
            LinkedListNode<int> middle = new LinkedListNode<int>(5);
            first.Next = middle;
            LinkedListNode<int> last = new LinkedListNode<int>(7);
            middle.Next = last;

            //Print values of nodes in order; must feed in first node.
            LinkedListOperations.PrintList(first);
            LinkedListOperations.PrintListBackwards();
            Console.ReadLine();
        }
    }

    
}
