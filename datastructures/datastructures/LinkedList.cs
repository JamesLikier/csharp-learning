namespace datastructures
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    public class LinkedList<T> : IList<T>
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
        }
        private Node? Root;
        private Node? Last;
        private int _count;
        public int Count { get { return _count; } }
        public bool IsReadOnly { get; }
        public LinkedList()
        {
            Root = null;
            Last = null;
            _count = 0;
        }
        public T this[int index]
        {
            get
            {
                if (index >= _count || index < 0) throw new IndexOutOfRangeException();
                return GetNode(index)!.Data;
            }
            set { Insert(index, value); }
        }
        private void RemoveNode(Node n)
        {
            if (_count == 1)
            {
                Root = null;
                Last = null;
            }
            else if (Root == n)
            {
                Root = n.Next;
            }
            else if (Last == n)
            {
                Last = n.Prev;
            }
            else
            {
                Node? prev = n.Prev;
                Node? next = n.Next;
                if (prev is not null) prev.Next = next;
                if (next is not null) next.Prev = prev;
            }
            _count--;
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node? n = Root;
            while(n is { })
            {
                yield return n.Data;
                n = n.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public void CopyTo(T[] toArray, int startIndex)
        {
            int i = startIndex;
            foreach(T d in this) toArray[i++] = d;
        }
        public void Clear()
        {
            Root = null;
            Last = null;
            _count = 0;
        }
        public void Add(T data)
        {
            Insert(_count, data);
        }
        public void Insert(int index, T data)
        {
            if (index > _count || index < 0) throw new IndexOutOfRangeException();
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
                Node? insertBefore = GetNode(index);
                new Node(data, insertBefore?.Prev, insertBefore);
            }
            _count++;
        }
        public bool Remove(T data)
        {
            Node? n = FindNode(data);
            if (n is not null)
            {
                RemoveNode(n);
                return true;
            }
            return false;
        }
        public void RemoveAt(int index)
        {
            Node? n = GetNode(index);
            if (n is { })
            {
                RemoveNode(n);
            }
        }
        public int IndexOf(T data)
        {
            Node? cur = Root;
            int pos = 0;
            while (cur is not null)
            {
                if (cur.Data is { } d && d.Equals(data)) return pos;
                pos++;
                cur = cur.Next;
            }
            return -1;
        }

        private Node? GetNode(int index)
        {
            if (index >= _count || index < 0) throw new IndexOutOfRangeException();
            if (Root is null) return null;
            if (index == _count-1) return Last;
            Node cur = Root;
            int pos = 0;
            while (pos < index && cur.Next is { })
            {
                cur = cur.Next;
                pos++;
            }
            return cur;
        }
        private Node? FindNode(T data)
        {
            Node? cur = Root;
            while (cur is not null)
            {
                if (cur.Data is not null && cur.Data.Equals(data)) return cur;
                cur = cur.Next;
            }
            return null;
        }
        public bool Contains(T data)
        {
            if (IndexOf(data) >= 0) return true;
            return false;
        }
    }
}