namespace datastructures_test
{
    using datastructures;
    using ds = datastructures;
    using System.Linq;
    [TestClass]
    public class BinarySearchTreeTest
    {
        public static int CreateTreeCount = 0;
        public ds.BinarySearchTree<int> CreateTree()
        {
            ds.BinarySearchTree<int> bst = new();
            bst.Add(10);//1
            bst.Add(5);//2
            bst.Add(3);//3
            bst.Add(4);//4
            bst.Add(2);//5
            bst.Add(1);//6
            bst.Add(15);//7
            bst.Add(17);//8
            bst.Add(16);//9
            bst.Add(20);//10
            bst.Add(19);//11
            bst.Add(18);//12
            bst.Add(6);//13
            bst.Add(7);//14
            bst.Add(14);//15
            BinarySearchTreeTest.CreateTreeCount = 15;
            return bst;
            /*
             *                      10
             *                 5          15
             *             3     6     14      17
             *           2   4     7        16    20
             *         1                        19
             *                                18
             */
        }
        [TestMethod]
        public void AddTest()
        {
            BinarySearchTree<int> bst = CreateTree();
            Assert.AreEqual(BinarySearchTreeTest.CreateTreeCount, bst.Count);
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
            Assert.AreEqual(BinarySearchTreeTest.CreateTreeCount-5, bst.Count);
            int[] preorder = (bst.PreOrder()).ToArray();
            Assert.IsTrue(preorder is [14, 5, 4, 2, 6, 15, 17, 16, 20, 18]);
        }
    }
}