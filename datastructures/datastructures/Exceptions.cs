namespace datastructures
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
    public class EmptyListException : Exception
    {
        public EmptyListException() : base() { }
        public EmptyListException(string message) : base(message) { }
        public EmptyListException(string message, Exception innerException) : base(message, innerException) { }
    }
}