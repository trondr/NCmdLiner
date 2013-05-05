// File: CmdLineryUnitTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using NCmdLiner.Exceptions;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCmdLiner.Tests
{
    [TestFixture, Category(TestCategory.UnitTests)]
    public class CmdLineryUnitTests
    {
        private static MockRepository _mockRepository;

        [SetUp]
        public static void SetUp()
        {
            _mockRepository = new MockRepository();
            TestCommands1.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommands2.TestLogger = _mockRepository.StrictMock<ITestLogger>();
            TestCommands3.TestLogger = _mockRepository.StrictMock<ITestLogger>();
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
            CmdLinery.Run(typeof (TestCommands1), new string[] {"CommandWithNoParameters"}, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (NCmdLinerException), ExpectedMessage = "Concoler test exception message")]
        public static void RunCommandWithNoParametersThrowingException()
        {
            Expect.Call(TestCommands1.TestLogger.Write("Running CommandWithNoParametersThrowingException")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof (TestCommands1), new string[] {"CommandWithNoParametersThrowingException"},
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
            CmdLinery.Run(typeof (TestCommands1),
                          new string[]
                              {"CommandWithRequiredStringParameter", "/parameter1=\"required parameter1 value\""},
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        [ExpectedException(typeof (MissingCommandParameterException),
            ExpectedMessage = "Required parameter is missing: parameter1")]
        public static void RunCommandWithRequiredStringParameterNotSet()
        {
            Expect.Call(
                TestCommands1.TestLogger.Write(
                    "Running CommandWithRequiredStringParameter(\"required parameter1 value\")")).Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof (TestCommands1), new string[] {"CommandWithRequiredStringParameter"},
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
            CmdLinery.Run(typeof (TestCommands1),
                          new string[]
                              {"CommandWithOptionalStringParameter", "/parameter1=\"optional parameter1 value\""},
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
            CmdLinery.Run(typeof (TestCommands1), new string[] {"CommandWithOptionalStringParameter"},
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
            CmdLinery.Run(typeof (TestCommands1),
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
            CmdLinery.Run(typeof (TestCommands1),
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
            CmdLinery.Run(typeof (TestCommands1),
                          new string[]
                              {"CommandWithOneRequiredAndOptionalStringParameter", "/parameter1=\"parameter 1 value\""},
                          new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }


        [ExpectedException(typeof (MissingExampleValueException))]
        [Test]
        public static void RunCommandWithOneRequiredStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            Expect.Call(
                TestCommands2.TestLogger.Write(
                    "Running CommandWithOneRequiredStringParameterWithoutExampleValue(\"parameter 1 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof (TestCommands2),
                          new string[]
                              {
                                  "CommandWithOneRequiredStringParameterWithoutExampleValue",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [ExpectedException(typeof (MissingExampleValueException))]
        [Test]
        public static void RunCommandWithOneOptionalStringParameterWithoutExampleValueThrowMissingExampleValueException()
        {
            Expect.Call(
                TestCommands3.TestLogger.Write(
                    "Running CommandWithOneOptionalStringParameterWithoutExampleValue(\"parameter 1 value\")"))
                  .Return(null);
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof (TestCommands3),
                          new string[]
                              {
                                  "CommandWithOneOptionalStringParameterWithoutExampleValue",
                                  "/parameter1=\"parameter 1 value\""
                              }, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }

        [Test]
        public static void ShowHelpTest()
        {
            _mockRepository.ReplayAll();
            CmdLinery.Run(typeof (TestCommands1), new string[] {"Help"}, new TestApplicationInfo());
            _mockRepository.VerifyAll();
        }
    }
}