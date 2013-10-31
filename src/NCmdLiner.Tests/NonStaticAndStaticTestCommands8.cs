using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests
{
    public class NonStaticAndStaticTestCommands8
    {
        public static ITestLogger TestLogger;

        [Command(Description = "NonStaticCommand description")]
        public int NonStaticCommand(
            [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "parameter 1 example", AlternativeName = "p1")] string parameter1,
            [OptionalCommandParameter(Description = "Optional parameter 2 description", ExampleValue = "parameter 2 example", AlternativeName = "p2", DefaultValue = "Default parameter 2 value")] string parameter2
            )
        {
            string msg = string.Format("Running NonStaticCommand(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 1;
        }

        [Command(Description = "StaticCommand description")]
        public static int StaticCommand(
            [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "parameter 1 example", AlternativeName = "p1")] string parameter1,
            [OptionalCommandParameter(Description = "Optional parameter 2 description", ExampleValue = "parameter 2 example", AlternativeName = "p2", DefaultValue = "Default parameter 2 value")] string parameter2
            )
        {
            string msg = string.Format("Running StaticCommand(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 2;
        }

    }
}