using System.IO;
using LanguageExt.Common;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommandReturningFailureResult
    {
        [Command(Description = "Some command returning failure result.")]
        public static Result<int> SomeCommandReturningFailureResult()
        {
            return new Result<int>(new FileNotFoundException("Testing. Some example was not found."));
        }
    }
}