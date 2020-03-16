using System;
using System.Threading.Tasks;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommand10Async
    {
        [Command(
            Description =
                "ExampleCommand2 will do the same as ExampleCommand1 only echo value of the input parameters.")]
        public static async Task<int> AsyncCommand1(
            [RequiredCommandParameter(
                Description = "parameter1 is a required string parameter and must be specified.",
                ExampleValue = "Some example parameter1 value",
                AlternativeName = "p1"
            )] string parameter1
        )
        {
            Console.WriteLine("ExampleCommand2 just echoing the input parameters...");
            Console.WriteLine("parameter1={0}", parameter1);
            Console.WriteLine("Finished echoing the input parameters.");
            return await Task.FromResult(10);
        }
    }
}