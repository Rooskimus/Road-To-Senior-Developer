using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures2
{
    public class BTree<T> : ICollection<T>
        where T: IComparable<T>
    {
        BTreeNode<T> root = null;

        const int MinimumDegree = 2;

        #region Add
        public void Add(T value) // O(log n)
        {
            if (root == null)
            {
                root = new BTreeNode<T>(null, true, MinimumDegree, new[] { value }, new BTreeNode<T>[] { });
            }
            else
            {
                if (root.Full)
                {
                    root = root.SplitFullRootNode();
                }
                InsertNonFull(root, value);
            }
            Count++;
        }

        private void InsertNonFull(BTreeNode<T> node, T value)
        {
            if (node.Leaf)
            {
                node.InsertKeyToLeafNode(value);
            }
            else
            {
                int index = node.Values.Count - 1;
                while (index >= 0 && value.CompareTo(node.Values[index]) < 0)
                {
                    index--;
                }
                index++;
                if (node.Children[index].Full)
                {
                    node.SplitFullChild(index);
                    if (value.CompareTo(node.Values[index]) > 0)
                    {
                        index++;
                    }
                }
                InsertNonFull(node.Children[index], value);
            }
        }

        #endregion // O(log n)

        #region Remove
        public bool Remove(T value) // O(log n)
        {
            bool removed = false;
            if (Count > 0)
            {
                removed = RemoveValue(root, value);
                if (removed)
                {
                    Count--;
                    if (Count == 0)
                    {
                        root = null;
                    }
                    else if (root.Values.Count == 0)
                    {
                        root = root.Children[0];
                    }
                }
            }
            return removed;
        }

        internal static bool RemoveValue(BTreeNode<T> node, T value)
        {
            if (node.Leaf)
            {
                // Deletion case 1...
                // By the time we are in a leaf node, we have either pushed down
                // values such that the leaf node has minimum degree children
                // and can therefore have one node removed, OR the root node is
                // also a leaf node and we can freely violate the minimum rule.
                return node.DeleteKeyFromLeafNode(value);
            }
            int valueIndex;
            if(TryGetIndexOf(node, value, out valueIndex))
            {
               // LEFT OFF HERE!
            }
        }

        internal static void RotateAndPushDown(BTreeNode<T> node, int childIndex, int indexOfMaxSibling)
        {
            int valueIndex;
            if (childIndex < indexOfMaxSibling)
            {
                valueIndex = childIndex;
            }
            else
            {
                valueIndex = childIndex - 1;
            }
            if (indexOfMaxSibling > childIndex)
            {
                // We are moving the leftmost key from the right sibling into the parent
                // and pushing the parent down into the child.
                //
                //     [6      10]
                //  [1]  [7 8 9] [11]
                //
                // Deleting something less than 6.
                //
                //       [7   10]
                //    [1 6] [8 9] [11]
                // Grab the 7.
                T valueToMoveToX = node.Children[indexOfMaxSibling].Values.First();
                // Get 7's left child if it has one (not a leaf).
                BTreeNode<T> childToMoveToNode = node.Children[indexOfMaxSibling].Leaf ? null :
                    node.Children[indexOfMaxSibling].Children.First();
                // Record the 6 (the push down value)
                T valueToMoveDown = node.Values[valueIndex];
                // Move 7 into the parent.
                node.ReplaceValue(valueIndex, valueToMoveToX);
                // Move 6 into the child.
                node.Children[childIndex].AddEnd(valueToMoveDown, childToMoveToNode);
                // Remove the first value and child from the sibling now that they've been moved.
                node.Children[indexOfMaxSibling].RemoveFirst();
            }
            else
            {
                // We are moving the rightmost key from the left sibling into the parent
                // and pushing the parent down into the child.
                //
                //     [6      10]
                //  [1]  [7 8 9] [11]
                //
                // Deleting something greater than 10.
                //
                //     [6     9]
                //  [1]  [7 8] [10, 11]
                // Grab the 9.
                T valueToMoveToX = node.Children[indexOfMaxSibling].Values.Last();
                // Get 9's right child if it has one (not a leaf node).
                BTreeNode<T> childToMoveToNode = node.Children[indexOfMaxSibling].Leaf ? null :
                    node.Children[indexOfMaxSibling].Children.Last();
                // Record the 10 (the push down value)
                T valueToMoveDown = node.Values[valueIndex];
                // Move 9 into the parent.
                node.ReplaceValue(valueIndex, valueToMoveToX);
                // Move 10 into the child.
                node.Children[childIndex].AddFront(valueToMoveDown, childToMoveToNode);
                // Remove the last value and child from the sibling now that they've been moved.
                node.Children[indexOfMaxSibling].RemoveLast();
            }
        }

        internal static void FindPotentialPath(BTreeNode<T> node, T value, out int valueIndex, out int childIndex)
        {
            // We want to find out which child the value we are searching for (value)
            // would be in if the value were in the tree.
            childIndex = node.Children.Count - 1;
            valueIndex = node.Values.Count - 1;
            // Start at the rightmost child and value indexes and work
            // backward until we are at less than the value we want.
            while (valueIndex > 0)
            {
                int compare = value.CompareTo(node.Values[valueIndex]);
                if (compare > 0)
                {
                    break;
                }
                childIndex--;
                valueIndex--;
            }
            // If we make it all the way to the last value...
            if (valueIndex == 0)
            {
                // If the value we are searching for is less than the first
                // value in the node, then the child is the 0 index child,
                // not the 1 index.
                if (value.CompareTo(node.Values[valueIndex]) < 0)
                {
                    childIndex--;
                }
            }
        }

        // Returns the index (to the left or right) of the child node
        // that has the most values in it.
        //
        // Example
        //
        //     [3      7]
        // [1 2] [4 5 6] [8 9]
        //
        // If we pass in the [3 7] node with index 0, the left child [1 2]
        // and right child [4 5 6] would be checked and the index 1 for child
        // node [4 5 6] would be returned.
        //
        // If we checked [3 7] with index 1, the left child [4 5 6] and the
        // right child [8 9] would be checked and the value 1 would be returned.
        private static int GetIndexOfMaxSibling(int index, BTreeNode<T> node)
        {
            int indexOfMaxSibling = -1;
            BTreeNode<T> leftSibling = null;
            if (index > 0)
            {
                leftSibling = node.Children[index - 1];
            }
            BTreeNode<T> rightSibling = null;
            if (index + 1 < node.Children.Count)
            {
                rightSibling = node.Children[index + 1];
            }
            if (leftSibling != null || rightSibling != null) // If one or the other is not null...
            {
                if (leftSibling != null && rightSibling != null) // And if both are not null...
                {
                    indexOfMaxSibling = leftSibling.Values.Count > rightSibling.Values.Count ?
                        index - 1 : index + 1; // assign larger count as correct index.
                }
                else
                {  // Else we assign the index that isn't null as the correct index.
                    indexOfMaxSibling = leftSibling != null ? index - 1 : index + 1;
                }
            }
            return indexOfMaxSibling;
        }

        // Gets the index of the specified value from the current node's values,
        // returning true if the value was found, else false if not.
        private static bool TryGetIndexOf(BTreeNode<T> node, T value, out int valueIndex)
        {
            for (int index = 0; index < node.Values.Count; index++)
            {
                if (value.CompareTo(node.Values[index]) == 0)
                {
                    valueIndex = index;
                    return true;
                }
            }
            valueIndex = -1;
            return false;
        }

        // Finds the value of the successor of a specific value in a node.
        // Basically, it will search through the subsequent children nodes in
        // the tree until it hits the leaf level and return the next value in
        // the sequence that follows the given value.
        //    [3  6]
        // [1 2] [4 5] [7 8]
        // The successor of 3 is 4.
        private static T FindSuccessor(BTreeNode<T> node, int index)
        {
            node = node.Children[index + 1];
            while (!node.Leaf)
            {
                node = node.Children.First();
            }
            return node.Values.First();
        }

        // Similar to FindSuccessor but finds number prior in sequence.
        // in the example above, the predecessor of 3 is 2.
        private static T FindPredecessor(BTreeNode<T> node, int index)
        {
            node = node.Children[index];
            while (!node.Leaf)
            {
                node = node.Children.Last();
            }
            return node.Values.Last();
        }

        #endregion  

        public bool Contains(T value);

        public void Clear();

        public void CopyTo(T[] array, int arrayIndex);

        public int Count { get; private set; }

        public bool IsReadOnly { get; }

        public IEnumerator<T> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator();
    }

    #region BTreeNode
    internal class BTreeNode<T>
        where T: IComparable<T>
    {
        private readonly List<T> _values;
        private readonly List<BTreeNode<T>> _children;
        internal BTreeNode(BTreeNode<T> parent, bool leaf, int minimumDegree, T[] values, BTreeNode<T>[] children)
        {
            ValidatePotentialState(parent, leaf, minimumDegree, values, children);
            Parent = parent;
            Leaf = leaf;
            MinimumDegree = minimumDegree;
            _values = new List<T>(values);
            _children = new List<BTreeNode<T>>(children);
        }

        /// <summary>
        /// Returns true if the node has (2 * T - 1) nodes, false otherwise.
        /// </summary>
        internal bool Full { get; }

        /// <summary>
        /// True if the node is a leaf node, false otherwise.
        /// </summary>
        internal bool Leaf { get; private set; }

        /// <summary>
        /// The node's values.
        /// </summary>
        internal IList<T> Values { get; }

        /// <summary>
        /// The node's children.
        /// </summary>
        internal IList<BTreeNode<T>> Children { get; }

        /// <summary>
        /// The minimum degree of the node is the minimum degree of the tree.
        /// If the minimum degree is T then the node must have at least T - 1
        /// values, but no more than 2 * T - 1.
        /// </summary>
        internal int MinimumDegree { get; private set; }

        /// <summary>
        /// The parent of the current node (or null if the root node).
        /// </summary>
        internal BTreeNode<T> Parent { get; set; }

        /// <summary>
        /// Splits a full child node, pulling the split value into the current node.
        /// </summary>
        /// <param name="indexOfChildToSplit">The child to split.</param>
        internal void SplitFullChild(int indexOfChildToSplit)
        {
            // Splits a child node by pulling the middle node up from it into the current parent node.
            //           [3 9]
            //       /     |     \         
            // [1 2]  [4 5 6 7 8]  [10 11]
            //
            // Splitting [4 5 6 7 8] pulls 6 up to its parent.
            //
            //           [3 6 9]
            //        /    | |    \         
            //  [1 2]  [4 5] [7 8]  [10 11]

            int medianIndex = Children[indexOfChildToSplit].Values.Count / 2;
            bool isChildLeaf = Children[indexOfChildToSplit].Leaf;
            // Get the value 6.
            T valueToPullUp = Children[indexOfChildToSplit].Values[medianIndex];
            // Build node [4 5].
            BTreeNode<T> newLeftSide = new BTreeNode<T>(this, isChildLeaf, MinimumDegree,
                Children[indexOfChildToSplit].Values.Take(medianIndex).ToArray(), // This adds the values from beginning of node value list to just before index.
                Children[indexOfChildToSplit].Children.Take(medianIndex + 1).ToArray()); // I believe this assings the node [4 5] three null children (one before 4, between 4 and 5, and after 5)?
            // Build node [7 8].
            BTreeNode<T> newRightSide = new BTreeNode<T>(this, isChildLeaf, MinimumDegree,
                Children[indexOfChildToSplit].Values.Skip(medianIndex + 1).ToArray(), // This adds the values from just after the median index to the end.
                Children[indexOfChildToSplit].Children.Skip(medianIndex + 1).ToArray());
            // Add 6 to [3 9], making [3, 9, 6].
            _values.Insert(indexOfChildToSplit, valueToPullUp);
            // Sanity check.
            ValidateValues();
            // Remove the child that pointed to the old node [4 5 6 7 8].
            _children.RemoveAt(indexOfChildToSplit);
            // Add the child pointing to [4 5] and [7 8].
            _children.InsertRange(indexOfChildToSplit, new[] { newLeftSide, newRightSide });
        }

        /// <summary>
        /// Splits the full root node into a new root and two children.
        /// </summary>
        /// <returns>The new root node.</returns>
        internal BTreeNode<T> SplitFullRootNode()
        {
            // Let's say the whole tree is the root node of [1 2 3 4 5].
            // So pull out 3 and split the left and right side.
            //     [3]
            //     / \      
            // [1 2] [4 5]
            //
            //Find the index of the value to pull up: 3.
            int medianIndex = Values.Count / 2;
            // Now get the 3.
            T rootValue = Values[medianIndex];
            // Build the new root node (empty).
            BTreeNode<T> result = new BTreeNode<T>(Parent, false, MinimumDegree, new T[0], new BTreeNode<T>[0]);
            // Build the left node [1 2]
            BTreeNode<T> newLeftSide = new BTreeNode<T>(result, Leaf, MinimumDegree,
                Values.Take(medianIndex).ToArray(),
                Children.Take(medianIndex + 1).ToArray());
            // Build the right node [4 5]
            BTreeNode<T> newRightSide = new BTreeNode<T>(result, Leaf, MinimumDegree,
                Values.Skip(medianIndex + 1).ToArray(),
                Children.Skip(medianIndex + 1).ToArray());
            // Add 3 to the root node.
            result._values.Add(rootValue);
            // Add the left child [1 2]
            result._children.Add(newLeftSide);
            // Add the right child [4 5]
            result._children.Add(newRightSide);
            return result;
        }

        /// <summary>
        /// Insert the specified value into the non-full leaf node.
        /// </summary>
        internal void InsertKeyToLeafNode(T value)
        {
            // Leaf validation is done by caller.
            if (!Leaf)
            {
                throw new InvalidOperationException("Unable to insert into a non-leaf node.");
            }
            // Non-full validation done by caller.
            if (Full)
            {
                throw new InvalidOperationException("Unable to insert into a full node.");
            }

            // Find the indext to insert at.
            int index = 0;
            while (index < Values.Count && value.CompareTo(Values[index]) > 0)
            {  // While our index is less than the total number of values and is greater than the value at the current index...
                index++;  // Move to the next index.
            }
            // Insert.
            _values.Insert(index, value); // If the given value is greater than all in the leaf, or we find one larger, we insert it appropriately.

            //Sanity check.
            ValidateValues();
        }

        /// <summary>
        /// Removes the specified value from the leaf node if it exists.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>True if a value was removed, false otherwise.</returns>
        internal bool DeleteKeyFromLeafNode(T value)
        {
            if (!Leaf)
            {
                throw new InvalidOperationException("Unable to leaf-delete from a non-leaf node.");
            }
            return _values.Remove(value);
        }

        /// <summary>
        /// Replaces the value at the specified index with the new value.
        /// </summary>
        internal void ReplaceValue(int valueIndex, T newValue)
        {
            _values[valueIndex] = newValue;
            ValidateValues();
        }

        //     [3     6]
        // [1 2] [4 5] [7 8]
        // becomes
        //           [6]
        // [1 2 3 4 5] [7 8]
        internal BTreeNode<T> PushDown(int valueIndex)
        {
            List<T> values = new List<T>();
            // [1 2] -> [1 2]
            values.AddRange(Children[valueIndex].Values);
            // [3] -> [1 2 3]
            values.Add(Values[valueIndex]);
            // [4 5] -> [1 2 3 4 5]
            values.AddRange(Children[valueIndex + 1].Values);
            List<BTreeNode<T>> children = new List<BTreeNode<T>>();
            children.AddRange(Children[valueIndex].Children);
            children.AddRange(Children[valueIndex + 1].Children);
            BTreeNode<T> newNode = new BTreeNode<T>(this, Children[valueIndex].Leaf, MinimumDegree,
                values.ToArray(), children.ToArray());
            // [3 6] -> [6]
            _values.RemoveAt(valueIndex);
            // [c1 c2 c3] -> [c2 c3]
            _children.RemoveAt(valueIndex);
            // [c2 c3] -> [newNode c3]
            _children[valueIndex] = newNode;

            return newNode;
        }


        /// <summary>
        /// Adds the specified value to the front of the values and, if non-null,
        /// adds the specified value to the children.
        /// </summary>
        internal void AddFront(T newValue, BTreeNode<T> bTreeNode)
        {
            _values.Insert(0, newValue);
            ValidateValues();
            if (bTreeNode != null)
            {
                _children.Insert(0, bTreeNode);
            }
        }

        /// <summary>
        /// Adds the specified value to the node and, if the specified node is non-null,
        /// adds the node to the children.
        /// </summary>
        internal void AddEnd(T valueToPushDown, BTreeNode<T> bTreeNode)
        {
            _values.Add(valueToPushDown);
            ValidateValues();
            if (bTreeNode != null)
            {
                _children.Add(bTreeNode);
            }
        }

        /// <summary>
        /// Removes the first value and child (if applicable).
        /// </summary>
        internal void RemoveFirst()
        {
            _values.RemoveAt(0);
            if (!Leaf)
            {
                _children.RemoveAt(0);
            }
        }

        /// <summary>
        /// Removes the last value and child (if applicable).
        /// </summary>
        internal void RemoveLast()
        {
            _values.RemoveAt(_values.Count - 1);
            if (!Leaf)
            {
                _children.RemoveAt(_children.Count - 1);
            }
        }

        // Validates the constructor parameters
        private static void ValidatePotentialState(BTreeNode<T> parent, bool leaf, int minimumDegree,
            T[] values, BTreeNode<T>[] children)
        {
            bool root = parent == null;
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (children == null)
            {
                throw new ArgumentOutOfRangeException("minimumDegree", "The minimumDegree must be greater than or equal to 2");
            }
            if (values.Length == 0)
            {
                if (children.Length != 0)
                {
                    throw new ArgumentException("An empty node cannot have children");
                }
            }
            else
            {
                if (values.Length > (2 * minimumDegree - 1))
                {
                    throw new ArgumentException("There are too many values");
                }
                if (!root)
                {
                    if(values.Length < minimumDegree - 1)
                    {
                        throw new ArgumentException("Each non-root node must have at least degree -1 values");
                    }
                }
                if (!leaf && !root)
                {
                    if (values.Length + 1 != children.Length)
                    {
                        throw new ArgumentException("There should be one more child than values.");
                    }
                }
            }
        }

        [Conditional("DEBUG")]
        private void ValidateValues()
        {
            if (_values.Count > 1)
            {
                for (int i =1; i < _values.Count; i +)
                {
                    Debug.Assert(_values[i - 1].CompareTo(_values.[i]) < 0);
                }
            }
        }
    }
    #endregion
}