using System;
using NCmdLiner.Tests.Common;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommands5
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandWithNoOptionalDefaultValue description")]
        public static int CommandWithNoOptionalDefaultValue(
            [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "parameter 1 example", AlternativeName = "p1")] string parameter1,
            [OptionalCommandParameter(Description = "Optional parameter 2 description", ExampleValue = "parameter 2 example", AlternativeName = "p2")] string parameter2
            )
        {
            string msg = string.Format("Running CommandWithReturnValue(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }
        

    }
}