using System;
using Moq;
using NCmdLiner.Attributes;
using NCmdLiner.Tests.Common;


#if XUNIT
using Xunit;
using Test = Xunit.FactAttribute;
using TestFixture = NCmdLiner.Tests.Extensions.TestFixtureAttribute;
#else
using NUnit.Framework;
#endif
using Assert = NCmdLiner.Tests.Extensions.Assert;

namespace NCmdLiner.Tests.UnitTests
{
    [TestFixture(Category = "UnitTests")]
    public class CmdLineryRequiredCommandParameterAndExampleValueWithFormatCharactersTests
    {
        [Test]
        public static void CmdLineryRequiredCommandParameterAndExampleValueWithFormatCharactersTest()
        {
            var testCommand = new RequiredCommandParameterAndExampleValueWithFormatCharacterTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running ExampleCommand(\"{d}\")";

            CmdLinery.Run(new object[] { testCommand },
                new[]
                {
                    "ExampleCommand",
                    "/parameter1={d}",
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger())).Wait();

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }
        
        [Test]
        public static void CmdLineryRequiredCommandParameterAndExampleValueWithFormatCharactersHelpTest()
        {
            var testCommand = new RequiredCommandParameterAndExampleValueWithFormatCharacterTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            CmdLinery.Run(new object[] { testCommand },
                new[]
                {
                    "Help"                    
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger())).Wait();
        }

        public class RequiredCommandParameterAndExampleValueWithFormatCharacterTestCommand
        {
            public ITestLogger TestLogger;

            [Command(Description = "ExampleValueWithFormatCharacterTestCommand description")]
            public int ExampleCommand(
                [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "{d}", AlternativeName = "p1")] string parameter1
            )
            {
                var msg = $"Running ExampleCommand(\"{parameter1}\")";
                Console.WriteLine(msg);
                TestLogger.Write(msg);
                return 10;
            }
        }
    }
}