namespace NCmdLiner.Exceptions
{
    public class DuplicateCommandException : NCmdLinerException
    {
        public DuplicateCommandException(string message)
            : base(message)
        {
        }
    }
}