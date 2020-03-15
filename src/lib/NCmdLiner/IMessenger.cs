namespace NCmdLiner
{
    public interface IMessenger
    {
        void Write(string formatMessage, params object[] args);

        void WriteLine(string formatMessage, params object[] args);

        void Show();
    }
}
