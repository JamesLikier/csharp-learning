namespace datastructures
{
    public class BinarySearchTree<TKey, TVal> : BinaryTree<TKey, TVal>
    {
        protected Comparer<TKey> _comparer;
        protected class DefaultComparer : Comparer<TKey>
        {
            public override int Compare(TKey? x, TKey? y)
            {
                if (x is null) throw new ArgumentException("x is null");
                if (y is null) throw new ArgumentException("y is null");
                return x.GetHashCode().CompareTo(y.GetHashCode());
            }
        }

        public BinarySearchTree() : this(new DefaultComparer())
        {
        }
        public BinarySearchTree(Comparer<TKey> comparer) : base()
        {
            this._comparer = comparer;
        }

        protected void Add(Node root, Node n)
        {
            int weight = _comparer.Compare(n.Key, root.Key);
            if (weight < 0)
            {
                if (root.Left is null)
                {
                    root.Left = n;
                    n.Parent = root;
                }
                else
                {
                    Add(root.Left, n);
                }
            }
            else if (weight > 0)
            {
                if (root.Right is null)
                {
                    root.Right = n;
                    n.Parent = root;
                }
                else
                {
                    Add(root.Right, n);
                }
            }
        }
        protected void Add(Node n)
        {
            if (Root is null)
            {
                Root = n;
            }
            else
            {
                Add(Root, n);
            }
            _count++;
        }
        public void Add(TKey key, TVal value)
        {
            Add(new Node(key,value));
        }

        protected Node? FindNode(TKey key, Node? n)
        {
            // we ran out of nodes to search
            if (n is null) return null;

            int weight = _comparer.Compare(key, n.Key);
            // we found our node 
            if (weight == 0)
            {
                return n;
            }
            // key is less than n.Key
            else if (weight < 0)
            {
                return FindNode(key, n.Left);
            }
            // key is greater than n.Key
            else
            {
                return FindNode(key, n.Right);
            }
        }

        public bool Remove(TKey key)
        {
            Node? n = FindNode(key, Root);
            //did not find node
            if (n is null) return false;

            //found node
            if (n.Left is null && n.Right is null)
            {
                RemoveNoChild(n);
            }
            else if (n.Left is null ^ n.Right is null)
            {
                RemoveOneChild(n);
            }
            else
            {
                RemoveTwoChild(n);
            }
            _count--;
            return true;
        }
        protected void RemoveNoChild(Node n)
        {
            //root node
            if (Root == n)
            {
                ReassignRoot(null);
            }
            //chop parent.child
            else if (n.Parent is not null)
            {
                if (n.Parent.Left == n) n.Parent.Left = null;
                if (n.Parent.Right == n) n.Parent.Right = null;
            }
        }
        protected void RemoveOneChild(Node n)
        {
            Node child = n.Left ?? n.Right!;
            Node? parent = n.Parent;

            //root node
            if (Root == n)
            {
                ReassignRoot(child);
            }
            else if (parent is not null)
            {
                if (parent.Left == n) ReassignLeft(parent, child);
                if (parent.Right == n) ReassignRight(parent, child);
            }
        }
        protected void RemoveTwoChild(Node n)
        {
            Node? successor = FurthestLeft(n.Right);
            if (successor is null) return;

            //if successor has right child, point to successor.parent
            if (successor.Right is not null && successor.Parent is not null)
            {
                if (successor.Parent.Left == successor) ReassignLeft(successor.Parent, successor.Right);
                if (successor.Parent.Right == successor) ReassignRight(successor.Parent, successor.Right);
            }
            //no child, reassign successor.parent.child to null
            else if (successor.Parent is not null)
            {
                if (successor.Parent.Left == successor) successor.Parent.Left = null;
                if (successor.Parent.Right == successor) successor.Parent.Right = null;
            }

            //reassign Root if needed
            if (Root == n)
            {
                ReassignRoot(successor);
            }
            //not Root, reassign parent.child to successor
            else if (n.Parent is not null)
            {
                if (n.Parent.Left == n) ReassignLeft(n.Parent, successor);
                if (n.Parent.Right == n) ReassignRight(n.Parent, successor);
            }

            //assign n.child to successor
            ReassignLeftRight(successor, n.Left, n.Right);
        }

        public bool Contains(TKey key)
        {
            return FindNode(key, Root) is not null;
        }

        public IEnumerable<TVal> InOrder()
        {
            /*
             * Basic Algorithm:
             * Follow left nodes as far as possible,
             * adding current node to stack when left child exists.
             * 
             * Continue until no left child exists,
             * then pop a node from the stack to check for a right node.
             * 
             * If right node exists, repeat above steps for going down
             * as far left as possible.
             * 
             * Finished when stack is empty and current node has no
             * right child.
             *
             *
             * ** InOrder: yield return cursor.data when
             *  popping a node from stack.
             * 
             */
            if (Root is not null)
            {
                Stack<Node> stack = new();
                Node? cursor = Root;
                //go left as far as possible to get smallest node
                while (cursor is not null)
                {
                    stack.Push(cursor);
                    cursor = cursor.Left;
                }
                while (stack.Count > 0)
                {
                    cursor = stack.Pop();
                    yield return cursor.Value;
                    //go right once, then add all left nodes
                    cursor = cursor.Right;
                    while (cursor is not null)
                    {
                        stack.Push(cursor);
                        cursor = cursor.Left;
                    }
                }
            }
        }
    }
}