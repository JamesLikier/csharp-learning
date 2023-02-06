using datastructures;
using ds = datastructures;

namespace datastructures_test
{
    [TestClass]
    public class LinkedListTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ds.LinkedList<int> l = new();
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void ClearListTest()
        {
            ds.LinkedList<int> l = new();
            l.ClearList();
            Assert.AreEqual(0, l.Count);
            l.Append(1);
            l.ClearList();
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void AppendTest()
        {
            ds.LinkedList<int> l = new();
            l.Append(1);
            Assert.AreEqual(1, l.Count);
            l.Append(2);
            Assert.AreEqual(2, l.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            ds.LinkedList<int> l = new();
            l.Insert(1, 0);
            Assert.AreEqual(1, l.Count);
            l.Insert(2, 0);
            Assert.AreEqual(2, l.Count);
            Assert.ThrowsException<IndexOutOfRangeException>(() => { l.Insert(3, 5); });
            Assert.AreEqual(2, l.Count);
            Assert.ThrowsException<IndexOutOfRangeException>(() => { l.Insert(3, -1); });
            Assert.AreEqual(2, l.Count);
        }
        [TestMethod]
        public void RemoveTest()
        {
            ds.LinkedList<int> l = new();
            Assert.ThrowsException<EmptyListException>(()=> { l.Remove(1); });
            l.Append(1);
            l.Remove(1);
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void RemoveAtTest()
        {
            ds.LinkedList<int> l = new();
            Assert.ThrowsException<EmptyListException>(() => { l.RemoveAt(0); });
            l.Append(1);
            Assert.ThrowsException<IndexOutOfRangeException>(() => { l.RemoveAt(-1); });
            Assert.ThrowsException<IndexOutOfRangeException>(() => { l.RemoveAt(1); });
            l.RemoveAt(0);
            Assert.AreEqual(0, l.Count);
        }
    }
}