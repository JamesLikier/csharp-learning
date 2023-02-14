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
                            return;
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
                            return;
                        }
                    }
                            _count++;
                }
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
            if(Root is not null)
            {
                Node? cursor = Root;
                Node? lChild = cursor.children[Node.LEFT];
                Node? rChild = cursor.children[Node.RIGHT];
                Stack<Node> stack = new();

                while(cursor is not null)
                {
                    yield return cursor.data;
                    //go left if possible
                    if(cursor.children[Node.LEFT] is not null)
                    {
                        //save this node for checking right nodes later
                        stack.Push(cursor);
                        cursor = cursor.children[Node.LEFT];
                    }
                    //can't go left, try going right
                    else
                    {
                        //check if cursor has a right node
                        if(cursor.children[Node.RIGHT] is not null)
                        {
                            cursor = cursor.children[Node.RIGHT];
                        }
                        //cursor does not have a right node, so pop a node to try again
                        else
                        {
                            //pop a node until we find a right node
                            while(cursor.children[Node.RIGHT] is null && stack.Count > 0)
                            {
                                cursor = stack.Pop();
                            }
                            //we found a node
                            if (cursor.children[Node.RIGHT] is not null)
                            {
                                cursor = cursor.children[Node.RIGHT];
                            }
                            //no more nodes to check
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}