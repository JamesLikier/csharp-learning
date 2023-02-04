using ds = datastructures;

namespace datastructures_test
{
    [TestClass]
    public class QueueTest
    {
        [TestMethod]
        public void AddTest()
        {
            ds.Queue<int> q = new();
            Assert.AreEqual(0, q.Count);
            q.Add(1);
            Assert.AreEqual(1, q.Count);
            q.Add(1);
            Assert.AreEqual(2, q.Count);
            q.Remove();
            q.Remove();
            q.Add(1);
            Assert.AreEqual(1, q.Count);
        }

        [TestMethod]
        public void RemoveTest()
        {
            ds.Queue<int> q = new();
            Assert.ThrowsException<Exception>(() => { q.Remove(); });
            q.Add(1);
            q.Add(2);
            q.Add(3);
            Assert.AreEqual(3, q.Count);
            Assert.AreEqual(1, q.Remove());
            Assert.AreEqual(2, q.Count);
            q.Add(4);
            Assert.AreEqual(2, q.Remove());
            Assert.AreEqual(2, q.Count);
            Assert.AreEqual(3, q.Remove());
            Assert.AreEqual(1, q.Count);
            q.Remove();
            Assert.AreEqual(0, q.Count);
            Assert.ThrowsException<Exception>(() => { q.Remove(); });
        }
    }
}
