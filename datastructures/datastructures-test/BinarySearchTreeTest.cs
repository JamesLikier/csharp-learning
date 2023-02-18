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
        public ds.BinarySearchTree<int> CreateTree()
        {
            ds.BinarySearchTree<int> bst = new();
            foreach (int i in nums) bst.Add(i);
            return bst;
        }
        [TestMethod]
        public void AddTest()
        {
            BinarySearchTree<int> bst = CreateTree();
            Assert.AreEqual(nums.Length, bst.Count);
            int[] preorder = bst.PreOrder().ToArray();
            Assert.IsTrue(preorder.SequenceEqual(numsPreOrder));
        }
        [TestMethod]
        public void RemoveTest()
        {
            BinarySearchTree<int> bst = CreateTree();

            Assert.IsFalse(bst.Remove(-1));
            //remove childless left node
            Assert.IsTrue(bst.Remove(1));
            //remove childless right node
            Assert.IsTrue(bst.Remove(7));
            //remove node with 1 child
            Assert.IsTrue(bst.Remove(19));
            //remove node with 2 children
            Assert.IsTrue(bst.Remove(3));
            //remove root node with 2 children
            Assert.IsTrue(bst.Remove(10));

            int[] preorder = (bst.PreOrder()).ToArray();

            Assert.AreEqual(nums.Length-5, bst.Count);
            Assert.IsTrue(preorder is [14, 5, 4, 2, 6, 15, 17, 16, 20, 18]);
        }
        [TestMethod]
        public void BreadthFirstTest()
        {
            BinarySearchTree<int> bst = CreateTree();
            Assert.IsTrue(bst.BreadthFirst().ToArray().SequenceEqual(numsBreadthFirst));
        }
        [TestMethod]
        public void PreOrderTest()
        {
            BinarySearchTree<int> bst = CreateTree();
            Assert.IsTrue(bst.PreOrder().ToArray().SequenceEqual(numsPreOrder));
        }
        [TestMethod]
        public void InOrderTest()
        {
            BinarySearchTree<int> bst = CreateTree();
            Assert.IsTrue(bst.InOrder().ToArray().SequenceEqual(numsInOrder));
        }
    }
}