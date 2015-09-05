// File: CmdLineryUnitTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using NCmdLiner.Exceptions;
using NCmdLiner.Tests.Multi1;
using NCmdLiner.Tests.Multi2;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCmdLiner.Tests
{
    [TestFixture, Category(TestCategory.UnitTests)]
    public class CmdLineryUnitTests
    {
        private static MockRepository _mockRepository;
        private static NonStaticTestCommands7 _nonStaticCommands;
        private static NonStaticAndStaticTestCommands8 _nonStaticAndStaticCommands;

        [SetUp]
        public static void SetUp()
        {
            _mockRepository = new MockRepository();
            TestCommands1.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommands2.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommands3.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommands4.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommands5.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommands6.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommandsMulti1.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommandsMulti2.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommandsMulti1Duplicate.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommandsMulti2Duplicate.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            _nonStaticCommands = new NonStaticTestCommands7();
            _nonStaticCommands.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            _nonStaticAndStaticCommands = new NonStaticAndStaticTestCommands8();
            NonStaticAndStaticTestCommands8.TestLogger = _mockRepository.StrictMock<ITestLogger>();


        }

        [TearDown]
        public static void TearDown()
        {
            _mockRepository.BackToRecordAll();
            _mockRepository = null;
        }

        [Test]
        public static void RunCommandWithNoParametersSuccess()
        {
            Expect.Call(TestCommands1.TestLogger.Write("Running CommandWithNoParameters")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1), new string[] { "CommandWithNoParameters" }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(NCmdLinerException), ExpectedMessage = "Concoler test exception message")]
        public static void RunCommandWithNoParametersThrowingException()
        {
            Expect.Call(TestCommands1.TestLogger.Write("Running CommandWithNoParametersThrowingException")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1), new string[] { "CommandWithNoParametersThrowingException" },
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void RunCommandWithRequiredStringParameterSet()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithRequiredStringParameter(\"required parameter1 value\")")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1),
                          new string[] { "CommandWithRequiredStringParameter", "/parameter1=\"required parameter1 value\"" },
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(MissingCommandParameterException),
            ExpectedMessage = "Required parameter is missing: parameter1")]
        public static void RunCommandWithRequiredStringParameterNotSet()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithRequiredStringParameter(\"required parameter1 value\")")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1), new string[] { "CommandWithRequiredStringParameter" },
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void RunCommandWithOptionalStringParameterSet()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithOptionalStringParameter(\"optional parameter1 value\")")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1),
                          new string[] { "CommandWithOptionalStringParameter", "/parameter1=\"optional parameter1 value\"" },
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void RunCommandWithOptionalStringParameterNotSet()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithOptionalStringParameter(\"parameter1 default value\")")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1), new string[] { "CommandWithOptionalStringParameter" },
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameter()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1),
                          new string[]
                              {
                                  "CommandWithOneRequiredAndOptionalStringParameter",
                                  "/parameter1=\"parameter 1 value\"",
                                  "/parameter2=\"parameter 2 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameterReversedOrder()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1),
                          new string[]
                              {
                                  "CommandWithOneRequiredAndOptionalStringParameter",
                                  "/parameter2=\"parameter 2 value\"",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void RunCommandWithOneRequiredAndOptionalStringParameterNotSet()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithOneRequiredAndOptionalStringParameter(\"parameter 1 value\",\"parameter 2 default value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1),
                          new string[] { "CommandWithOneRequiredAndOptionalStringParameter", "/parameter1=\"parameter 1 value\"" },
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }


        [ExpectedException(typeof(MissingExampleValueException))]
        [Test]
        public static void RunCommandWithOneRequiredStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            Expect.Call(
                TestCommands2.TestLogger.Write(
                    "Running CommandWithOneRequiredStringParameterWithoutExampleValue(\"parameter 1 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands2),
                          new string[]
                              {
                                  "CommandWithOneRequiredStringParameterWithoutExampleValue",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [ExpectedException(typeof(MissingExampleValueException))]
        [Test]
        public static void RunCommandWithOneOptionalStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            Expect.Call(
                TestCommands3.TestLogger.Write(
                    "Running CommandWithOneOptionalStringParameterWithoutExampleValue(\"parameter 1 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands3),
                          new string[]
                              {
                                  "CommandWithOneOptionalStringParameterWithoutExampleValue",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [ExpectedException(typeof(MissingDefaultValueException))]
        [Test]
        public static void RunCommandWithNoOptionalDefaultValueThrowMissingExampleValueException()
        {
            Expect.Call(
                TestCommands5.TestLogger.Write(
                    "Running CommandWithNoOptionalDefaultValue(\"parameter 1 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands5),
                          new string[]
                              {
                                  "CommandWithNoOptionalDefaultValue",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        //[ExpectedException(typeof(MissingDefaultValueException))]
        [Test]
        public static void RunCommandWithNullOptionalDefaultValue()
        {
            Expect.Call(
                TestCommands6.TestLogger.Write(
                    "Running CommandWithNullOptionalDefaultValue(\"parameter 1 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands6),
                          new string[]
                              {
                                  "CommandWithNullOptionalDefaultValue",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void ShowHelpTest()
        {
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof(TestCommands1), new string[] { "Help" }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void CommandWithReturnValueTest()
        {
            Expect.Call(TestCommands4.TestLogger.Write("Running CommandWithReturnValue(\"parameter 1 value\")")).Return(null);
            _mockRepository.ReplayAll();
            const int expected = 10;
            int actual = CmdLinery.Run(typeof(TestCommands4), new string[] { "CommandWithReturnValue", "/parameter1=\"parameter 1 value\"" }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
            Assert.AreEqual(expected, actual, "Return value was not equal.");
        }

        [Test]
        public static void CommandsFromMultipleNamespaces()
        {
            Expect.Call(TestCommandsMulti2.TestLogger.Write("Running SecondCommand()")).Return(null);
            _mockRepository.ReplayAll();
            const int expected = 10;
            int actual = CmdLinery.Run(new Type[] { typeof(TestCommandsMulti1), typeof(TestCommandsMulti2) }, new string[] { "SecondCommand" }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
            Assert.AreEqual(expected, actual, "Return value was not equal.");
        }
        
        [Test]
        [ExpectedException(typeof(DuplicateCommandException))]
        public static void CommandsFromMultipleNamespacesDuplicateCommandThrowDuplicateCommandException()
        {
            Expect.Call(TestCommandsMulti2Duplicate.TestLogger.Write("Running FirstCommand()")).Return(null);
            _mockRepository.ReplayAll();
            const int expected = 10;
            int actual = CmdLinery.Run(new Type[] { typeof(TestCommandsMulti1Duplicate), typeof(TestCommandsMulti2Duplicate) }, new string[] { "FirstCommand" }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
            Assert.AreEqual(expected, actual, "Return value was not equal.");
        }
        
        [Test]
        public static void RunNonStaticCommand()
        {
            Expect.Call(_nonStaticCommands.TestLogger.Write("Running NonStaticCommand(\"parameter 1 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(new object[]{ _nonStaticCommands},
                          new string[]
                              {
                                  "NonStaticCommand",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo(),new ConsoleMessenger());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void RunNonStaticAndStaticCommands()
        {
            Expect.Call(NonStaticAndStaticTestCommands8.TestLogger.Write("Running NonStaticCommand(\"parameter 1 value\")")).Return(null);
            Expect.Call(NonStaticAndStaticTestCommands8.TestLogger.Write("Running StaticCommand(\"parameter 1 value\")")).Return(null);

            _mockRepository.ReplayAll();
            int nonStaticResult = CmdLinery.Run(new object[] { _nonStaticAndStaticCommands },
                          new string[]
                              {
                                  "NonStaticCommand",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo(), new ConsoleMessenger());

            int staticResult = CmdLinery.Run(new object[] { _nonStaticAndStaticCommands },
                          new string[]
                              {
                                  "StaticCommand",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo(), new ConsoleMessenger());
            _mockRepository.VerifyAll();
            Assert.AreEqual(1,nonStaticResult);
            Assert.AreEqual(2, staticResult);

        }

        [Test]
        public static void RunNonStaticCommandWithParamenterHavingEqualCharacter()
        {
            Expect.Call(_nonStaticCommands.TestLogger.Write("Running NonStaticCommand(\"LDAP://OU=TST,OU=Groups,DC=tst,DC=local\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(new object[] { _nonStaticCommands },
                          new string[]
                              {
                                  "NonStaticCommand",
                                  "/parameter1=\"LDAP://OU=TST,OU=Groups,DC=tst,DC=local\""
                              }, new TestApplicationInfo(), new ConsoleMessenger());
            _mockRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(CustomTestMessengerException))]
        public static void RunHelpcCommandWithCustomMessenger()
        {
            Expect.Call(_nonStaticCommands.TestLogger.Write("Running NonStaticCommand()"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(new object[] { _nonStaticCommands },
                          new string[]
                              {
                                  "Help",                                  
                              }, new TestApplicationInfo(), new CustomTestMessenger(), new HelpProvider(() => new CustomTestMessenger()));
            _mockRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(CustomTestApplicationInfoException))]
        public static void RunHelpCommandWithCustomApplicationInfo()
        {
            Expect.Call(_nonStaticCommands.TestLogger.Write("Running NonStaticCommand()"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(new object[] { _nonStaticCommands },
                          new string[]
                              {
                                  "Help",
                              }, new CustomTestApplicationInfo(), new ConsoleMessenger(), new HelpProvider(() => new ConsoleMessenger()));
            _mockRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof(CustomTestHelpProviderException))]
        public static void RunHelpcCommandWithCustomHelperProvider()
        {
            Expect.Call(_nonStaticCommands.TestLogger.Write("Running NonStaticCommand()"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(new object[] { _nonStaticCommands },
                          new string[]
                              {
                                  "Help",
                              }, new TestApplicationInfo(), new ConsoleMessenger(), new CustomTestHelpProvider());
            _mockRepository.VerifyAll();
        }

    }

    public class CustomTestHelpProvider : IHelpProvider
    {
        public bool IsCreditsRequested(string commandName)
        {
            throw new CustomTestHelpProviderException();
        }

        public bool IsHelpRequested(string commandName)
        {
            throw new CustomTestHelpProviderException();
        }

        public bool IsLicenseRequested(string commandName)
        {
            throw new CustomTestHelpProviderException();
        }

        public void ShowCredits(IApplicationInfo applicationInfo)
        {
            throw new CustomTestHelpProviderException();
        }

        public void ShowHelp(List<CommandRule> commandRules, CommandRule helpForCommandRule, IApplicationInfo applicationInfo)
        {
            throw new CustomTestHelpProviderException();
        }

        public void ShowLicense(IApplicationInfo applicationInfo)
        {
            throw new CustomTestHelpProviderException();
        }
    }

    public class CustomTestHelpProviderException : Exception
    {
    }

    public class CustomTestApplicationInfo : IApplicationInfo
    {
        //private string _name;

        public string Name
        {
            get
            {
                throw new CustomTestApplicationInfoException();
                //return _name;
            }
            set
            {
                throw new CustomTestApplicationInfoException();
                //_name = value;
            }
        }

        public string Version { get; set; }
        public string Copyright { get; set; }
        public string Authors { get; set; }
        public string Description { get; set; }
        public string ExeFileName { get; set; }
    }

    public class CustomTestApplicationInfoException : Exception
    {
    }

    public class CustomTestMessenger : IMessenger
    {
        public void Write(string formatMessage, params object[] args)
        {
            throw new CustomTestMessengerException();
        }

        public void WriteLine(string formatMessage, params object[] args)
        {
            throw new CustomTestMessengerException();
        }

        public void Show()
        {
            throw new CustomTestMessengerException();
        }
    }

    public class CustomTestMessengerException : Exception
    {
    }
}