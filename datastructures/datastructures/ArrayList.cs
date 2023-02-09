namespace datastructures
{
    public class ArrayList<T>
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

        public void ClearList()
        {
            _count = 0;
            Storage = new T[SizeInc];
        }

        public T[] ToArray()
        {
            T[] newArray = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                newArray[i] = Storage[i];
            }
            return newArray;
        }

        public void Insert(T data, int index)
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

        public void Append(T data)
        {
            Insert(data, _count);
        }

        public T Remove(T data)
        {
            if (_count == 0) throw new EmptyListException();
            for(int i = 0; i < _count; i++)
            {
                if (Storage[i] is { } d && d.Equals(data))
                {
                    RemoveAt(i);
                    return data;
                }
            }
            throw new NotFoundException();
        }

        public void RemoveAt(int index)
        {
            if (_count == 0) throw new EmptyListException();
            if (index >= _count) throw new IndexOutOfRangeException();
            for(int i = index; i<_count; i++)
            {
                //todo
            }
        }

        public int Find(T data)
        {
            return 0;
        }
    }
}