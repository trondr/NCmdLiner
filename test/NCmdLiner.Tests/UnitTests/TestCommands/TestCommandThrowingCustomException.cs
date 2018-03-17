using System.IO;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommandThrowingCustomException
    {
        [Command(Description = "Some command that throws an exception.")]
        public static Result<int> SomeCommandThrowingAnException()
        {
            throw new FileNotFoundException("Testing. Some example was not found.");
        }
    }
}
