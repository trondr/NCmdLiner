using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests
{
    public class NonStaticTestCommands7
    {
        public ITestLogger TestLogger;

        [Command(Description = "NonStaticTestCommands7 description")]
        public int NonStaticCommand(
            [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "parameter 1 example", AlternativeName = "p1")] string parameter1,
            [OptionalCommandParameter(Description = "Optional parameter 2 description", ExampleValue = "parameter 2 example", AlternativeName = "p2", DefaultValue = "Default parameter 2 value")] string parameter2
            )
        {
            string msg = string.Format("Running NonStaticCommand(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }
    }
}