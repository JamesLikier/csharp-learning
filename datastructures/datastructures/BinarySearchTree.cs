namespace datastructures
{
    public class BinarySearchTree<T>
    {
        private class Node
        {
            public T data;
            public Node? parent;
            public Node?[] children;
            public const int LEFT = 0;
            public const int RIGHT = 1;

            public Node(T data, Node? parent, Node? lChild, Node? rChild)
            {
                this.data = data;
                this.parent = parent;
                this.children = new Node?[2];
                this.children[Node.LEFT] = lChild;
                this.children[Node.RIGHT] = rChild;
            }
        }
        private Node? Root;
        private int _count;
        public int Count { get { return _count; } }
        private Comparer<T> _comparer;
        private class DefaultComparer : Comparer<T>
        {
            public override int Compare(T? x, T? y)
            {
                if (x is null) throw new ArgumentException("x is null");
                if (y is null) throw new ArgumentException("y is null");
                return x.GetHashCode().CompareTo(y.GetHashCode());
            }
        }

        public BinarySearchTree() : this(new DefaultComparer())
        {
        }
        public BinarySearchTree(Comparer<T> comparer)
        {
            this.Root = null;
            this._count = 0;
            this._comparer = comparer;
        }

        public void Add(T data)
        {
            //empty tree
            if (Root is null)
            {
                Root = new Node(data, null, null, null);
                _count++;
            }
            //not empty
            else
            {
                Node? current = Root;
                Node? parent = null;
                while (true)
                {
                    parent = current;
                    int weight = this._comparer.Compare(data, parent.data);
                    //duplicate already inserted
                    if (weight == 0) return;
                    //go left
                    if (weight < 0)
                    {
                        current = current.children[Node.LEFT];
                        //insert left
                        if (current is null)
                        {
                            parent.children[Node.LEFT] = new Node(data, parent, null, null);
                            break;
                        }
                    }
                    //go right
                    else
                    {
                        current = current.children[Node.RIGHT];
                        //insert right
                        if (current is null)
                        {
                            parent.children[Node.RIGHT] = new Node(data, parent, null, null);
                            break;
                        }
                    }
                }
                _count++;
            }
        }
        private Node? FindNode(T data)
        {
            if (Root is null) return null;
            Node? current = Root;
            while (true)
            {
                int weight = this._comparer.Compare(data, current.data);
                //found node
                if (weight == 0) return current;
                //go left
                if (weight < 0)
                {
                    current = current.children[Node.LEFT];
                    if (current is null) return null;
                }
                //go right
                else
                {
                    current = current.children[Node.RIGHT];
                    if (current is null) return null;
                }
            }
        }

        public bool Remove(T data)
        {
            Node? n = FindNode(data);
            //didn't find data
            if (n is null) return false;
            Node? parent = n.parent;
            Node? rChild = n.children[Node.RIGHT];
            Node? lChild = n.children[Node.LEFT];
            //no children
            if (lChild is null && rChild is null)
            {
                this.RemoveNoChild(n);
            }
            //one child 
            else if (lChild is not null ^ rChild is not null)
            {
                this.RemoveOneChild(n);
            }
            //two children
            else
            {
                //find in-order successor
                Node? current = rChild;
                while (current is not null && current.children[Node.LEFT] is not null) current = current.children[Node.LEFT];
                n.data = current!.data;
                Node? cChild = current.children[Node.LEFT] ?? current.children[Node.RIGHT];
                if(cChild is null)
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
        private void RemoveNoChild(Node n)
        {
            Node? parent = n.parent;
            //root node
            if (parent is null)
            {
                this.Root = null;
            }
            else
            {
                int weight = this._comparer.Compare(n.data, parent.data);
                //chop parent left child
                if (weight < 0)
                {
                    parent.children[Node.LEFT] = null;
                }
                //chop parent right child
                else
                {
                    parent.children[Node.RIGHT] = null;
                }
            }
        }
        private void RemoveOneChild(Node n)
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
    }
}