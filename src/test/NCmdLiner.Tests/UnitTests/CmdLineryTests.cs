//// File: CmdLineryUnitTests.cs
//// Project Name: NCmdLiner.Tests
//// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
//// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
//// Credits: See the Credit folder in this project
//// Copyright � <github.com/trondr> 2013 
//// All rights reserved.

using System;
using System.IO;
using System.Threading.Tasks;
using LanguageExt.Common;
using Moq;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;
using NCmdLiner.Tests.Common;
using NCmdLiner.Tests.UnitTests.Custom;
using NCmdLiner.Tests.UnitTests.TestCommands;

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
    public class CmdLineryTests
    {
        [Test]
        public static void RunCommandWithNoParametersSuccess()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNoParameters";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1), new[] { "CommandWithNoParameters" }, new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static async Task RunCommandWithNoParametersThrowingException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNoParametersThrowingException";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));

            var result = await CmdLinery.Run(typeof(TestCommands1), new[] { "CommandWithNoParametersThrowingException" }, new TestApplicationInfo());
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(typeof(NCmdLinerException), result.ToException().GetType());
            Assert.Contains("Concoler test exception message", result.ToException().Message);

            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithNoParametersThrowingExceptionCheckStackTrace()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNoParametersThrowingException";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            try
            {
                CmdLinery.Run(typeof(TestCommands1), new[] { "CommandWithNoParametersThrowingException" }, new TestApplicationInfo()).Wait();
            }
            catch (Exception ex)
            {
                Assert.Contains("TestCommands1.CommandWithNoParametersThrowingException", ex.StackTrace);
            }
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithRequiredStringParameterSet()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithRequiredStringParameter(\"required parameter1 value\")";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              new[] { "CommandWithRequiredStringParameter", "/parameter1=\"required parameter1 value\"" },
              new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static async Task RunCommandWithRequiredStringParameterNotSet()
        {
            var result = await CmdLinery.Run(typeof(TestCommands1), new[] { "CommandWithRequiredStringParameter" }, new TestApplicationInfo());
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("Required parameter is missing: parameter1", result.ToException().Message);

        }

        [Test]
        public static void RunCommandWithOptionalStringParameterSet()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOptionalStringParameter(\"optional parameter1 value\")";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              new[] { "CommandWithOptionalStringParameter", "/parameter1=\"optional parameter1 value\"" },
              new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithOptionalStringParameterNotSet()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOptionalStringParameter(\"parameter1 default value\")";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              new[] { "CommandWithOptionalStringParameter" },
              new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameter()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 value\")";
            var commandString = new[]
            {
                        "CommandWithOneRequiredAndOptionalStringParameter",
                        "/parameter1=\"parameter 1 value\"",
                        "/parameter2=\"parameter 2 value\""
            };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              commandString,
              new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);

        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameterReversedOrder()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 value\")";
            var commandString = new[]
                              {
                                          "CommandWithOneRequiredAndOptionalStringParameter",
                                          "/parameter2=\"parameter 2 value\"",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              commandString,
              new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameterNotSet()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 default value\")";
            var commandString = new[]
                              {
                                          "CommandWithOneRequiredAndOptionalStringParameter", "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              commandString,
              new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static async Task RunCommandWithOneRequiredStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands2.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredStringParameterWithoutExampleValue(\"parameter 1 value\")";
            var commandString = new[]
                              {
                                          "CommandWithOneRequiredStringParameterWithoutExampleValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));

            var result = await CmdLinery.Run(typeof(TestCommands2), commandString, new TestApplicationInfo());
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(typeof(MissingExampleValueException),result.ToException().GetType());
            Assert.AreEqual("Example value has not been specified for parameter 'parameter1' in command 'CommandWithOneRequiredStringParameterWithoutExampleValue'", result.ToException().Message);


        }

        [Test]
        public static async Task RunCommandWithOneOptionalStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands3.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneOptionalStringParameterWithoutExampleValue(\"parameter 1 value\")";
            var commandString = new[]
                              {
                                           "CommandWithOneOptionalStringParameterWithoutExampleValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));

            var result = await CmdLinery.Run(typeof(TestCommands3), commandString, new TestApplicationInfo());
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(typeof(MissingExampleValueException), result.ToException().GetType());
            Assert.AreEqual("Example value has not been specified for parameter 'parameter1' in command 'CommandWithOneOptionalStringParameterWithoutExampleValue'", result.ToException().Message);

        }

        [Test]
        public static async Task RunCommandWithNoOptionalDefaultValueThrowMissingExampleValueException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands5.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNoOptionalDefaultValue(\"parameter 1 value\")";
            var commandString = new[]
                              {
                                            "CommandWithNoOptionalDefaultValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));

            var result = await CmdLinery.Run(typeof(TestCommands5), commandString, new TestApplicationInfo());
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual(typeof(MissingDefaultValueException), result.ToException().GetType());
            Assert.AreEqual("Missing default value for optional parameter with alternative name 'p2'", result.ToException().Message);

        }

        //        //[ExpectedException(typeof(MissingDefaultValueException))]
        [Test]
        public static void RunCommandWithNullOptionalDefaultValue()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands6.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNullOptionalDefaultValue(\"parameter 1 value\")";
            var commandString = new[]
                              {
                                   "CommandWithNullOptionalDefaultValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands6), commandString, new TestApplicationInfo()).Wait();
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);

        }

        [Test]
        public static void ShowHelpTest()
        {
            CmdLinery.Run(typeof(TestCommands1), new[] { "Help" }, new TestApplicationInfo()).Wait();
        }

        [Test]
        public static async Task CommandWithReturnValueTest()
        {
            const int expected = 10;
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands4.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithReturnValue(\"parameter 1 value\")";
            var commandString = new[]
                              {
                                   "CommandWithReturnValue",
                                  "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            var actual = await CmdLinery.Run(typeof(TestCommands4), commandString, new TestApplicationInfo());
            Assert.AreEqual(expected, actual.ToValue());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static async Task CommandsFromMultipleNamespaces()
        {
            const int expected = 10;
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommandsMulti2.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running SecondCommand()";
            var commandString = new[]
                              {
                                   "SecondCommand"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            var actual = await CmdLinery.Run(new[] { typeof(TestCommandsMulti1), typeof(TestCommandsMulti2) }, commandString, new TestApplicationInfo());
            Assert.AreEqual(expected, actual.ToValue());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static async Task CommandsFromMultipleNamespacesDuplicateCommandThrowDuplicateCommandException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommandsMulti2Duplicate.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running FirstCommand()";
            var commandString = new[]
                              {
                                            "FirstCommand"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            var actual = await CmdLinery.Run(new[] { typeof(TestCommandsMulti1Duplicate), typeof(TestCommandsMulti2Duplicate) }, commandString, new TestApplicationInfo());
            Assert.IsFalse(actual.IsSuccess);
            Assert.AreEqual(typeof(DuplicateCommandException),actual.ToException().GetType());
            Assert.AreEqual("A duplicate command has been defined: FirstCommand", actual.ToException().Message);
        }

        [Test]
        public static async Task RunNonStaticCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            const int expected = 10;
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand(\"parameter 1 value\")";
            var commandString = new[]
                              {
                                "NonStaticCommand",
                                "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            var actual = await CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new TestApplicationInfo(), new ConsoleMessenger());
            Assert.AreEqual(expected, actual.ToValue());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static async Task RunNonStaticAndStaticCommands()
        {
            var nonStaticAndStaticCommands = new NonStaticAndStaticTestCommands8();

            var testLoggerMoc = new Mock<ITestLogger>();
            NonStaticAndStaticTestCommands8.TestLogger = testLoggerMoc.Object;
            const string logMessage1 = "Running NonStaticCommand(\"parameter 1 value\")";
            const string logMessage2 = "Running StaticCommand(\"parameter 1 value\")";
            testLoggerMoc.Setup(logger => logger.Write(logMessage1));
            testLoggerMoc.Setup(logger => logger.Write(logMessage2));

            var nonStaticResult = await CmdLinery.Run(new object[] { nonStaticAndStaticCommands },
                          new[]
                              {
                                          "NonStaticCommand",
                                          "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo(), new ConsoleMessenger());

            var staticResult = await CmdLinery.Run(new object[] { nonStaticAndStaticCommands },
                          new[]
                              {
                                          "StaticCommand",
                                          "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo(), new ConsoleMessenger());

            Assert.AreEqual(1, nonStaticResult.ToValue());
            Assert.AreEqual(2, staticResult.ToValue());
            testLoggerMoc.Verify(logger => logger.Write(logMessage1), Times.Once);

        }

        [Test]
        public static async Task RunNonStaticCommandWithParameterHavingEqualCharacter()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            const int expected = 10;
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand(\"LDAP://OU=TST,OU=Groups,DC=tst,DC=local\")";
            var commandString = new[]
                              {
                                "NonStaticCommand",
                                "/parameter1=\"LDAP://OU=TST,OU=Groups,DC=tst,DC=local\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            var actual = await CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new TestApplicationInfo(), new ConsoleMessenger());
            Assert.AreEqual(expected, actual.ToValue());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);

        }

        [Test]
        public static void RunHelpCommandWithCustomMessenger()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand()";
            var commandString = new[]
                              {
                               "Help"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.ThrowsAggregateExceptionWith<CustomTestMessengerException>(() =>
            {
                CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new TestApplicationInfo(), new CustomTestMessenger(), new HelpProvider(() => new CustomTestMessenger())).Wait();
            });
        }

        [Test]
        public static void RunHelpCommandWithCustomApplicationInfo()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand()";
            var commandString = new[]
                              {
                               "Help"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.ThrowsAggregateExceptionWith<CustomTestApplicationInfoException>(() =>
            {
                CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new CustomTestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger())).Wait();
            });
        }

        [Test]
        public static void RunHelpCommandWithCustomHelperProvider()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand()";
            var commandString = new[]
                              {
                               "Help"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.ThrowsAggregateExceptionWith<CustomTestHelpProviderException>(() =>
            {
                CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new TestApplicationInfo(), new ConsoleMessenger(), new CustomTestHelpProvider()).Wait();
            });
        }

        [Test]
        public static void RunHelpCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            CmdLinery.Run(new object[] { nonStaticTestCommands },
                          new[]
                              {
                                          "Help",
                              }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger())).Wait();
        }

        [Test]
        public static void RunLicenseCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            CmdLinery.Run(new object[] { nonStaticTestCommands },
                          new[]
                              {
                                          "License",
                              }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger())).Wait();
        }

        [Test]
        public static void RunCreditsCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            CmdLinery.Run(new object[] { nonStaticTestCommands },
                          new[]
                              {
                                          "Credits",
                              }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger())).Wait();
        }
        [Test]
        public static async Task RunCommandThatThrowsAnException()
        {
            string[] args = { "SomeCommandThrowingAnException" };
            var result = await CmdLinery.Run(typeof(TestCommandThrowingCustomException), args);
            Assert.AreEqual(false, result.IsSuccess);
            Assert.AreEqual("Testing. Some example was not found.", result.ToException().Message);
            Assert.AreEqual(typeof(FileNotFoundException), result.ToException().GetType());
        }


        [Test]
        public static async Task RunCommandReturnsFailureResult()
        {
            string[] args = { "SomeCommandReturningFailureResult" };
            var result = await CmdLinery.Run(typeof(TestCommandReturningFailureResult), args);
            Assert.AreEqual(false, result.IsSuccess);
            Assert.AreEqual("Testing. Some example was not found.", result.ToException().Message);
            Assert.AreEqual(typeof(FileNotFoundException), result.ToException().GetType());
        }

        [Test]
        public static async Task RunCommandOnNonStaticCommandDefinitionClass()
        {
            var actualResult = await CmdLinery.Run(typeof(NonStaticCommandDefinition), new []{ "NonStaticTestCommand" });
            Assert.IsTrue(actualResult.IsFaulted);
            Assert.IsTrue(actualResult.ToException().Message.StartsWith("The command '"), "Exception message does not start with 'Command ''");
            Assert.AreEqual(actualResult.ToException().GetType(), typeof(NCmdLinerException));
        }

        [Test]
        public static async Task RunAsyncCommand1()
        {
            var actualResult = await CmdLinery.Run(typeof(TestCommand10Async), new[] { "AsyncCommand1", "/parameter1=\"parameter 1 value\"" });
            var actual = actualResult.Match(i => i,
                exception => 1);
            Assert.AreEqual(10, actual, "Exit code not expected");
        }


    }
}