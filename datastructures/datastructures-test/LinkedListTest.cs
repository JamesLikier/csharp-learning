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
            Assert.AreEqual(l.ToArray() is [1, 2], true);
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
            Assert.AreEqual(l.ToArray() is [2, 1], true);
            l.Insert(3, l.Count);
            Assert.AreEqual(true, l.ToArray() is [2, 1, 3]);
            l.Insert(4, 1);
            Assert.AreEqual(true, l.ToArray() is [2, 4, 1, 3]);
            Assert.AreEqual(4, l.Count);
        }
        [TestMethod]
        public void RemoveTest()
        {
            ds.LinkedList<int> l = new();
            Assert.ThrowsException<EmptyListException>(()=> { l.Remove(1); });
            l.Append(1);
            Assert.ThrowsException<NotFoundException>(() => { l.Remove(2); });
            l.Remove(1);
            Assert.AreEqual(0, l.Count);
            l.Append(1);
            l.Append(2);
            l.Append(3);
            Assert.AreEqual(true, l.ToArray() is [1, 2, 3]);
            l.Remove(1);
            Assert.AreEqual(true, l.ToArray() is [2, 3]);
            l.Remove(3);
            Assert.AreEqual(true, l.ToArray() is [2]);
            l.Remove(2);
            Assert.AreEqual(0,l.Count);
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
            l.Append(1);
            l.Append(2);
            l.Append(3);
            Assert.AreEqual(true, l.ToArray() is [1, 2, 3]);
            Assert.AreEqual(1, l.RemoveAt(0));
            Assert.AreEqual(true, l.ToArray() is [2, 3]);
            Assert.AreEqual(3, l.RemoveAt(1));
            Assert.AreEqual(true, l.ToArray() is [2]);
            Assert.AreEqual(2, l.RemoveAt(0));
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void PopTest()
        {
            ds.LinkedList<int> l = new();
            Assert.ThrowsException<EmptyListException>(() => { l.Pop(); });
            l.Append(1);
            l.Append(2);
            l.Append(3);
            Assert.AreEqual(3, l.Pop());
            Assert.AreEqual(2, l.Pop());
            Assert.AreEqual(1, l.Pop());
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void FindPositionTest()
        {
            ds.LinkedList<int> l = new();
            Assert.ThrowsException<EmptyListException>(() => { l.FindPosition(1); });
            l.Append(1);
            l.Append(2);
            l.Append(3);
            Assert.AreEqual(0, l.FindPosition(1));
            Assert.AreEqual(1, l.FindPosition(2));
            Assert.AreEqual(2, l.FindPosition(3));
        }
    }
}