using System;
using System.Text;
using NCmdLiner;

namespace MyUtil.Extensions
{
    public class ConsoleMessenger: IMessenger
    {
        private readonly StringBuilder _message = new StringBuilder();
        private readonly IMessenger _defaultMessenger;

        public ConsoleMessenger(IMessenger defaultMessenger)
        {            
            _defaultMessenger = defaultMessenger;
        }

        public void Write(string formatMessage, params object[] args)
        {
            _message.Append(string.Format(formatMessage,args));
            _defaultMessenger.Write(formatMessage,args);
        }

        public void WriteLine(string formatMessage, params object[] args)
        {
            _message.Append(string.Format(formatMessage, args) + Environment.NewLine);
            _defaultMessenger.WriteLine(formatMessage, args);
        }

        public void Show()
        {
            System.Console.WriteLine(_message.ToString());            
        }
    }
}