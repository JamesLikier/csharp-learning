namespace datastructures
{
    public class BinaryTree<TKey, TVal>
    {
        protected class Node
        {
            public TKey Key;
            public TVal Value;
            public Node? Parent;
            public Node? Left;
            public Node? Right;

            public Node(TKey key, TVal value) : this(key, value, null)
            {
            }
            public Node(TKey key, TVal value, Node? parent)
            {
                this.Key = key;
                this.Value = value;
                this.Parent = parent;
                this.Left = null;
                this.Right = null;
            }
        }
        protected Node? Root;
        protected int _count;
        public int Count { get { return _count; } }

        public BinaryTree()
        {
            this.Root = null;
            this._count = 0;
        }

        protected const int LEFT = 0;
        protected const int RIGHT = 1;
        protected void Rotate(Node subRoot, int direction)
        {
            Node? parent = subRoot.Parent;
            Node? odChild;
            if (direction == LEFT)
            {
                odChild = subRoot.Right;
            }
            else
            {
                odChild = subRoot.Left;
            }

            if (odChild is null) throw new NullReferenceException("Rotate: Child node is null.");

            //new subRoot parent and child relationship
            if (parent is not null && parent.Left == subRoot) ReassignLeft(parent, odChild);
            if (parent is not null && parent.Right == subRoot) ReassignRight(parent, odChild);

            //new od.child and orig subRoot relationship
            if (direction == LEFT) ReassignRight(subRoot, odChild.Left);
            if (direction == RIGHT) ReassignLeft(subRoot, odChild.Right);

            //new subRoot and orig subRoot relationship
            if (direction == LEFT)
            {
                ReassignLeft(odChild, subRoot);
            }
            else
            {
                ReassignRight(odChild , subRoot);
            }

            //re-establish Root if needed
            if (Root == subRoot) ReassignRoot(odChild);
        }
        protected void RotateRight(Node subRoot)
        {
            Rotate(subRoot, RIGHT);
        }
        protected void RotateLeft(Node subRoot)
        {
            Rotate(subRoot, LEFT);
        }

        protected void ReassignLeft(Node? parent, Node? left)
        {
            if (parent is not null) parent.Left = left;
            if (left is not null) left.Parent = parent;
        }

        protected void ReassignRight(Node? parent, Node? right)
        {
            if (parent is not null) parent.Right = right;
            if (right is not null) right.Parent = parent;
        }

        protected void ReassignLeftRight(Node parent, Node? left, Node? right)
        {
            parent.Left = left;
            parent.Right = right;
            if (left is not null) left.Parent = parent;
            if (right is not null) right.Parent = parent;
        }

        protected void ReassignRoot(Node? newRoot)
        {
            Root = newRoot;
            if (newRoot is not null) newRoot.Parent = null;
        }
        protected Node? FurthestLeft(Node? n)
        {
            if (n is null || n.Left is null) return n;
            return FurthestLeft(n.Left);
        }

        public IEnumerable<TVal> PreOrder()
        {
            /* Basic Algorithm:
             * 
             * Same as InOrder for traversal.
             * 
             * ** PreOrder: yield return cursor.data when
             * pushing a node onto stack.
             * 
             */
            if (Root is not null)
            {
                Stack<Node> stack = new();
                Node? cursor = Root;
                while (cursor is not null)
                {
                    yield return cursor.Value;
                    stack.Push(cursor);
                    cursor = cursor.Left;
                }
                while(stack.Count > 0)
                {
                    cursor = stack.Pop();
                    cursor = cursor.Right;
                    while(cursor is not null)
                    {
                        yield return cursor.Value;
                        stack.Push(cursor);
                        cursor = cursor.Left;
                    }
                }
            }
        }
        public IEnumerable<TVal> BreadthFirst()
        {
            /* Basic Algorithm:
             * 
             * create queue and add root.
             * 
             * while queue has nodes:
             * yield return cursor.data
             * add children to queue.
             *
             * finished when queue is empty
             */

            if (Root is not null)
            {
                Node cursor = Root;
                Queue<Node> queue = new();
                queue.Add(cursor);
                Node? lChild = null;
                Node? rChild = null;
                while(queue.Count > 0)
                {
                    cursor = queue.Remove();
                    yield return cursor.Value;
                    lChild = cursor.Left;
                    rChild = cursor.Right;
                    if(lChild is not null) queue.Add(lChild);
                    if(rChild is not null) queue.Add(rChild);
                }
            }
        }
    }
}
