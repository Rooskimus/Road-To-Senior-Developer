using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            #region CompositeDemo
            // Composite Demo:
            Writing hello = new Writing();
            hello.Add(new Symbol('h'));
            hello.Add(new Symbol('e'));
            hello.Add(new Symbol('l'));
            hello.Add(new Symbol('l'));
            hello.Add(new Symbol('o'));
            // Add a line break
            LineBreak br = new LineBreak();
            hello.Add(br);
            // Print hello.
            hello.Print();
            // Remove the line break so we can re-use hello.
            hello.Remove(br);
            // Create the writing 'bye'.
            Writing bye = new Writing();
            bye.Add(new Symbol('b'));
            bye.Add(new Symbol('y'));
            bye.Add(new Symbol('e'));
            // Create a writing consisting of the writings.
            // hello and bye separated by a space.
            // This is where things get composite; a list of a list.
            Writing helloBye = new Writing();
            helloBye.Add(hello);
            helloBye.Add(new Space());
            helloBye.Add(bye);
            // Print all the Printables
            helloBye.Print();
            Console.ReadKey();
            #endregion


        }
    }
}
