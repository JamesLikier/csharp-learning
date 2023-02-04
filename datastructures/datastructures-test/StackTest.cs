using DS = datastructures;

namespace datastructures_test
{
    [TestClass]
    public class StackTest
    {
        [TestMethod]
        public void TestPop()
        {
            DS.Stack<int> stack = new();
            Assert.ThrowsException<Exception>(() => { stack.Pop(); });
            stack.Push(1);
            Assert.AreEqual(1, stack.Pop());
            Assert.ThrowsException<Exception>(() => { stack.Pop(); });

            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Assert.AreEqual(3, stack.Length);
            Assert.AreEqual(3,stack.Pop());
            Assert.AreEqual(2,stack.Pop());
            Assert.AreEqual(1,stack.Pop());
            Assert.AreEqual(0, stack.Length);
        }
        [TestMethod]
        public void TestPush()
        {
            DS.Stack<int> stack = new();
            stack.Push(1);
            Assert.AreEqual(1, stack.Length);
            stack.Push(1);
            Assert.AreEqual(2, stack.Length);
        }
    }
}