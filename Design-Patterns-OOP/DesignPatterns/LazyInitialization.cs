using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // Create a fake process with a heavy load time
    public class Something
    {
        public Something()
        {
            Thread.Sleep(5000);
        }
        public void PrintUse()
        {
            Console.WriteLine("Something is used.");
        }
    }

    public class LazyDemo
    {
        public void RunDemo()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Lazy<Something> lazy = new Lazy<Something>();
            sw.Stop();
            Console.WriteLine($"Initializing the Lazy<Something> cost: {sw.ElapsedMilliseconds} ms.");
            sw.Restart();
            lazy.Value.PrintUse();
            sw.Stop();
            Console.WriteLine($"Using the Lazy<Something> cost: {sw.ElapsedMilliseconds} ms.");
            Console.ReadLine();
        }
    }
}
