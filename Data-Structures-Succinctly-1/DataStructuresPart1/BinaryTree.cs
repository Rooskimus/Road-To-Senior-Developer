using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructuresPart1
{
    class BinaryTreeNode<TNode> : IComparable<TNode>
        where TNode : IComparable<TNode>
    {
        public BinaryTreeNode(TNode value)
        {
            Value = value;
        }

        public BinaryTreeNode<TNode> Left { get; set; }
        public BinaryTreeNode<TNode> Right { get; set; }
        public TNode Value { get; private set; }

        /// <summary>
        /// Compares the current node to the provided value
        /// </summary>
        /// <param name="other">The node value to compare to</param>
        /// <returns>1 if the instance value is greater than the provided value
        /// -1 if less, or 0 if equal.</returns>
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }
    }
    public class BinaryTree<T> : IEnumerable<T>
        where T : IComparable<T>
    {
        private BinaryTreeNode<T> _head;
        private int _count;
        public int Count { get { return _count; } }
        
        public void Add(T value) // O(log n); 0(n) in worst case.
        {
            if (_head == null) // For the case where tree is empty
            {
                _head = new BinaryTreeNode<T>(value);
            }
            else // Tree not empty, find right location to insert value.
            {
                AddTo(_head, value);
            }

            _count++;
        }
         private void AddTo(BinaryTreeNode<T> node, T value) // Recursive add algorithm
        {
            if (value.CompareTo(node.Value)<0) // Value is less than current node value
            {
                // If no left child, make this new left child.
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else // Else add to the left node
                {
                    AddTo(node.Left, value);
                }
            }
            else // Value is greater than or equal to current value
            {
                if (node.Right == null)
                {
                    // If no right node, make one
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // else add to right node
                    AddTo(node.Right, value);
                }
            }
        }

        public bool Remove (T value)
        {
            BinaryTreeNode<T> current, parent;
            // Find the node to remove
            current = FindWithParent(value, out parent);

            if (current == null) return false; // if not found, do nothing.
            _count--;
            if (current.Right == null) //If current has no right child, currents left replaces current.
            {
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If the parent value is greater than the current value, make the current 
                        // left child a left child of the parent.
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        // If the parent value is less, make current left child a right child of parent.
                        parent.Right = current.Left;
                    }
                }
            }
            else if (current.Right.Left == null) // If the current's right child has no left child, current's right child replaces current.
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    _head = current.Right;  // if current is top node, it's right child becomes the head of the tree
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If we're looking at the left tree of the parent, the current's
                        // right child becomes a left child of the parent.
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        // If we're looking at the right tree of the parent, the current's right child
                        // becomes a right child of the parent.
                        parent.Right = current.Right;
                    }
                }
            }
            else // If current's right child has a left child, replace with current's right child's left-most child.
            {
                // First find the right's left-most child
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;
                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
                // The parent's left subtree becomes the leftmost's right subtree.
                leftmostParent.Left = leftmost.Right;
                // Assign leftmost's left and right to current's left and right
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null) _head = leftmost;
                else
                {   // Again controlling here for left and right branches from top parent
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }
            return true;
        }

        public bool Contains(T value)
        {
            // Defer to the node search helper function
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            BinaryTreeNode<T> current = _head; //start at the head
            parent = null;
            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0) // Value is less than current.
                {
                    parent = current;
                    current = current.Left; // Go left.
                }
                else if (result < 0) // Value is more than current.
                {
                    parent = current;
                    current = current.Right; // Go right.
                }
                else
                {
                    break; // We have a match.
                }
            }
            return current;
        }

        public void Clear()
        {
            _head = null;
            _count = 0;
        }

        // Preorder is a top-down left to right traversal method, head first.
        public void PreOrderTraversal(Action<T> action)
        {
            PreOrderTraversal(action, _head);
        }

        private void PreOrderTraversal(Action<T> action, BinaryTreeNode<T> node) // O(n)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }

        // Postorder is a bottom-to-top left to right traversal method, head last.

        public void PostOrderTraversal(Action<T> action)  // O(n)
        {
            PostOrderTraversal(action, _head);
        }

        private void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }

        // Inorder is a lowest-to-highest traversal method.

        public void InOrderTraversal(Action<T> action)
        {
            InOrderTraversal(action, _head);
        }

        private void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left); // lower numbers first
                action(node.Value); // middle number
                InOrderTraversal(action, node.Right); // higher numbers
            }
        }

        // Recursion can be removed by using a stack.

        public IEnumerator<T> InOrderTraversal()
        {
            if (_head != null)
            {
                // Store the nodes we've skipped in this stack.
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> current = _head;
                // We need to keep track of whether to go to left or right nodes next, which was done by recursion previously.
                bool goLeftNext = true;
                // Start by pushing head onto the stack.
                stack.Push(current);  // This first push controls the while loop, its value is never actually returned.
                while (stack.Count > 0)
                {
                    if (goLeftNext) // If we're heading left...
                    {
                        //Push everything but the left-most node to the stack.
                        while (current.Left != null)
                        {
                            stack.Push(current);
                            current = current.Left;
                        }
                    }
                    // Inorder is left -> yield -> right
                    yield return current.Value;
                    // If we can go right, do so.
                    if (current.Right != null)
                    {
                        current = current.Right;
                        // Once we've gone right once, we need to go left again if able.
                        goLeftNext = true;
                    }
                    else
                    {
                        // If we can't go right, we need to pop off the parent node
                        // so we can process it and then go to its right node.
                        current = stack.Pop(); // At the end of the process, when the last item on the stack pops it will be the head, but the stack count will hit zero and the loop will terminate before anything is returned.
                        goLeftNext = false;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator() // O(1) to return enumerator, O(n) to enumerate all items.
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
