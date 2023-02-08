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
            ds.ArrayList<int> l = new ds.ArrayList<int>();
            l.Insert(1, 0);
            l.ClearList();
            Assert.AreEqual(0, l.Count);
        }
        [TestMethod]
        public void InsertTest()
        {
            ds.ArrayList<int> l = new ds.ArrayList<int>();
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
            //wip
        }
    }
}