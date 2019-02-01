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
            // The guts of each demo are commented out so I could test just
            // one at a time.

            #region CompositeDemo
            //// Composite Demo:
            //Writing hello = new Writing();
            //hello.Add(new Symbol('h'));
            //hello.Add(new Symbol('e'));
            //hello.Add(new Symbol('l'));
            //hello.Add(new Symbol('l'));
            //hello.Add(new Symbol('o'));
            //// Add a line break
            //LineBreak br = new LineBreak();
            //hello.Add(br);
            //// Print hello.
            //hello.Print();
            //// Remove the line break so we can re-use hello.
            //hello.Remove(br);
            //// Create the writing 'bye'.
            //Writing bye = new Writing();
            //bye.Add(new Symbol('b'));
            //bye.Add(new Symbol('y'));
            //bye.Add(new Symbol('e'));
            //// Create a writing consisting of the writings.
            //// hello and bye separated by a space.
            //// This is where things get composite; a list of a list.
            //Writing helloBye = new Writing();
            //helloBye.Add(hello);
            //helloBye.Add(new Space());
            //helloBye.Add(bye);
            //// Print all the Printables
            //helloBye.Print();
            //Console.ReadKey();
            #endregion

            #region DecoratorDemo
            //IMessage msg = new Message();
            //msg.Msg = "Hello";
            //// The variable msg is of the base class Message stored in type
            //// IMessage so it is accessible to the decorators.
            //IMessage decorator = new EmailDecorator(
            //    new FaxDecorator(
            //        new ExternalSystemDecorator(msg)));
            //// When run, the above processes from the inside out, sending 
            //// external, then fax, then email.  The database call from 
            //// the base Message class went first because the decorators all
            //// inherit the "innerMessage" behavor from the BaseDecorator class
            //// in combination with using base.Process() before their own processes.
            //decorator.Process();
            //Console.WriteLine();
            //decorator = new EmailDecorator(msg);
            //decorator.Msg = "Bye";
            //decorator.Process();
            //Console.ReadKey();

            #endregion

            #region ObserverDemo

            //Stock stock = new Stock("Cheese");
            //stock.NoOfItemsInStock = 10;
            //// Register observers
            //Seller seller = new Seller();
            //stock.Attach(seller);
            //Buyer buyer = new Buyer();
            //stock.Attach(buyer);
            //stock.NoOfItemsInStock = 5;
            //Console.ReadLine();

            #endregion

            #region StrategyDemo

            //Game game = new Game();
            //game.RunGame();

            #endregion

            #region LazyDemo

            //LazyDemo demo = new LazyDemo();
            //demo.RunDemo();

            #endregion
        }
    }
}
