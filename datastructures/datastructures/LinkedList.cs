using System.Runtime.CompilerServices;

namespace datastructures
{
    public class LinkedList<T>
    {
        private class Node
        {
            public T Data;
            public Node? Prev;
            public Node? Next;
            public Node(T data, Node? prev, Node? next)
            {
                this.Data = data;
                this.Prev = prev;
                this.Next = next;
            }
        }
        private Node? Root;
        private Node? Last;
        private int _count;
        public int Count { get { return _count; } }
        public LinkedList()
        {
            Root = null;
            Last = null;
            _count = 0;
        }
        public void Append(T data)
        {
            if (Root is null)
            {
                Root = new(data, null, null);
                Last = Root;
            }
            else
            {
                Node n = new(data, Last, null);
                if (Last is not null) Last.Next = n;
                Last = n;
            }
            _count++;
        }
        public void Insert(T data, int position)
        {
            if (position > _count) throw new IndexOutOfRangeException();
            if (_count == 0)
            {
                Root = new(data, null, null);
                Last = Root;
            }
            else
            {
                Node? prev = null;
                Node? cur = Root;

                for (int i = 0; i < position && cur is not null; i++)
                {
                    prev = cur;
                    cur = cur.Next;
                }
                Node n = new(data, prev, cur);
                if (prev is not null) prev.Next = n;
                if (cur is not null) cur.Prev = n;
            }
            _count++;
        }
        public T Remove(T data)
        {
            if (_count == 0) throw new EmptyListException();
            Node cur = FindNode(data);
            Node? prev = cur.Prev;
            Node? next = cur.Next;
            if (prev is not null) prev.Next = next;
            if (next is not null) next.Prev = prev;
            cur.Next = null;
            cur.Prev = null;
            return cur.Data;
            throw new NotFoundException();
        }
        public T RemoveAt(int index)
        {
            if (index > _count) throw new IndexOutOfRangeException();
            if (Root is null) throw new EmptyListException();
            Node cur = Root;
            for (int i = 0; i < index && cur.Next is not null; i++)
            {
                cur = cur.Next;
            }
            Node? prev = cur.Prev;
            Node? next = cur.Next;
            if (prev is not null) prev.Next = next;
            if (next is not null) next.Prev = prev;
            cur.Next = null;
            cur.Prev= null;
            return cur.Data;
        }
        public int FindPosition(T data)
        {
            if (Root is null) throw new EmptyListException();
            Node? cur = Root;
            int pos = 0;
            while (cur is not null)
            {
                if (cur.Data is not null && cur.Data.Equals(data)) return pos;
                pos++;
                cur = cur.Next;
            }
            throw new NotFoundException();
        }

        private Node FindNode(T data)
        {
            if (Root is null) throw new EmptyListException();
            Node? cur = Root;
            while (cur is not null)
            {
                if (cur.Data is not null && cur.Data.Equals(data)) return cur;
            }
            throw new NotFoundException();
        }
    }
}