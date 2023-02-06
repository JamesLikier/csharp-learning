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

                if (prev is not null) prev.Next = this;
                if (next is not null) next.Prev = this;
            }
            public void Remove()
            {
                if (Prev is not null) Prev.Next = Next;
                if (Next is not null) Next.Prev = Prev;
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
        public void ClearList()
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
                Last = n;
            }
            _count++;
        }
        public void Insert(T data, int index)
        {
            if (index > _count) throw new IndexOutOfRangeException();
            if (_count == 0)
            {
                Root = new(data, null, null);
                Last = Root;
            }
            else if (index == _count)
            {
                Node n = new Node(data, Last, null);
                Last = n;
            }
            else if (index == 0)
            {
                Root = new(data, null, Root);
            }
            else
            {
                Node insertBefore = GetNode(index);
                new Node(data, insertBefore.Prev, insertBefore);
            }
            _count++;
        }
        public T Remove(T data)
        {
            if (_count == 0) throw new EmptyListException();
            Node n = FindNode(data);
            if (Last == n) Last = n.Prev;
            if (Root == n) Root = n.Next;
            n.Remove();
            if (_count == 1)
            {
                ClearList();
            }
            else
            {
                _count--;
            }
            return n.Data;
        }
        public T RemoveAt(int index)
        {
            if (Root is null) throw new EmptyListException();
            if (index >= _count || index < 0) throw new IndexOutOfRangeException();
            Node n = GetNode(index);
            if (_count == 1)
            {
                ClearList();
            }
            else
            {
                if (Last == n) Last = n.Prev;
                if (Root == n) Root = n.Next;
                n.Remove();
                _count--;
            }
            return n.Data;
        }
        public T Pop()
        {
            if (Last is null) throw new EmptyListException();
            Node n = Last;
            if (_count > 1)
            {
                Last = Last.Prev;
                n.Remove();
                _count--;
            }
            else
            {
                ClearList();
            }
            return n.Data;
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

        private Node GetNode(int index)
        {
            if (Root is null || Last is null) throw new EmptyListException();
            if (index > _count || index < 0) throw new IndexOutOfRangeException();
            if (index == _count) return Last;
            Node cur = Root;
            int pos = 0;
            while (pos < index && cur.Next is not null)
            {
                cur = cur.Next;
                pos++;
            }
            return cur!;
        }
        private Node FindNode(T data)
        {
            if (Root is null) throw new EmptyListException();
            Node? cur = Root;
            while (cur is not null)
            {
                if (cur.Data is not null && cur.Data.Equals(data)) return cur;
                cur = cur.Next;
            }
            throw new NotFoundException();
        }
        public T[] ToArray()
        {
            if (Root is null) throw new EmptyListException();
            Node cur = Root;
            T[] result =  new T[_count];
            for (int i = 0; i < _count && cur is not null; i++)
            {
                result[i] = cur.Data;
                cur = cur.Next!;
            }
            return result;

        }
    }
}