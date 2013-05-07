using System;
using System.Text;

namespace NCmdLiner.Example
{
    public class MyDialogMessenger: IMessenger
    {
        private readonly StringBuilder _message = new StringBuilder();
        private readonly IMessenger _defaultMessenger;

        public MyDialogMessenger(IMessenger defaultMessenger)
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
            MessengerForm form = new MessengerForm(_message.ToString());
            form.Text = "NCmdLiner.Example";
            form.ShowDialog();
        }
    }
}