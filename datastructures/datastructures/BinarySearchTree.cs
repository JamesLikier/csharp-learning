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

        public BinarySearchTree()
        {
            this.Root = null;
            this._count = 0;
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
                Node? n = Root;
                if(data < n.data && n.children[Node.LEFT] is null)
                {
                    n.children[Node.LEFT] = new Node(data, n, null, null);
                }
                else if (data > n.data && n.children[Node.RIGHT] is null)
                {
                    n.children[Node.RIGHT] = new Node(data, n, null, null);
                }
                //todo
            }
        }

        public bool Remove(T data)
        {
        }

        public bool Contains(T data)
        {
        }
    }
}