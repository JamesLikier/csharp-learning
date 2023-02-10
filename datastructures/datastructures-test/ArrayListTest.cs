namespace datastructures_test
{
    using datastructures;
    using ds = datastructures;
    [TestClass]
    public class ArrayListTest
    {
        [TestMethod]
        public void ClearTest()
        {
            ds.ArrayList<int> l = new();
            l.Clear();
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void CopyToTest()
        {
            ds.ArrayList<int> l = new();
            l.Add(1);
            l.Add(2);
            l.Add(3);
            int[] data = new int[l.Count * 2];
            l.CopyTo(data, 0);
            Assert.IsTrue(data is [1, 2, 3, 0, 0, 0]);
            l.CopyTo(data, l.Count);
            Assert.IsTrue(data is [1, 2, 3, 1, 2, 3]);
        }
        [TestMethod]
        public void InsertTest()
        {
            ds.ArrayList<int> l = new();
            Assert.ThrowsException<IndexOutOfRangeException>(() => l.Insert(-1, 0));
            Assert.ThrowsException<IndexOutOfRangeException>(() => l.Insert(1, 0));
            l.Insert(0, 0);//0
            l.Insert(0, 1);//1,0
            l.Insert(l.Count, 2);//1,0,2
            l.Insert(1, 3);//1,3,0,2
            Assert.IsTrue(l is [1, 3, 0, 2]);
            Assert.AreEqual(4, l.Count);
        }
        [TestMethod]
        public void ItemTest()
        {
            ds.ArrayList<int> l = new();
            l[0] = 1;
            Assert.AreEqual(1, l.Count);
            Assert.AreEqual(1, l[0]);
        }
        [TestMethod]
        public void AddTest()
        {
            ds.ArrayList<int> l = new();
            l.Add(1);
            l.Add(2);
            l.Add(3);
            Assert.IsTrue(l is [1, 2, 3]);
            Assert.AreEqual(3, l.Count);
        }
        [TestMethod]
        public void RemoveTest()
        {
            ds.ArrayList<int> l = new();
            Assert.IsFalse(l.Remove(1));
            l.Add(1);
            l.Add(2);
            l.Add(3);
            Assert.IsTrue(l.Remove(1));
            Assert.IsTrue(l.Remove(3));
            Assert.IsTrue(l.Remove(2));
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void RemoveAtTest()
        {
            ds.ArrayList<int> l = new();
            Assert.ThrowsException<IndexOutOfRangeException>(() => l.RemoveAt(0));
            l.Add(1);
            l.Add(2);
            l.Add(3);
            l.RemoveAt(0);
            Assert.IsTrue(l is [2, 3]);
        }
        [TestMethod]
        public void IndexOfTest()
        {
            ds.ArrayList<int> l = new();
            Assert.AreEqual(-1, l.IndexOf(1));
            l.Add(1);
            Assert.AreEqual(0, l.IndexOf(1));
            l.Add(2);
            Assert.AreEqual(1, l.IndexOf(2));
        }
        [TestMethod]
        public void ContainsTest()
        {
            ds.ArrayList<int> l = new();
            Assert.IsFalse(l.Contains(1));
            l.Add(1);
            l.Add(2);
            l.Add(3);
            Assert.IsTrue(l.Contains(1));
            Assert.IsTrue(l.Contains(2));
            Assert.IsTrue(l.Contains(3));
        }
    }
}