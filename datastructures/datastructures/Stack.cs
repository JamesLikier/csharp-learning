namespace datastructures
{
    public class Stack<T>
    {
        private Node? Root;
        private int _Length;
        public int Length
        {
            get
            {
                return _Length;
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
            _Length = 0;
            Root = null;
        }

        public void Push(T data)
        {
            Root = new Node(data, Root);
            _Length++;
        }

        public T Pop()
        {
            if (Root is null) throw new Exception("Stack is Emtpy");
            T RData = Root.Data;
            Root = Root.Prev;
            _Length--;
            return RData;
        }
    }
}
