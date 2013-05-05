namespace NCmdLiner
{
    public class ConsoleMessenger : IMessenger
    {
        public void Write(string formatMessage, params object[] args)
        {
            System.Console.Write(formatMessage,args);
        }

        public void WriteLine(string formatMessage, params object[] args)
        {
            System.Console.WriteLine(formatMessage, args);
        }

        public void Show()
        {
            //Do nothing since the Write and WriteLine methods allready have output the contents
        }
    }
}