using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCmdLiner.Tests.UnitTests.Custom
{
    public class StringMessenger : IMessenger
    {
        public readonly StringBuilder Message = new StringBuilder();

        public void Write(string formatMessage, params object[] args)
        {
            Message.Append(string.Format(formatMessage, args));
            System.Console.Write(formatMessage,args);
        }

        public void WriteLine(string formatMessage, params object[] args)
        {
            Message.AppendLine(string.Format(formatMessage, args));
            System.Console.WriteLine(formatMessage, args);
        }

        public void Show()
        {
            //Do nothing since the Write and WriteLine methods allready have output the contents
        }
    }
}
