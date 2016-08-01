namespace NCmdLiner.Tests.UnitTests.Custom
{
    public class CustomTestMessenger : IMessenger
    {
        public void Write(string formatMessage, params object[] args)
        {
            throw new CustomTestMessengerException();
        }

        public void WriteLine(string formatMessage, params object[] args)
        {
            throw new CustomTestMessengerException();
        }

        public void Show()
        {
            throw new CustomTestMessengerException();
        }
    }
}