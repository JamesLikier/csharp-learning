namespace datastructures_test
{
    using datastructures;
    using ds = datastructures;
    using System.Linq;
    using System.Collections.Generic;

    [TestClass]
    public class BinaryTreeTest
    {
        public BinaryTree createBTWithNLevels(int n)
        {
            if (n <= 0) return null;
            BinaryTree bt = new();
            bt.ReassignRoot(new BinaryTree.Node())
            Queue<BinaryTree.Node> queue = new();
            for(int i = 1; i < n; i++)
            {
            }
        }
        [TestMethod]
        public void RotateTest()
        {
            BinaryTree bt = new();
            bt.Root = new(1,1);
            bt.Root.Left = new(2,2);
            bt.Root.Right = new(3,3);
            bt.ReassignRoot(bt.Root);
            bt.ReassignLeftRight(bt.Root, bt.Root.Left, bt.Root.Right);
            Assert.IsTrue(bt.PreOrder() is [1,2,3]);
            bt.RotateLeft(bt.Root);
            Assert.IsTrue(bt.PreOrder() is [3,1,2]);

            bt = new();
            bt.Root = new(1,1);
            bt.Root.Left = new(2,2);
            bt.Root.Right = new(3,3);
            bt.ReassignRoot(bt.Root);
            bt.ReassignLeftRight(bt.Root, bt.Root.Left, bt.Root.Right);
            bt.RotateRight(bt.Root);
            Assert.IsTrue(bt.PreOrder() is [2,1,3]);

            bt = new();
        }
    }
}