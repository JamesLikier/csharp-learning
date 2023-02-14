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
            Assert.IsTrue(bst.Remove(10));
            Assert.AreEqual(nums.Length-5, bst.Count);
            int[] preorder = (bst.PreOrder()).ToArray();
            Assert.IsTrue(preorder is [14, 5, 4, 2, 6, 15, 17, 16, 20, 18]);
        }
    }
}