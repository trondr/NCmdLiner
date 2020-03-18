using System;
using NCmdLiner.Tests.Common;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommandsMulti1
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandsMulti1 first partial command 1")]
        public static int FirstCommand()
        {
            string msg = string.Format("Running FirstCommand()");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }
    }
}