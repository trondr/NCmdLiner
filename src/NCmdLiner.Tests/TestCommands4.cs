using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests
{
    public class TestCommands4
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandWithReturnValue description")]
        public static int CommandWithReturnValue(
            [OptionalCommandParameter(Description = "Optional parameter 1 description",ExampleValue = "parameter 1 example",AlternativeName = "p1", DefaultValue = "Default value string")] string parameter1
            )
        {
            string msg = string.Format("Running CommandWithReturnValue(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }        
    }
}