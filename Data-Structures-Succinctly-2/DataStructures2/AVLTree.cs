using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures2
{
    #region AVLTree
    public class AVLTree<T> : IEnumerable<T>
        where T: IComparable
    {
        public AVLTreeNode<T> Head { get; internal set; }

        public void Add(T value) // O(log n)
        {
            if (Head == null) // Case 1: The tree is empty -- allocate the head.
            {
                Head = new AVLTreeNode<T>(value, null, this);
            }
            else // Case 2: THe tree is not empty -- find the right location to insert.
            {
                AddTo(Head, value);
            }
            Count++;
        }

        private void AddTo(AVLTreeNode<T> node, T value) // Recursive add algorithm.
        {
            if (value.CompareTo(node.Value) < 0) // Value is less than current node value.
            {
                if (node.Left == null) // If no left child, make this the left child.
                {
                    node.Left = new AVLTreeNode<T>(value, node, this);
                }
                else
                {
                    AddTo(node.Left, value); // Else, add it to the left node.
                }
            }
            else // Case 2: Value is equal or greater than the current node
            {
                if (node.Right == null) // If there is no right, add it to the right.
                {
                    node.Right = new AVLTreeNode<T>(value, node, this);
                }
                else // Else add it to the right node
                {
                    AddTo(node.Right, value);
                }
            }
            node.Balance(); // ensure balanced
        }

        public bool Contains(T value) // O(log n) -- guaranteed because worst case of binary tree no longer possible.
        {
            return Find(value) != null;
        }

        /// <summary>
        ///  Finds and returns the first node containing the specified value. If the value
        /// is not found, it returns null. It also returns the parent of the found node (or null)
        /// which is used in Remove.
        /// </summary>
        /// <param name="value">The value to search for.</param>
        /// <returns>The found node (or null)</returns>
        private AVLTreeNode<T> Find(T value)
        {
            AVLTreeNode<T> current = Head; // Starting from the head, look for a match.
            while (current != null) // While we don't have a match...
            {
                int result = current.CompareTo(value);
                if (result > 0) // If the value is less than current...
                {
                    current = current.Left;  // Go left.
                }
                else if (result < 0) // If the value is greater than current...
                {
                    current = current.Right; // Go right.
                }
                else // We have a match!
                {
                    break;
                }
            }
            return current; // If value is not found in the tree, this will be null.
        }

        public bool Remove(T value);

        public IEnumerator<T> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator();

        public void Clear();

        public int Count { get; private set; }
    }
    #endregion

    #region AVLTreeNode
    public class AVLTreeNode<TNode> : IComparable<TNode>
        where TNode : IComparable
    {
        // This class contains access to left and right children, the nodee's
        // value, and a reference to the tree the node is in.  It also
        // provides methods to determine heights of the left and right children
        // and heaviness and balance factors as well as methods for all four rotation types.

        AVLTree<TNode> _tree;
        AVLTreeNode<TNode> _left;
        AVLTreeNode<TNode> _right;

        public AVLTreeNode(TNode value, AVLTreeNode<TNode> parent, AVLTree<TNode> tree)
        {
            Value = value;
            Parent = parent;
            _tree = tree;
        }

        public AVLTreeNode<TNode> Left
        {
            get { return _left; }
            internal set
            {
                _left = value;
                if (_left != null)
                {
                    _left.Parent = this;
                }
            }
        }

        public AVLTreeNode<TNode> Right
        {
            get { return _right; }
            internal set
            {
                _right = value;
                if (_right != null)
                {
                    _right.Parent = this;
                }
            }
        }

        public AVLTreeNode<TNode> Parent { get; internal set; }
        public TNode Value { get; private set; }

        /// <summary>
        /// Compares the current node to the provided value
        /// </summary>
        /// <param name="other">The node value to compare to.</param>
        /// <returns>1 if the instance value is greater than the provided value, -1 if less, 0 if equal.</returns>
        public int CompareTo(TNode other)
        {
            return Value.CompareTo(other);
        }

        #region BalancingProps&Methods
        internal void Balance()
        {
            if (State == TreeState.RightHeavy)
            {
                if (Right != null && Right.BalanceFactor < 0)
                {
                    LeftRightRotation();
                }
                else
                {
                    LeftRotation();
                }
            }
            else if (State == TreeState.LeftHeavy)
            {
                if (Left != null && Left.BalanceFactor > 0)
                {
                    RightLeftRotation();
                }
                else
                {
                    RightRotation();
                }
            }
        }

        private int MaxChildHeight(AVLTreeNode<TNode> node)
        {
            if (node != null)
            {
                return 1 + Math.Max(MaxChildHeight(node.Left), MaxChildHeight(node.Right));
            }
            return 0;
        }

        private int LeftHeight { get { return MaxChildHeight(Left); } }

        private int RightHeight { get { return MaxChildHeight(Right); } }

        private TreeState State
        {
            get
            {
                if (LeftHeight - RightHeight > 1)
                {
                    return TreeState.LeftHeavy;
                }
                if (RightHeight - LeftHeight > 1)
                {
                    return TreeState.RightHeavy;
                }
                return TreeState.Balanced;
            }
        }

        private int BalanceFactor { get { return RightHeight - LeftHeight; } }

        enum TreeState
        {
            Balanced,
            LeftHeavy,
            RightHeavy,
        }
        #endregion

        #region RotationMethods
        private void LeftRotation()
        {
            // a                     b
            //   \                  / \
            //    b      --->      a   c
            //     \
            //      c

            AVLTreeNode<TNode> newRoot = Right;  // Assigns B as a new root.
            ReplaceRoot(newRoot); // Shifts B and all its children up to A's location; Assigns B as a parent of A.
            Right = newRoot.Left; // Assigns any left children of B to be right children of A after the shift.
            //newRoot.Left.Parent = this; // I added this - Assigns A as the parent of the left child of B.
            newRoot.Left = this; // Assigns A as the left child of B.
            // Wouldn't any left child of B need to have A assigned as a parent?

        }

        private void RightRotation()
        {
            //      c                b
            //     /                / \
            //    b      --->      a   c
            //  /   
            // a     

            AVLTreeNode<TNode> newRoot = Left;  // Assigns B as a new root.
            ReplaceRoot(newRoot); // Shifts B and all its children up to C's location; Assigns B as a parent of C.
            Left = newRoot.Right; // Assigns any right children of B to be left children of C after the shift.
            //newRoot.Right.Parent = this; // I added this - Assigns C as the parent of the right child of B.
            newRoot.Right = this; // Assigns A as the left child of B.
            // Wouldn't any right child of B need to have C assigned as a parent?

        }

        private void ReplaceRoot(AVLTreeNode<TNode> newRoot)
        {
            if (this.Parent != null) // This shifts the new root up if it's not replacing the top root (head).
            {
                if (this.Parent.Left == this)
                {
                    this.Parent.Left = newRoot;
                }
                else if (this.Parent.Right == this)
                {
                    this.Parent.Right = newRoot;
                }
            }
            else // This shifts the new root up to the top (head).
            {
                _tree.Head = newRoot;
            }

            newRoot.Parent = this.Parent; // Assigns the new root the parent of spot it's taking (null if head)
            this.Parent = newRoot; // Assigns current root the new root as a parent.
        }


        #endregion
    }
#endregion
}
