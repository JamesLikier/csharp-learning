namespace datastructures
{
    public class RedBlackTree<TKey, TVal> : BinarySearchTree<TKey, TVal>
    {
        protected class RBTNode : BinaryTree<TKey,TVal>.Node
        {
            public int Color;
            public const int RED = 0;
            public const int BLACK = 1;
            public RBTNode(TKey key, TVal value) : this(key,value,null)
            {
            }
            public RBTNode(TKey key, TVal value, RBTNode? parent) : base(key,value,parent)
            {
                Color = RED;
            }
        }

        new public void Add(TKey key, TVal value)
        {
            //add to tree like a BST
            Add(new RBTNode(key,value));
            
            //find freshly added node
            RBTNode? n = (RBTNode?)FindNode(key, Root);

            //todo
        }
    }
}