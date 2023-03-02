namespace datastructures_test
{
    using datastructures;
    using ds = datastructures;
    using System.Linq;
    using System.Globalization;

    [TestClass]
    public class BinarySearchTreeTest
    {
        public static int[] nums = new int[] { 10, 5, 3, 4, 2, 1, 15, 17, 16, 20, 19, 18, 6, 7, 14 };
        public static ds.ArrayList<int> numsPreOrder = new() { 10, 5, 3, 2, 1, 4, 6, 7, 15, 14, 17, 16, 20, 19, 18 };
        public static ds.ArrayList<int> numsBreadthFirst = new() { 10, 5, 15, 3, 6, 14, 17, 2, 4, 7, 16, 20, 1, 19, 18 };
        public static ds.ArrayList<int> numsInOrder = new() { 1, 2, 3, 4, 5, 6, 7, 10, 14, 15, 16, 17, 18, 19, 20 };
        /*
         *                      10
         *                 5          15
         *             3     6     14      17
         *           2   4     7        16    20
         *         1                        19
         *                                18
         */
        public ds.BinarySearchTree<int,int> CreateTree()
        {
            ds.BinarySearchTree<int,int> bst = new();
            foreach (int i in nums) bst.Add(i,i);
            return bst;
        }
        [TestMethod]
        public void AddTest()
        {
            BinarySearchTree<int,int> bst = CreateTree();
            Assert.AreEqual(nums.Length, bst.Count);
            int[] preorder = bst.PreOrder().ToArray();
            Assert.IsTrue(preorder.SequenceEqual(numsPreOrder));
        }
        public delegate bool RemoveAndCount(int i);
        [TestMethod]
        public void RemoveTest()
        {
            BinarySearchTree<int, int> bst = CreateTree();
            int removeCount = 0;
            RemoveAndCount rac = i => { bool r = bst.Remove(i); if (r) removeCount++; return r; };

            Assert.IsFalse(rac(-1));
            //remove childless left node
            Assert.IsTrue(rac(1));
            Assert.IsTrue(bst.PreOrder().ToArray() is [ 10, 5, 3, 2, 4, 6, 7, 15, 14, 17, 16, 20, 19, 18 ]);
            //remove childless right node
            Assert.IsTrue(rac(7));
            Assert.IsTrue(bst.PreOrder().ToArray() is [ 10, 5, 3, 2, 4, 6, 15, 14, 17, 16, 20, 19, 18 ]);
            //remove node with 1 child
            Assert.IsTrue(rac(19));
            Assert.IsTrue(bst.PreOrder().ToArray() is [ 10, 5, 3, 2, 4, 6, 15, 14, 17, 16, 20, 18 ]);
            //remove node with 2 children
            Assert.IsTrue(rac(3));
            Assert.IsTrue(bst.PreOrder().ToArray() is [ 10, 5, 4, 2, 6, 15, 14, 17, 16, 20, 18 ]);
            //remove root node with 2 children, successor no children
            Assert.IsTrue(rac(10));
            Assert.IsTrue(bst.PreOrder().ToArray() is [ 14, 5, 4, 2, 6, 15, 17, 16, 20, 18 ]);
            //remove root node with 2 children, successor with child
            Assert.IsTrue(rac(14));
            Assert.IsTrue(bst.PreOrder().ToArray() is [ 15, 5, 4, 2, 6, 17, 16, 20, 18 ]);

            Assert.AreEqual(nums.Length-removeCount, bst.Count);
        }
        [TestMethod]
        public void BreadthFirstTest()
        {
            BinarySearchTree<int,int> bst = CreateTree();
            Assert.IsTrue(bst.BreadthFirst().ToArray().SequenceEqual(numsBreadthFirst));
        }
        [TestMethod]
        public void PreOrderTest()
        {
            BinarySearchTree<int,int> bst = CreateTree();
            Assert.IsTrue(bst.PreOrder().ToArray().SequenceEqual(numsPreOrder));
        }
        [TestMethod]
        public void InOrderTest()
        {
            BinarySearchTree<int,int> bst = CreateTree();
            Assert.IsTrue(bst.InOrder().ToArray().SequenceEqual(numsInOrder));
        }
    }
}