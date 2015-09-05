using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests
{
    public class TestCommandsMulti1Duplicate
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandsMulti1Duplicate first partial command 1")]
        public static int FirstCommand()
        {
            string msg = string.Format("Running FirstCommand()");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }
    }
}