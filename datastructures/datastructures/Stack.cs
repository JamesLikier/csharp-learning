namespace datastructures
{
    public class Stack<T>
    {
        private Node? Root;
        private int _count;
        public int Count
        {
            get
            {
                return _count;
            }
        }

        private class Node {
            public Node? Prev;
            public T Data;
            public Node(T data, Node? prev)
            {
                this.Data = data;
                this.Prev = prev;
            }
        }
        public Stack()
        {
            _count = 0;
            Root = null;
        }

        public void Push(T data)
        {
            Root = new Node(data, Root);
            _count++;
        }

        public T Pop()
        {
            if (Root is null) throw new IndexOutOfRangeException();
            T RData = Root.Data;
            Root = Root.Prev;
            _count--;
            return RData;
        }
    }
}
