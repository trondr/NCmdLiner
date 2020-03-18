using System.Text;

namespace NCmdLiner.Tests.UnitTests.Custom
{
    public class StringMessenger : IMessenger
    {
        public readonly StringBuilder Message = new StringBuilder();

        public void Write(string formatMessage, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                Message.Append(formatMessage);
                System.Console.Write(formatMessage);
            }
            else
            {
                Message.Append(string.Format(formatMessage, args));
                System.Console.Write(formatMessage, args);
            }
        }

        public void WriteLine(string formatMessage, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                Message.AppendLine(formatMessage);
                System.Console.WriteLine(formatMessage);
            }
            else
            {
                Message.AppendLine(string.Format(formatMessage, args));
                System.Console.WriteLine(formatMessage, args);
            }
        }

        public void Show()
        {
            //Do nothing since the Write and WriteLine methods allready have output the contents
        }
    }
}
