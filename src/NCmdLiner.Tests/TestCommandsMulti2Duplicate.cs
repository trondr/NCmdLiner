using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.Multi2
{
    public class TestCommandsMulti2Duplicate
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandsMulti2Duplicate first partial command 2")]
        public static int FirstCommand()
        {
            string msg = string.Format("Running FirstCommand()");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }
    }
}