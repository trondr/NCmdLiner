using System;
using Moq;
using NCmdLiner.Attributes;
using NCmdLiner.Tests.Common;
using NCmdLiner.Tests.UnitTests.Custom;
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
    public class CmdLineryOptionalCommandParameterAndDefaultValueWithArrayValueTests
    {
        [Test]
        public static void CmdLineryOptionalCommandParameterAndDefaultValueWithArrayValueTest()
        {
            var testCommand = new OptionalCommandParameterAndDefaultValueWithArrayTestCommand();

            var stringMessenger = new StringMessenger();
            CmdLinery.RunEx(new object[] { testCommand },
                new string[]
                {
                    "Help"                    
                }, new TestApplicationInfo(), stringMessenger, new HelpProvider(() => stringMessenger));
            var helpMessage = stringMessenger.Message.ToString();

            Assert.IsTrue(helpMessage.Contains("['Default Value 1']"));
            Assert.IsTrue(helpMessage.Contains("['Example Value 1';'Example Value 2']"));
            Assert.IsFalse(helpMessage.Contains("System.String[]"));
        }

        [Test]
        public static void CmdLineryOptionalCommandParameterAndDefaultValueStringArrayTest()
        {
            var testCommand = new OptionalCommandParameterAndDefaultValueWithArrayTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running ExampleCommand(\"Default Value 1\")";

            CmdLinery.RunEx(new object[] { testCommand },
                new string[]
                {
                    "ExampleCommand"                    
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void CmdLineryOptionalCommandParameterAndDefaultValueIntegerArrayTest()
        {
            var testCommand = new OptionalCommandParameterAndDefaultValueWithArrayTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running ExampleCommand2(\"10\")";

            CmdLinery.RunEx(new object[] { testCommand },
                new string[]
                {
                    "ExampleCommand2"
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void CmdLineryOptionalCommandParameterAndDefaultValueIntegerArrayTest2()
        {
            var testCommand = new OptionalCommandParameterAndDefaultValueWithArrayTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running ExampleCommand2(\"13\")";

            CmdLinery.RunEx(new object[] { testCommand },
                new string[]
                {
                    "ExampleCommand2",
                    "/parameter1=['13';'14']"
                }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }


        public class OptionalCommandParameterAndDefaultValueWithArrayTestCommand
        {
            public ITestLogger TestLogger;

            [Command(Description = "ExampleValueWithFormatCharacterTestCommand description")]
            public int ExampleCommand(
                [OptionalCommandParameter(Description = "Optional parameter 1 description", ExampleValue = new string[] {"Example Value 1","Example Value 2"}, AlternativeName = "p1", DefaultValue = new string[] {"Default Value 1"})] string[] parameter1
            )
            {
                var msg = string.Format("Running ExampleCommand(\"{0}\")", parameter1[0]);
                Console.WriteLine(msg);
                TestLogger.Write(msg);
                return 10;
            }

            [Command(Description = "ExampleValueWithFormatCharacterTestCommand description")]
            public int ExampleCommand2(
                [OptionalCommandParameter(Description = "Optional parameter 1 description", ExampleValue = new int[] { 10, 11 }, AlternativeName = "p1", DefaultValue = new int[] { 10 })] int[] parameter1
            )
            {
                var msg = string.Format("Running ExampleCommand2(\"{0}\")", parameter1[0]);
                Console.WriteLine(msg);
                TestLogger.Write(msg);
                return 10;
            }
        }
    }
}