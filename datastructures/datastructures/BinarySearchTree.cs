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
        }

        public bool Remove(T data)
        {
        }

        public bool Contains(T data)
        {
        }
    }
}