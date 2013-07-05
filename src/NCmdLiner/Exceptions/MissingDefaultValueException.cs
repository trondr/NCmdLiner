namespace NCmdLiner.Exceptions
{
    public class MissingDefaultValueException : NCmdLinerException
    {
        public MissingDefaultValueException(string message)
            : base(message)
        {
        }
    }
}