namespace datastructures
{
    public class Stack<T>
    {
        private Node? root;
        private int _length;
        public int length
        {
            get
            {
                return _length;
            }
        }

        private class Node {
            public Node? prev;
            public T? data;
            public Node(T? data, Node? prev)
            {
                this.data = data;
                this.prev = prev;
            }
        }
        public Stack()
        {
            _length = 0;
            root = null;
        }

        public void push(T data)
        {
            if (root == null)
            {
                //todo
            }
        }
    }
}
