namespace datastructures
{
    using System.Collections;
    using System.Collections.Generic;
    public class ArrayList<T> : IList<T>
    {
        private int _count;
        public int Count { get { return _count;} }
        private T[] Storage;
        private const int SizeInc = 10;

        public ArrayList()
        {
            _count = 0;
            Storage = new T[SizeInc];
        }

        public bool IsReadOnly { get; }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; i++) yield return Storage[i];
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            _count = 0;
            Storage = new T[SizeInc];
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            for(int i = 0; i < _count; i++) array[i+arrayIndex] = Storage[i];
        }

        public T this[int index]
        {
            get 
            {
                if (index < 0 || index >= _count) throw new IndexOutOfRangeException();
                return Storage[index]; 
            }
            set
            {
                Insert(index, value);
            }
        }
        public void Insert(int index, T data)
        {
            if (index > _count && index != 0) throw new IndexOutOfRangeException();
            //need to resize array
            if (_count == Storage.Length)
            {
                T[] newStorage = new T[Storage.Length + SizeInc];
                //insert at tail
                if (index == _count)
                {
                    for(int i = 0; i < index; i++)
                    {
                        newStorage[i] = Storage[i];
                    }
                    newStorage[index] = data;
                }
                //make a hole for insertion
                else
                {
                    for(int i = 0; i < index; i++)
                    {
                        newStorage[i] = Storage[i];
                    }
                    newStorage[index] = data;
                    for(int i = index + 1; i < _count+1; i++)
                    {
                        newStorage[i] = Storage[i - 1];
                    }    
                }
                Storage = newStorage;
            }
            //do not need to resize
            else
            {
                //tail insertion
                if (index == _count)
                {
                    Storage[index] = data;
                }
                //make a hole for insertion
                else
                {
                    for (int i = _count; i > index; i--)
                    {
                        Storage[i] = Storage[i-1];
                    }
                    Storage[index] = data;
                }
            }
            _count++;
        }

        public void Add(T data)
        {
            this[_count] = data;
        }

        public bool Remove(T data)
        {
            int index = IndexOf(data);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index >= _count || index < 0) throw new IndexOutOfRangeException();
            //if index is not tail, move everything backwards
            for(int i = index; i<(_count - 1); i++)
            {
                Storage[i] = Storage[i+1];
            }
            _count--;
        }

        public int IndexOf(T data)
        {
            for(int i = 0; i < _count; i++)
            {
                if(Storage[i] is { } n && n.Equals(data)) return i;
            }
            return -1;
        }

        public bool Contains(T data)
        {
            foreach(T d in this) if (d is { } dd && dd.Equals(dd)) return true;
            return false;
        }
    }
}