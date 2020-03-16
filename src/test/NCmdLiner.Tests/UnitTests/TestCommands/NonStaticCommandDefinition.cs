using LanguageExt.Common;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class NonStaticCommandDefinition
    {
        [Command(Description = "Non static test command description", Summary = "Non static test command summary")]
        public Result<int> NonStaticTestCommand()
        {
            return new Result<int>(15);
        }
    }
}