//// File: CmdLineryUnitTests.cs
//// Project Name: NCmdLiner.Tests
//// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
//// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
//// Credits: See the Credit folder in this project
//// Copyright © <github.com/trondr> 2013 
//// All rights reserved.

using System;
using Moq;
using NCmdLiner;
using NCmdLiner.Exceptions;
using NCmdLiner.Tests.Common;
using NCmdLiner.Tests.UnitTests.Custom;
using NCmdLiner.Tests.UnitTests.TestCommands;
using Xunit;
using Test = Xunit.FactAttribute;

namespace NCmdLiner.Tests.UnitTests
{
    public class CmdLineryTests
    {
        [Test]
        public static void RunCommandWithNoParametersSuccess()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNoParameters";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1), new string[] { "CommandWithNoParameters" }, new TestApplicationInfo());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]        
        public static void RunCommandWithNoParametersThrowingException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNoParametersThrowingException";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<NCmdLinerException>(() =>
            {
                CmdLinery.Run(typeof(TestCommands1), new string[] { "CommandWithNoParametersThrowingException" }, new TestApplicationInfo());
            });
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
                CmdLinery.Run(typeof(TestCommands1), new string[] {"CommandWithNoParametersThrowingException"}, new TestApplicationInfo());
            }
            catch (Exception ex)
            {
                Assert.Contains("TestCommands1.CommandWithNoParametersThrowingException",ex.StackTrace);
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
                          new string[] { "CommandWithRequiredStringParameter", "/parameter1=\"required parameter1 value\"" },
                          new TestApplicationInfo());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithRequiredStringParameterNotSet()
        {
            Assert.Throws<MissingCommandParameterException>(() =>
            {
                CmdLinery.Run(typeof(TestCommands1), new string[] { "CommandWithRequiredStringParameter" },new TestApplicationInfo());
            });
        }

        [Test]
        public static void RunCommandWithOptionalStringParameterSet()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOptionalStringParameter(\"optional parameter1 value\")";
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
                        CmdLinery.Run(typeof(TestCommands1),
                          new string[] { "CommandWithOptionalStringParameter", "/parameter1=\"optional parameter1 value\"" },
                          new TestApplicationInfo());
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
                          new string[] { "CommandWithOptionalStringParameter" },
                          new TestApplicationInfo());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameter()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 value\")";
            var commandString = new string[]
            {
                        "CommandWithOneRequiredAndOptionalStringParameter",
                        "/parameter1=\"parameter 1 value\"",
                        "/parameter2=\"parameter 2 value\""
            };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              commandString,
              new TestApplicationInfo());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);

        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameterReversedOrder()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 value\")";
            var commandString = new string[]
                              {
                                          "CommandWithOneRequiredAndOptionalStringParameter",
                                          "/parameter2=\"parameter 2 value\"",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              commandString,
              new TestApplicationInfo());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameterNotSet()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands1.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 default value\")";
            var commandString = new string[]
                              {
                                          "CommandWithOneRequiredAndOptionalStringParameter", "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands1),
              commandString,
              new TestApplicationInfo());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunCommandWithOneRequiredStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands2.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneRequiredStringParameterWithoutExampleValue(\"parameter 1 value\")";
            var commandString = new string[]
                              {
                                          "CommandWithOneRequiredStringParameterWithoutExampleValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<MissingExampleValueException>(() =>
            {
                CmdLinery.Run(typeof(TestCommands2), commandString, new TestApplicationInfo());
            });
            
        }

        [Test]
        public static void RunCommandWithOneOptionalStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands3.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithOneOptionalStringParameterWithoutExampleValue(\"parameter 1 value\")";
            var commandString = new string[]
                              {
                                           "CommandWithOneOptionalStringParameterWithoutExampleValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<MissingExampleValueException>(() =>
            {
                CmdLinery.Run(typeof(TestCommands3), commandString, new TestApplicationInfo());
            });
        }

        [Test]
        public static void RunCommandWithNoOptionalDefaultValueThrowMissingExampleValueException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands5.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNoOptionalDefaultValue(\"parameter 1 value\")";
            var commandString = new string[]
                              {
                                            "CommandWithNoOptionalDefaultValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<MissingDefaultValueException>(() =>
            {
                CmdLinery.Run(typeof(TestCommands5), commandString, new TestApplicationInfo());
            });
        }

        //        //[ExpectedException(typeof(MissingDefaultValueException))]
        [Test]
        public static void RunCommandWithNullOptionalDefaultValue()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands6.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithNullOptionalDefaultValue(\"parameter 1 value\")";
            var commandString = new string[]
                              {
                                   "CommandWithNullOptionalDefaultValue",
                                          "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            CmdLinery.Run(typeof(TestCommands6), commandString, new TestApplicationInfo());
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
            
        }

        [Test]
        public static void ShowHelpTest()
        {
            CmdLinery.Run(typeof(TestCommands1), new string[] { "Help" }, new TestApplicationInfo());
        }

        [Test]
        public static void CommandWithReturnValueTest()
        {
            const int expected = 10;
             var testLoggerMoc = new Mock<ITestLogger>();
            TestCommands4.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running CommandWithReturnValue(\"parameter 1 value\")";
            var commandString = new string[]
                              {
                                   "CommandWithReturnValue",
                                  "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            int actual = CmdLinery.Run(typeof(TestCommands4), commandString, new TestApplicationInfo());
            Assert.Equal(expected, actual);
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void CommandsFromMultipleNamespaces()
        {
            const int expected = 10;
             var testLoggerMoc = new Mock<ITestLogger>();
            TestCommandsMulti2.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running SecondCommand()";
            var commandString = new string[]
                              {
                                   "SecondCommand"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            int actual = CmdLinery.Run(new Type[] { typeof(TestCommandsMulti1), typeof(TestCommandsMulti2) }, commandString, new TestApplicationInfo());
            Assert.Equal(expected, actual);
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void CommandsFromMultipleNamespacesDuplicateCommandThrowDuplicateCommandException()
        {
            var testLoggerMoc = new Mock<ITestLogger>();
            TestCommandsMulti2Duplicate.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running FirstCommand()";
            var commandString = new string[]
                              {
                                            "FirstCommand"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<DuplicateCommandException>(() =>
            {
                CmdLinery.Run(new Type[] { typeof(TestCommandsMulti1Duplicate), typeof(TestCommandsMulti2Duplicate) }, commandString, new TestApplicationInfo());
            });


        }

        [Test]
        public static void RunNonStaticCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            const int expected = 10;
             var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand(\"parameter 1 value\")";
            var commandString = new string[]
                              {
                                "NonStaticCommand",
                                "/parameter1=\"parameter 1 value\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            int actual = CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new TestApplicationInfo(), new ConsoleMessenger());
            Assert.Equal(expected, actual);
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);
        }

        [Test]
        public static void RunNonStaticAndStaticCommands()
        {
            var nonStaticAndStaticCommands = new NonStaticAndStaticTestCommands8();

            var testLoggerMoc = new Mock<ITestLogger>();
            NonStaticAndStaticTestCommands8.TestLogger = testLoggerMoc.Object;
            const string logMessage1 = "Running NonStaticCommand(\"parameter 1 value\")";
            const string logMessage2 = "Running StaticCommand(\"parameter 1 value\")";
            testLoggerMoc.Setup(logger => logger.Write(logMessage1));
            testLoggerMoc.Setup(logger => logger.Write(logMessage2));
            
            int nonStaticResult = CmdLinery.Run(new object[] { nonStaticAndStaticCommands },
                          new string[]
                              {
                                          "NonStaticCommand",
                                          "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo(), new ConsoleMessenger());

            int staticResult = CmdLinery.Run(new object[] { nonStaticAndStaticCommands },
                          new string[]
                              {
                                          "StaticCommand",
                                          "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo(), new ConsoleMessenger());

            Assert.Equal(1, nonStaticResult);
            Assert.Equal(2, staticResult);
            testLoggerMoc.Verify(logger => logger.Write(logMessage1), Times.Once);

        }

        [Test]
        public static void RunNonStaticCommandWithParamenterHavingEqualCharacter()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            const int expected = 10;
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand(\"LDAP://OU=TST,OU=Groups,DC=tst,DC=local\")";
            var commandString = new string[]
                              {
                                "NonStaticCommand",
                                "/parameter1=\"LDAP://OU=TST,OU=Groups,DC=tst,DC=local\""
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            int actual = CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new TestApplicationInfo(), new ConsoleMessenger());
            Assert.Equal(expected, actual);
            testLoggerMoc.Verify(logger => logger.Write(logMessage), Times.Once);

        }

        [Test]        
        public static void RunHelpCommandWithCustomMessenger()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand()";
            var commandString = new string[]
                              {
                               "Help"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<CustomTestMessengerException>(() =>
            {
                CmdLinery.Run(new object[] {nonStaticTestCommands}, commandString, new TestApplicationInfo(), new CustomTestMessenger(), new HelpProvider(() => new CustomTestMessenger()));
            });            
        }

        [Test]
        public static void RunHelpCommandWithCustomApplicationInfo()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand()";
            var commandString = new string[]
                              {
                               "Help"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<CustomTestApplicationInfoException>(() =>
            {
                CmdLinery.Run(new object[] {nonStaticTestCommands}, commandString, new CustomTestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));
            });
        }

        [Test]
        public static void RunHelpCommandWithCustomHelperProvider()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            var testLoggerMoc = new Mock<ITestLogger>();
            nonStaticTestCommands.TestLogger = testLoggerMoc.Object;
            const string logMessage = "Running NonStaticCommand()";
            var commandString = new string[]
                              {
                               "Help"
                              };
            testLoggerMoc.Setup(logger => logger.Write(logMessage));
            Assert.Throws<CustomTestHelpProviderException>(() =>
            {
                CmdLinery.Run(new object[] { nonStaticTestCommands }, commandString, new TestApplicationInfo(), new ConsoleMessenger(), new CustomTestHelpProvider());
            });
        }

        [Test]
        public static void RunHelpCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            CmdLinery.Run(new object[] { nonStaticTestCommands },
                          new string[]
                              {
                                          "Help",
                              }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));
        }

        [Test]
        public static void RunLicenseCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            CmdLinery.Run(new object[] { nonStaticTestCommands },
                          new string[]
                              {
                                          "License",
                              }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));
        }

        [Test]
        public static void RunCreditsCommand()
        {
            var nonStaticTestCommands = new NonStaticTestCommands7();
            CmdLinery.Run(new object[] { nonStaticTestCommands },
                          new string[]
                              {
                                          "Credits",
                              }, new TestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));
        }
    }
}