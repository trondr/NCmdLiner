using System;
using NCmdLiner.Tests.Common;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommandsMulti2
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandsMulti2 second partial command 2")]
        public static int SecondCommand()
        {
            string msg = string.Format("Running SecondCommand()");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }
    }
}