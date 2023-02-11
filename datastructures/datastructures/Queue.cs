namespace datastructures
{
    public class Queue<T>
    {
        private int _count;
        public int Count { get { return _count; } }

        private class Node
        {
            public Node? Next;
            public T Data;

            public Node(T data, Node? next)
            {
                this.Data = data;
                this.Next= next;
            }
        }
        private Node? Root;
        private Node? Last;
        public Queue()
        {
            Root = null;
            Last = null;
            _count= 0;
        }

        public void Add(T data)
        {
            Node n = new(data, null);
            if (Last is not null) Last.Next = n;
            Root ??= n;
            Last = n;
            _count++;
        }

        public T Remove()
        {
            if(Root is null) throw new IndexOutOfRangeException();
            Node n = Root;
            Root = Root.Next;
            n.Next= null;
            _count--;
            return n.Data;
        }
    }
}
