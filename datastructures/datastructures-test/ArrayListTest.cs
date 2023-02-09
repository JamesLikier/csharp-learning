namespace datastructures_test
{
    using datastructures;
    using ds = datastructures;
    [TestClass]
    public class ArrayListTest
    {
        [TestMethod]
        public void ClearListTest()
        {
            ds.ArrayList<int> l = new();
            l.Insert(1, 0);
            l.ClearList();
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            ds.ArrayList<int> l = new();
            l.Insert(1, 0);
            Assert.AreEqual(true, l.ToArray() is [1]);
            l.Insert(2, 0);
            Assert.AreEqual(true, l.ToArray() is [2, 1]);
            l.Insert(3, 2);
            Assert.AreEqual(true, l.ToArray() is [2, 1, 3]);
            l.Insert(4, 1);
            Assert.AreEqual(true, l.ToArray() is [2, 4, 1, 3]);
            l = new();
            for(int i = 0; i < 15; i++)
            {
                l.Insert(1, i);
            }
            Assert.AreEqual(15, l.Count);
            Assert.ThrowsException<IndexOutOfRangeException>(()=>{l.Insert(1,-1);});
            Assert.ThrowsException<IndexOutOfRangeException>(()=>{l.Insert(1,l.Count+1);});
        }
        [TestMethod]
        public void AppendTest()
        {
            ds.ArrayList<int> l = new();
            l.Append(3);
            l.Append(2);
            l.Append(1);
            Assert.AreEqual(true, l.ToArray() is [3, 2, 1]);
        }
        [TestMethod]
        public void RemoveTest()
        {
            ds.ArrayList<int> l = new();
            Assert.ThrowsException<EmptyListException>(() => l.Remove(1));
            l.Append(1);
            l.Append(2);
            l.Append(3);
            Assert.AreEqual(1, l.Remove(1));
            Assert.ThrowsException<NotFoundException>(() => l.Remove(1));
            Assert.AreEqual(true, l.ToArray() is [2, 3]);
        }
        [TestMethod]
        public void RemoveAtTest()
        {
            //todo
        }
        [TestMethod]
        public void FindTest()
        {
            //todo
        }
    }
}