using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataStructures2
{
    public class PriorityQueue<T>  // A thinly wrapped heap becomes a priority queue.
        where T: IComparable<T>
    {
        Heap<T> _heap = new Heap<T>();
        
        public void Enqueue(T value)
        {
            _heap.Add(value);
        }

        public T Dequeue()
        {
            return _heap.RemoveMax();
        }

        public void Clear()
        {
            _heap.Clear();
        }

        public int Count { get { return _heap.Count; } }
    }

    #region ClassesForDemonstratingPriorityQueue
    public class Data : IComparable<Data>
    {
        readonly DateTime _creationTime;
        public Data(string message, int priority)
        {
            _creationTime = DateTime.UtcNow;
            Message = message;
            Priority = priority;
        }
        public string Message { get; private set; }
        public int Priority { get; private set; }
        public TimeSpan Age { get { return DateTime.UtcNow.Subtract(_creationTime); } }

        public int CompareTo(Data other)
        {
            int pri = Priority.CompareTo(other.Priority);
            if (pri == 0)
            {
                pri = Age.CompareTo(other.Age);
            }
            return pri;
        }

        public override string ToString()
        {
            return string.Format("[{0} : {1}] {2}", Priority, Age.Milliseconds, Message);
        }

        public static void PriorityQueueSample()
        {
            PriorityQueue<Data> queue = new PriorityQueue<Data>();
            Random rng = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int priority = rng.Next() % 3;
                queue.Enqueue(new Data(string.Format($"This is message {i}"), priority));
                Thread.Sleep(priority);
            }
            while (queue.Count > 0)
            {
                Console.WriteLine(queue.Dequeue().ToString());
            }
        }

    }
    #endregion
}
