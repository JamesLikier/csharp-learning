namespace datastructures
{
    public class BinarySearchTree<TKey,TVal>
    {
        protected class Node
        {
            public TKey Key;
            public TVal Value;
            public Node? Parent;
            public Node? Left;
            public Node? Right;

            public Node(TKey key, TVal value) : this(key, value, null)
            {
            }
            public Node(TKey key, TVal value, Node? parent)
            {
                this.Key = key;
                this.Value = value;
                this.Parent = parent;
                this.Left = null;
                this.Right = null;
            }
        }
        protected Node? Root;
        protected int _count;
        public int Count { get { return _count; } }
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
        public BinarySearchTree(Comparer<TKey> comparer)
        {
            this.Root = null;
            this._count = 0;
            this._comparer = comparer;
        }

        protected void Add(TKey key, TVal value, Node n)
        {
            int weight = _comparer.Compare(key, n.Key);
            if(weight < 0)
            {
                if(n.Left is null)
                {
                    n.Left = new(key,value,n);
                    _count++;
                }
                else
                {
                    Add(key,value,n.Left);
                }
            }
            else if(weight > 0)
            {
                if(n.Right is null)
                {
                    n.Right = new(key,value,n);
                    _count++;
                }
                else
                {
                    Add(key,value,n.Right);
                }
            }
        }
        public void Add(TKey key, TVal value)
        {
            //empty tree
            if (Root is null)
            {
                Root = new(key,value);
                _count++;
            }
            //not empty
            else
            {
                Add(key,value,Root);
            }
        }
        protected Node? FindNode(TKey key)
        {
            if (Root is null) return null;
            Node? current = Root;
            while (current is not null)
            {
                int weight = this._comparer.Compare(key, current.Key);
                //found node
                if (weight == 0) return current;
                //go left
                if (weight < 0)
                {
                    current = current.Left;
                }
                //go right
                else
                {
                    current = current.Right;
                }
            }
            return null;
        }

        protected bool Remove(TKey key, Node? n)
        {
            if(n is null) return false;
            int weight = _comparer.Compare(key, n.Key);
            // we found our node to remove
            if(weight == 0)
            {
            }
            else if (weight < 0)
            {
            }
        }
        public bool Remove(TKey key)
        {
            Node? n = FindNode(key);
            //didn't find key
            if (n is null) return false;

            //no children
            if (n.Left is null && n.Right is null)
            {
                this.RemoveNoChild(n);
            }
            //one child 
            else if (n.Left is not null ^ n.Right is not null)
            {
                this.RemoveOneChild(n);
            }
            //two children
            else
            {
                //find in-order successor
                Node? successor = n.Right;
                while(successor is not null && successor.Left is not null) successor = successor.Left;
                //TODO
                n.Key = successor.Key;
                n.Value = successor.Value;
                if(successor.Right is null)
                {
                    RemoveNoChild(current);
                }
                else
                {
                    RemoveOneChild(current);
                }
            }
            _count--;
            return true;
        }
        protected void RemoveNoChild(Node n)
        {
            //root node
            if (n.Parent is null)
            {
                this.Root = null;
                return;
            }
            int weight = this._comparer.Compare(n.Key, n.Parent.Key);
            //chop parent left child
            if (weight < 0) n.Parent.Left = null;
            //chop parent right child
            if (weight > 0) n.Parent.Right = null;
        }
        protected void RemoveOneChild(Node n)
        {
            Node? parent = n.parent;
            Node child = n.children[Node.LEFT] ?? n.children[Node.RIGHT]!;
            if (parent is null)
            {
                this.Root = child;
                this.Root.parent = null;
            }
            else
            {
                int weight = this._comparer.Compare(n.data, parent.data);
                if (weight < 0)
                {
                    parent.children[Node.LEFT] = child;
                }
                else
                {
                    parent.children[Node.RIGHT] = child;
                }
                child.parent = parent;
            }
        }

        public bool Contains(T data)
        {
            return FindNode(data) is not null;
        }
        
        public IEnumerable<T> InOrder()
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
                    cursor = cursor.children[Node.LEFT];
                }
                while(stack.Count > 0)
                {
                    cursor = stack.Pop();
                    yield return cursor.data;
                    //go right once, then add all left nodes
                    cursor = cursor.children[Node.RIGHT];
                    while(cursor is not null)
                    {
                        stack.Push(cursor);
                        cursor = cursor.children[Node.LEFT];
                    }
                }
            }
        }
        public IEnumerable<T> PreOrder()
        {
            /* Basic Algorithm:
             * 
             * Same as InOrder for traversal.
             * 
             * ** PreOrder: yield return cursor.data when
             * pushing a node onto stack.
             * 
             */
            if (Root is not null)
            {
                Stack<Node> stack = new();
                Node? cursor = Root;
                while (cursor is not null)
                {
                    yield return cursor.data;
                    stack.Push(cursor);
                    cursor = cursor.children[Node.LEFT];
                }
                while(stack.Count > 0)
                {
                    cursor = stack.Pop();
                    cursor = cursor.children[Node.RIGHT];
                    while(cursor is not null)
                    {
                        yield return cursor.data;
                        stack.Push(cursor);
                        cursor = cursor.children[Node.LEFT];
                    }
                }
            }
        }
        public IEnumerable<T> BreadthFirst()
        {
            /* Basic Algorithm:
             * 
             * create queue and add root.
             * 
             * while queue has nodes:
             * yield return cursor.data
             * add children to queue.
             *
             * finished when queue is empty
             */

            if (Root is not null)
            {
                Node cursor = Root;
                Queue<Node> queue = new();
                queue.Add(cursor);
                Node? lChild = null;
                Node? rChild = null;
                while(queue.Count > 0)
                {
                    cursor = queue.Remove();
                    yield return cursor.data;
                    lChild = cursor.children[Node.LEFT];
                    rChild = cursor.children[Node.RIGHT];
                    if(lChild is not null) queue.Add(lChild);
                    if(rChild is not null) queue.Add(rChild);
                }
            }
        }
    }
}