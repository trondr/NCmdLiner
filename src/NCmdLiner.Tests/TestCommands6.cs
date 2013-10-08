using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests
{
    public class TestCommands6
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandWithNullOptionalDefaultValue description")]
        public static int CommandWithNullOptionalDefaultValue(
            [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "parameter 1 example", AlternativeName = "p1")] string parameter1,
            [OptionalCommandParameter(Description = "Optional parameter 2 description", ExampleValue = "parameter 2 example", AlternativeName = "p2", DefaultValue = null)] string parameter2
            )
        {
            string msg = string.Format("Running CommandWithNullOptionalDefaultValue(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }
    }
}