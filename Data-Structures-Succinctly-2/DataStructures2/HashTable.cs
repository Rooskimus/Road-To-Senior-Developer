using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures2
{
    #region HashTableClass
    public class HashTable<TKey, TValue>
    {
        #region HashAlgorithms
        private static int Djb2(string input) // A solid string hashing algorithm.
        {
            int hash = 5381;
            foreach (int c in input.ToCharArray())
            {
                unchecked // Unchecked allows for overflow if integer gets too big (i.e. if you go to big you come around the other side as a negative)
                {
                    hash = ((hash << 5) + hash) + c; 
                    // Left-shifting hash essentially multiplies it by 32, + hash makes a total of 33*hash.  Then add the character value.
                    // This is done for each character in the string.
                }
            }
            return hash;
        }

        private static int FoldingHash(string input) // Treats each 4 characters as an integer, so aaaabbbb and bbbbaaaa has differently.
        {
            int hashValue = 0;
            int startIndex = 0;
            int currentFourBytes;
            do
            {
                currentFourBytes = GetNextBytes(startIndex, input);
                unchecked { hashValue += currentFourBytes; }
                startIndex += 4;
            } while (currentFourBytes != 0);
            return hashValue;
        }

        private static int GetNextBytes(int startIndex, string str) // Gets next four bytes of string converted to an integer.  If not enough characters, 0 is used.
        {
            int currentFourBytes = 0;
            currentFourBytes += GetByte(str, startIndex);
            currentFourBytes += GetByte(str, startIndex + 1);
            currentFourBytes += GetByte(str, startIndex + 2);
            currentFourBytes += GetByte(str, startIndex + 3);
            return currentFourBytes;
        }

        private static int GetByte(string str, int index)
        {
            if (index < str.Length)
            {
                int value = (int)str[index];
                return value;
            }
            return 0;
        }
        #endregion

        //If the array exceeds this fill percentage, it will grow.
        const double _fillFactor = 0.75;

        // The maximum number of items to store before growing.
        // This is just a cached value of the fill factor calculation.
        int _maxItemsAtCurrentSize;

        // The number of items in the hash table.
        int _count;

        // The array where items are stored.
        HashTableArray<TKey, TValue> _array;

        // Constructs a hash table with default capacity.
        public HashTable() : this(1000)
        {

        }

        // Constructs a hash table with specified capacity.
        public HashTable(int initialCapacity)
        {
            if (initialCapacity < 1)
            {
                throw new ArgumentOutOfRangeException("initialCapacity");
            }
            _array = new HashTableArray<TKey, TValue>(initialCapacity);

            // When the count exceeds this value, the next Add will cause the array to grow.
            _maxItemsAtCurrentSize = (int)(initialCapacity * _fillFactor) + 1;
        }

        public void Add(TKey key, TValue value) // O(1) on average.  O(n+1) when array growth occrus.
        {
            // Adds layer of abstraction over HashTableArray class.
            // If we are at capacity, the array needs to grow.
            if (_count >= _maxItemsAtCurrentSize)
            {
                // Allocate a larger array
                HashTableArray<TKey, TValue> largerArray = 
                    new HashTableArray<TKey, TValue>(_array.Capacity * 2);
                // Re-add each item to the new array
                foreach (HashTableNodePair<TKey, TValue> node in _array.Items)
                {
                    largerArray.Add(node.Key, node.Value);
                }
                // The larger array is now the hash table storage.
                _array = largerArray;
                // Update the new max items cached value.
                _maxItemsAtCurrentSize = (int)(_array.Capacity * _fillFactor + 1);
            }
            _array.Add(key, value);
            _count++;

        }

        public bool Remove(TKey key) // O(1) on average; O(n) worst case.
        {
            bool removed = _array.Remove(key);
            if (removed)
            {
                _count--;
            }
            return removed;
        }

        /// <summary>
        /// Gets and sets the value with the specified key.  An ArgumentException
        /// is thrown if the key does not already exist in the hash table.
        /// </summary>
        /// <param name="key">The key of the value to retrieve.</param>
        /// <returns>The value associated with the specified key.</returns>
        public TValue this[TKey key] // O(1) on average; O(n) worst case.
        {
            get
            {
                TValue value;
                if (!_array.TryGetValue(key, out value))
                {
                    throw new ArgumentException("key");
                }
                return value;
            }
            set
            {
                _array.Update(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value) // O(1) on average; O(n) worst case.
        {
            return _array.TryGetValue(key, out value);
        }

        public bool ContainsKey(TKey key) // O(1) on average; O(n) worst case.
        {
            TValue value;
            return _array.TryGetValue(key, out value);
        }

        public bool ContainsValue(TValue value) // O(n)
        {
            foreach (TValue foundValue in _array.Values)
            {
                if (value.Equals(foundValue)) return true;
            }
            return false;
        }

        public IEnumerable<TKey> Keys // O(n)
        {
            get
            {
                foreach (TKey key in _array.Keys)
                {
                    yield return key;
                }
            }
        }

        public IEnumerable<TValue> Values // O(n)
        {
            get
            {
                foreach (TValue value in _array.Values)
                {
                    yield return value;
                }
            }
        }

        public void Clear() // O(1)
        {
            _array.Clear();
            _count = 0;
        }

        public int Count // Note the capacity of the backing array will never be the count due to a fill factor of less than 1.
        {
            get
            {
                return _count;
            }
        }
    }
    #endregion

    // To combat collisions, we need the following:
    #region HashTableNodePair
    public class HashTableNodePair<TKey, TValue>
    {
        public HashTableNodePair(TKey key, TValue value) // Constructs key/value pair for storage in the hash table.
        {
            Key = key;
            Value = value;
        }

        public TKey Key { get; private set; }
        public TValue Value { get; set; }
    }
    #endregion

    #region HashTableArrayNode
    internal class HashTableArrayNode<TKey, TValue> // This list contains the actual data in the hash table.  It chains together data collisions.
    {
        LinkedList<HashTableNodePair<TKey, TValue>> _items;

        /// <summary>
        /// Adds the key/value pair to the node. If the key already exists in the
        /// list, an ArgumentException will be thrown.
        /// </summary>
        /// <param name="key">The key of the item being added.</param>
        /// <param name="value">The value of the item being added.</param>
        public void Add(TKey key, TValue value) // O(1)
        {
            // Lazy init the linked list.
            if (_items == null)
            {
                _items = new LinkedList<HashTableNodePair<TKey, TValue>>();
            }
            else
            {
                // Multiple items might collide and exist in this list, but each keys should only be in the list once.
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        throw new ArgumentException("The collection already contains the key.");
                    }
                }
            }
            // If we made it this far, add the item.
            _items.AddFirst(new HashTableNodePair<TKey, TValue>(key, value));
        }

        // <summary>
        /// Updates the value of the existing key/value pair in the list.
        /// If the key does not exist in the list, an ArgumentException
        /// will be thrown.
        /// </summary>
        /// <param name="key">The key of the item being updated.</param>
        /// <param name="value">The updated value.</param>
        public void Update(TKey key, TValue value) // O(n) where n is the number of items in the linked list; O(1) when no collisions.
        {
            bool updated = false;
            if (_items != null)
            {
                // Check each item in the list for the specified key.
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        // Update the value
                        pair.Value = value;
                        updated = true;
                        break;
                    }
                }
            }
            if (!updated)
            {
                throw new ArgumentException("The collection does not contain the key");
            }
        }

        /// <summary>
        /// Finds and returns the value for the specified key.
        /// </summary>
        /// <param name="key">The key whose value is sought.</param>
        /// <param name="value">The value associated with the specified key.</param>
        /// <returns>True if the value was found, false otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value) // O(n) when collision; O(1) when no collision
        {
            value = default(TValue);
            bool found = false;
            if (_items != null)
            {
                foreach (HashTableNodePair<TKey, TValue> pair in _items)
                {
                    if (pair.Key.Equals(key))
                    {
                        value = pair.Value;
                        found = true;
                        break;
                    }
                }
            }

            return found;
        }

        public bool Remove(TKey key)            // O(n) if collision; O(1) if no collision;
        {
            bool removed = false;
            if (_items != null)
            {
                LinkedListNode<HashTableNodePair<TKey, TValue>> current = _items.First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        _items.Remove(current);
                        removed = true;
                        break;
                    }
                    current = current.Next;
                }
            }

            return removed;
        }

        /// <summary>
        /// Removes all the items from the list.
        /// </summary>
        public void Clear() // O(1)
        {
            if (_items != null)
            {
                _items.Clear();
            }
        }

        public IEnumerable<TValue> Values // O(1)
        { get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> node in _items)
                    {
                        yield return node.Value;
                    }
                }
            }        
        }

        public IEnumerable<TKey> Keys // O(1)
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> node in _items)
                    {
                        yield return node.Key;
                    }
                }
            }
        }

        public IEnumerable<HashTableNodePair<TKey, TValue>> Items // O(1)
        {
            get
            {
                if (_items != null)
                {
                    foreach (HashTableNodePair<TKey, TValue> node in _items)
                    {
                        yield return node;
                    }
                }
            }
        }
    }
    #endregion

    // Backing array for the HashTable class.
    // Finds appropriate backing array index and defers to HashTableArrayNode class.
    #region HashTableArray
    class HashTableArray<TKey, TValue>
    {
        HashTableArrayNode<TKey, TValue>[] _array;

        public HashTableArray(int capacity) // Constructs new array with given capacity.
        {
            _array = new HashTableArrayNode<TKey, TValue>[capacity];
        }

        public void Add(TKey key, TValue value) // O(1)
        {
            // Lazily allocates HashTableArrayNode instance so that only
            // hash table entries that hold a value allocate an instance.
            int index = GetIndex(key);
            HashTableArrayNode<TKey, TValue> nodes = _array[index];
            if (nodes == null)
            {
                nodes = new HashTableArrayNode<TKey, TValue>();
                _array[index] = nodes;
            }
            nodes.Add(key, value);
        }

        /// <summary>
        /// Updates the value of the existing key/value pair in the node array.
        /// If the Key does not exist, an ArgumentException is thrown.
        /// </summary>
        /// <param name="key">The key of the item being updated.</param>
        /// <param name="value">The updated value</param>
        public void Update(TKey key, TValue value) // O(n) where n is the number of items in the HashTableNodeArray (i.e. where a collision has occured); typically O(1)
        {
            HashTableArrayNode<TKey, TValue> nodes = _array[GetIndex(key)];
            if (nodes == null)
            {
                throw new ArgumentException("The key does not exist in the hash table.");
            }
            nodes.Update(key, value);
        }


        /// <summary>
        /// Removes the item from the node array whose key matches
        /// the specified key.
        /// </summary>
        /// <param name="key">The key of the item to remove.</param>
        /// <returns>True if the item was removed; false otherwise.</returns>
        public bool Remove(TKey key) //O(n) where n is the number of items in the HashTableNodeArray (i.e. where a collision has occured); typically O(1)
        {
            HashTableArrayNode<TKey, TValue> nodes = _array[GetIndex(key)];
            if (nodes != null)
            {
                return nodes.Remove(key);
            }
            return false;
        }

        /// <summary>
        /// Finds and returns the value for the specified key.
        /// </summary>
        /// <param name="key">The key whose value is sought.</param>
        /// <param name="value">The value associated with the specified key.</param>
        /// <returns>True if the value is found; false otherwise.</returns>
        public bool TryGetValue(TKey key, out TValue value) // O(n) where n is the number of items in the HashTableNodeArray (i.e. where a collision has occured); typically O(1)
        {
            HashTableArrayNode<TKey, TValue> nodes = _array[GetIndex(key)];
            if (nodes != null)
            {
                return nodes.TryGetValue(key, out value);
            }
            value = default(TValue);
            return false;
        }

        public int Capacity { get { return _array.Length; } }

        public void Clear() // O(n) where n is the number of nodes in the table that contain data.
        {
            foreach (HashTableArrayNode<TKey, TValue> node in _array.Where(node => node != null))
            {
                node.Clear();
            }
        }

        public IEnumerable<TValue> Values // O(n), where n is the total number of items contained in the hash table array and all of its contained nodes.
        {
            get
            {
                foreach (HashTableArrayNode<TKey, TValue> node in 
                    _array.Where(node => node != null))
                {
                    foreach (TValue value in node.Values)
                    {
                        yield return value;
                    }
                }
            }
        }

        public IEnumerable<TKey> Keys // O(n), where n is the total number of items contained in the hash table array and all of its contained nodes.
        {
            get
            {
                foreach (HashTableArrayNode<TKey, TValue> node in
                    _array.Where(node => node != null))
                {
                    foreach (TKey key in node.Keys)
                    {
                        yield return key;
                    }
                }
            }
        }

        public IEnumerable<HashTableNodePair<TKey, TValue>> Items // O(n), where n is the total number of items contained in the hash table array and all of its contained nodes.
        {
            get
            {
                foreach (HashTableArrayNode<TKey, TValue> node in
                    _array.Where(node => node != null))
                {
                    foreach (HashTableNodePair<TKey, TValue> pair in node.Items)
                    {
                        yield return pair;
                    }
                }
            }
        }

        // Maps a key to the array index based on the hash code.

        private int GetIndex(TKey key) //O(1)
        {
            return Math.Abs(key.GetHashCode() % Capacity);
        }
    }
    #endregion
}
