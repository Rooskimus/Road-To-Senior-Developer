using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures2
{
    internal class SkipListNode<T>
    {
        public SkipListNode(T value, int height)
        {
            Value = value;
            Next = new SkipListNode<T>[height];
        }
        public SkipListNode<T>[] Next { get; private set; } // The number of items is the height of the links.
        public T Value { get; private set; }

    }
    public class SkipList<T> : ICollection<T>
        where T : IComparable<T>
    {
        private readonly Random _rand = new Random(); // Used to determine height of node links
        private SkipListNode<T> _head; // Data node which starts the list.
        private int _levels = 1; // Always at least one level of depth (base list).
        private int _count = 0; // Numer of items currently in the list.

        public int Count  {get{return _count;} }
        public bool IsReadOnly { get { return false; } }

        //public SkipList
        //{

        //}

        public void Add(T item) // O(log n) - quite good!
        {
            int level = PickRandomLevel();
            SkipListNode<T> newNode = new SkipListNode<T>(item, level + 1); // Create new node with new value and level + 1 because our PickRandomLevel gives back 0's and we need a minimum of 1.
            SkipListNode<T> current = _head; // Start at the head.
            for (int i = _levels-1; i >= 0; i--) // Going backward down the indicies in current.Next; we do levels-1 because array indexes are one less than our levels.
            {
                while(current.Next[i] != null) // Until the there is no node at level i of the Next array...
                {
                    if (current.Next[i].Value.CompareTo(item) > 0) break; // If the item is smaller, we've found where we need to insert so break.
                    current = current.Next[i]; // If not, let's check the next node.
                }

                if (i <= level) // Once you've found the next node is bigger than the item, insert before it.
                {
                    newNode.Next[i] = current.Next[i]; // Assign the bigger value as the new node's next
                    current.Next[i] = newNode; // Assign the new node as the current node's next.
                }
                // Repeat this process for any lower levels.
            }

            _count++; // Count the item we've inserted.
        }

        private int PickRandomLevel()
        {
            int rand = _rand.Next();
            int level = 0;
            // We're using the bit mask of a random integer to determine if the max
            // level should increase by one or not.
            // Say the 8 LSBs of the int are 00101100. In that case, when the
            // LSB is compared against 1, it tests to 0 and the while loop is never
            // entered so the level stays the same. That should happen 1/2 of the time.
            // Later, if the _levels field is set to 3 and the rand value is 01101111,
            // the while loop will run 4 times and on the last iteration will
            // run another 4 times, creating a node with a skip list height of 4. This should
            // only happen 1/16 of the time.
            while ((rand & 1) == 1) // Because & 1 will only produce a result of 1 on bits that have a 1 at the end, this works.
            {
                if (level == _levels) // If we create a level that matches the current highest level, we make more room at the top.
                {
                    _levels++; // Raise the level ceiling.
                    break;  // The level will be returned as-is below.
                }
                rand >>= 1; // Shift binary digits over to check again.
                level++; // increase our level
            }
            return level;
        }

        public void Clear() // O(1)
        {
            // Reinitialize _head and set count to zero:
            _head = new SkipListNode<T>(default(T), 32 + 1);
            _count = 0;
        }

        public bool Contains(T item) // O(log n)
        {
            SkipListNode<T> cur = _head;
            for (int i = _levels -1; i >= 0; i--)
            {
                while (cur.Next[i] != null)
                {
                    int cmp = cur.Next[i].Value.CompareTo(item);
                    if (cmp > 0) // If cur.Next[i] value is larger
                    {
                        // Go down a level and search again.
                        break;
                    }
                    if (cmp == 0)
                    {
                        // We found it.
                        return true;
                    }
                    cur = cur.Next[i];  // If item is larger, try next node at this level.
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex) // O(n)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            int offset = 0;
            foreach (T item in this)
            {
                array[arrayIndex + offset++] = item;
            }
        }

        public bool Remove(T item) // O(log n)
        {
            SkipListNode<T> cur = _head;
            bool removed = false;
            // Walk down each level in the list (make big jumps).
            for (int level = _levels - 1; level >= 0; level--)
            {
                // While we're not at the end of the list:
                while (cur.Next[level] != null)
                {
                    if (cur.Next[level].Value.CompareTo(item) == 0) // If we found our node,
                    {
                        // remove the node,
                        cur.Next[level] = cur.Next[level].Next[level];
                        removed = true;
                        // and go to do the next level (where we'll find our node again
                        // if we're not at the bottom level
                        break; // Sends us back to the for loop, which will take us down a level.
                    }
                    // If we went too far the item is not indexed at this level, so go down a level.
                    if (cur.Next[level].Value.CompareTo(item) > 0)
                    {
                        break;
                    }
                    cur = cur.Next[level];
                }
            }
            if (removed) _count--;
            return removed;
        }

        public IEnumerator<T> GetEnumerator()
        {
            SkipListNode<T> cur = _head.Next[0];
            while (cur != null)
            {
                yield return cur.Value;
                cur = cur.Next[0];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
