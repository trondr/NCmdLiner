using System;
using System.Text.RegularExpressions;
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
    [TestFixture(Category = "UnitTests")]
    public class CmdLineryRequiredCommandParameterWithAndWithoutAlternativeNameTests
    {
        [Test]
        public static void CmdLineryRequiredCommandParameterWithoutAlternativeNameTest()
        {
            var testCommand = new RequiredCommandParameterWithoutAlternativeNameTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;

            var stringMessenger = new StringMessenger();
            CmdLinery.Run(new object[] { testCommand },
                new[]
                {
                    "Help"
                }, new TestApplicationInfo(), stringMessenger, new HelpProvider(() => stringMessenger)).Wait();

            var helpMessage = stringMessenger.Message.ToString();

            Assert.IsFalse(Regex.IsMatch(helpMessage, @"Alternative\s+parameter\s+name:"));
        }

        public class RequiredCommandParameterWithoutAlternativeNameTestCommand
        {
            public ITestLogger TestLogger;

            [Command(Description = "ExampleValueWithFormatCharacterTestCommand description")]
            public int ExampleCommand(
                [RequiredCommandParameter(Description = "Required parameter 1 description", ExampleValue = "{d}")] string parameter1
            )
            {
                var msg = $"Running ExampleCommand(\"{parameter1}\")";
                Console.WriteLine(msg);
                TestLogger.Write(msg);
                return 10;
            }
        }


        [Test]
        public static void CmdLineryRequiredCommandParameterWithAlternativeNameTest()
        {
            var testCommand = new RequiredCommandParameterWithAlternativeNameTestCommand();
            var testLoggerMoc = new Mock<ITestLogger>();
            testCommand.TestLogger = testLoggerMoc.Object;

            var stringMessenger = new StringMessenger();
            CmdLinery.Run(new object[] { testCommand },
                new[]
                {
                    "Help"
                }, new TestApplicationInfo(), stringMessenger, new HelpProvider(() => stringMessenger)).Wait();

            var helpMessage = stringMessenger.Message.ToString();

            Assert.IsTrue(Regex.IsMatch(helpMessage,@"Alternative\s+parameter\s+name:"));

        }


        public class RequiredCommandParameterWithAlternativeNameTestCommand
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