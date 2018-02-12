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
    [TestFixture]
    public class CmdLineryRequiredCommandParameterAndExampleValueWithFormatCharactersTests
    {
        [Test]
        public static void CmdLineryRequiredCommandParameterAndExampleValueWithFormatCharactersTest()
        {
            var testCommand = new RequiredCommandParameterAndExampleValueWithFormatCharacterTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running ExampleCommand(\"{d}\")";

            CmdLinery.RunEx(new object[] { testCommand },
                new string[]
                {
                    "ExampleCommand",
                    "/parameter1={d}",
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }
        
        [Test]
        public static void CmdLineryRequiredCommandParameterAndExampleValueWithFormatCharactersHelpTest()
        {
            var testCommand = new RequiredCommandParameterAndExampleValueWithFormatCharacterTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            CmdLinery.RunEx(new object[] { testCommand },
                new string[]
                {
                    "Help"                    
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));
        }

        public class RequiredCommandParameterAndExampleValueWithFormatCharacterTestCommand
        {
            public ITestLogger TestLogger;

            [Command(Description = "ExampleValueWithFormatCharacterTestCommand description")]
            public int ExampleCommand(
                [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "{d}", AlternativeName = "p1")] string parameter1
            )
            {
                var msg = string.Format("Running ExampleCommand(\"{0}\")", parameter1);
                Console.WriteLine(msg);
                TestLogger.Write(msg);
                return 10;
            }
        }
    }
}