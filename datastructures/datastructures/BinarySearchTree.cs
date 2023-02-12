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
        
        public IEnumerator<T> InOrder()
        {
            //todo
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
                    if (cursor.children[Node.LEFT] is null)
                    {
                        yield return cursor.data;
                        //todo
                    }
                }
            }

        }
    }
}