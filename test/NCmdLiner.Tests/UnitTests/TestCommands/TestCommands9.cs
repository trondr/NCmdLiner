using System;
using NCmdLiner.Attributes;
using NCmdLiner.Tests.Common;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommands9
    {
        public static ITestLogger TestLogger;

        [Command(Description = "Command with both summary and description defined", Summary = "Summary of command")]
        public static int CommandWithBothDescriptionAndSummaryDefined(            
            )
        {
            string msg = string.Format("Running CommandWithBothDescriptionAndSummaryDefined()");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }

        [Command(Description = "Command with only description defined. Summary should the be set to the same as the description.")]
        public static int CommandWithOnlyDescriptionAndNoSummaryDefined(            
            )
        {
            string msg = string.Format("Running CommandWithOnlyDescriptionAndNoSummaryDefined()");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            return 10;
        }

        [Command(Description = "Command with two required parameters and one optional paramter descritpion", Summary = "Summary of CommandWithTwoRequiredParameterAndOneOptionalParameter")]
        public static void CommandWithTwoRequiredParameterAndOneOptionalParameter
            (
            [RequiredCommandParameter(Description = "Some required parameter 1 description",
                ExampleValue = "Example value 1")] string someRequiredParameter1,
            [RequiredCommandParameter(Description = "Some required parameter 2 description",
                ExampleValue = "Example value 2")] string someRequiredParameter2,
            [OptionalCommandParameter(DefaultValue = false, ExampleValue = true)] bool someOptionalParameter
            )
        {
            throw new NotImplementedException();
        }
    }
}