using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // This is a demonstration of the Observer Pattern.
    public interface IObservable
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public interface IObserver
    {
        void Update(IObservable observable);
    }

    // We're going to implement these by creating a product stock that notifies
    // observers (listeners) when the stock changes.

    public class Stock : IObservable
    {
        private List<IObserver> observers = new List<IObserver>();
        private string productName;
        private int noOfItemsInStock;
        public Stock(string name)
        {
            productName = name;
        }

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (IObserver observer in observers)
            {
                observer.Update(this);
            }
        }

        public string ProductName
        {
            get { return productName; }
        }

        public int NoOfItemsInStock
        {
            get { return noOfItemsInStock; }
            set
            {
                noOfItemsInStock = value;
                Notify(); // placing the notify in the set is what notfies the listeners of changes.
            }
        }
    }

    // Now we make some observers.

    public class Seller : IObserver
    {
        public void Update (IObservable observable)
        {
            Stock stock = (Stock)observable;
            Console.WriteLine($"Seller was notified about the stock change of {stock.ProductName} to {stock.NoOfItemsInStock} items.");
            // Instead of the cast, one could make the IObservable more specific to the 
            // issue and contain ProductName and NoOfItemsInStock properties.
            // Alternatively, just make it a generic.
        }
    }

    public class Buyer : IObserver
    {
        public void Update (IObservable observable)
        {
            Console.WriteLine("Buyer was notified about the stock change.");
        }
    }

   
}
