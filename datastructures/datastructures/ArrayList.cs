namespace datastructures
{
    public class ArrayList<T>
    {
        private int _count;
        public int Count { get { return _count;} }
        private T[] Storage;
        private const SizeInc = 10;

        public ArrayList()
        {
            ClearList();
        }

        public void ClearList()
        {
            _count = 0;
            Storage = new T[SizeInc];
        }

        private T[] Resize()
        {
            return new T[Storage.Length + SizeInc];
        }

        public void Insert(T data, int index)
        {
            if (index > _count) throw new IndexOutOfRangeException();
            if (_count == Storage.Length) 
            {
            }
        }
    }
}