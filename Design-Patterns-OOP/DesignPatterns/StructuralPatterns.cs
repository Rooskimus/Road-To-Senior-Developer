using System;
using System.Collections;
using System.Collections.Generic;
// As a note to myself, I did the Design Patterns by hand in my notebook 
// before seeing how LONG this chapter is.  I moved to a digital file to
// speed up writing example code.  I also did the first structural 
// pattern, the Adapter, in my notebook.

namespace DesignPatterns
{
    // To demostrate a Composite Pattern, we will construct a sentence
    // where each item in the composite is a single character.
    //  First, we make the interface for our Print operation:
    
    // IPrintable for our Composite:
    public interface IPrintable
    {
        void Print();
    }

    public class Symbol : IPrintable
    {
        private char symbol;
        public Symbol (char c)
        {
            symbol = c;
        }
        public void Print()
        {
            Console.Write(symbol);
        }
    }

    // Some IPrintables Implementations
    public class LineBreak : IPrintable
    {
        public void Print()
        {
            Console.WriteLine();
        }
    }

    public class Space : IPrintable
    {
        public void Print()
        {
            Console.Write(" ");
        }
    }

    // The Composite Writing Class
    public class Writing : IPrintable
    {
        private List<IPrintable> printables = new List<IPrintable>();
        public void Add(IPrintable printable)
        {
            printables.Add(printable);
        }

        public void Remove(IPrintable printable)
        {
            printables.Remove(printable);
        }

        public IPrintable GetPrintable(int index)
        {
            return printables[index];
        }

        public void Print()
        {
            foreach (IPrintable printable in printables)
            {
                printable.Print();
            }
        }
    }    
}
