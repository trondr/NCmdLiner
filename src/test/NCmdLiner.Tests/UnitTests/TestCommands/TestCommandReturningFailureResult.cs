using System.IO;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommandReturningFailureResult
    {
        [Command(Description = "Some command returning failure result.")]
        public static Result<int> SomeCommandReturningFailureResult()
        {
            return Result.Fail<int>(new FileNotFoundException("Testing. Some example was not found."));
        }
    }
}