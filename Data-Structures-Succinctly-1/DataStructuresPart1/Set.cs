using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    public class Set<T> : IEnumerable<T>
        where T: IComparable<T>
    {
        private readonly List<T> _items = new List<T>();
        public Set()
        {

        }

        public Set(IEnumerable<T> items)
        {
            AddRange(items);
        }

        public int Count { get { return _items.Count; } } // O(1)

        public void Add(T item) // O(n)
        {  // We're implementing a no-duplicate set; could choose to have dupes.
            if (Contains(item))
            {
                throw new InvalidOperationException("Item already exists in Set");
            }
            _items.Add(item); // Simply add to our backing list.
        }

        public void AddRange(IEnumerable<T> items) // O(mn) where m is the number of items in the input enumeration and n is the number of items currently in the set.
        {                                          // This is due to needing to check each new item against existing items for duplicates.  
            foreach (T item in items) Add(item);
        }

        public bool Remove(T item) // O(n)
        {
            return _items.Remove(item); // Simply remove from backing list; will evaluate as false if not present.
        }

        public bool Contains(T item) // O(n)
        {
            return _items.Contains(item);
        }

        public Set<T> Union(Set<T> other) // O(mn) where m and n are the number of items in the provided and current sets, respectively.
        {
            // Contains all distinct items in both sets; if there's overlap the items only exist once.
            Set<T> result = new Set<T>(_items);
            foreach (T item in other._items)
            {
                if (!Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Intersection(Set<T> other) // O(mn) as above
        {
            // Contains all data that exist in both data sets only.
            Set<T> result = new Set<T>();
            foreach (T item in _items)
            {
                if (other._items.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        public Set<T> Difference(Set<T> other) // O(mn)
        {
            // Items from set n minus any items in set m (parameter).
            Set<T> result = new Set<T>(_items);
            foreach (T item in other._items)
            {
                result.Remove(item);
            }
            return result;
        }

        public Set<T> SymmetricDifference(Set<T> other) // O(mn)
        {
            // Items from both sets minus any overlapping items.
            // In other words, we want the union of both sets minus
            // the intersection of both sets.
            Set<T> union = Union(other);
            Set<T> intersection = Intersection(other);
            return union.Difference(intersection);
        }

        public bool IsSubset(Set<T> other)
        {
            Set<T> result = Difference(other);
            if (result.Count == 0) return true;
            else return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}
