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
    public class CmdLineryOptionalCommandParameterAndExampleValueWithFormatCharactersTests
    {
        [Test]
        public static void CmdLineryOptionalCommandParameterAndExampleValueWithFormatCharactersTest()
        {
            var testCommand = new OptionalCommandParameterAndExampleValueWithFormatCharacterTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running ExampleCommand(\"{d}\")";

            CmdLinery.Run(new object[] { testCommand },
                new string[]
                {
                    "ExampleCommand",
                    "/parameter1={d}",
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }
        
        [Test]
        public static void CmdLineryOptionalCommandParameterAndExampleValueWithFormatCharacters_WithoutParameterTest()
        {
            var testCommand = new OptionalCommandParameterAndExampleValueWithFormatCharacterTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running ExampleCommand(\"{de}\")";

            CmdLinery.Run(new object[] { testCommand },
                new string[]
                {
                    "ExampleCommand"
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        public class OptionalCommandParameterAndExampleValueWithFormatCharacterTestCommand
        {
            public ITestLogger TestLogger;

            [Command(Description = "ExampleValueWithFormatCharacterTestCommand description")]
            public int ExampleCommand(
                [OptionalCommandParameter(Description = "Optional parameter 1 description", ExampleValue = "{d}", AlternativeName = "p1", DefaultValue = "{de}")] string parameter1
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
