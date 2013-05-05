// File: CommandRuleProviderUnitTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCmdLiner.Tests
{
    [TestFixture, Category(TestCategory.UnitTests)]
    public class CommandRuleProviderUnitTests
    {
        private static MockRepository _mockRepository;

        [SetUp]
        public static void SetUp()
        {
            _mockRepository = new MockRepository();
        }

        [TearDown]
        public static void TearDown()
        {
            _mockRepository.BackToRecordAll();
            _mockRepository = null;
        }

        [Test]
        [ExpectedException(typeof (CommandMehtodNotStaticException))]
        public static void GetCommandRuleMetodHasNoCommandAttributeThrowCommandMehtodNotStaticExceptionUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            target.GetCommandRule(typeof (TestCommands0).GetMethod("CommandNotStatic"));
        }

        [Test]
        [ExpectedException(typeof (MissingCommandAttributeException))]
        public static void
            GetCommandRuleMetodHasNoCommandAttributeThrowMissingCommandAttributeExceptionExceptionUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            target.GetCommandRule(typeof (TestCommands0).GetMethod("CommandWithoutCommandAttribute"));
        }

        [Test]
        [ExpectedException(typeof (MissingCommandParameterAttributeException))]
        public static void
            GetCommandRuleMetodHasValidCommandWithOneParameterWithoutParameterAttributeThrowMissingCommandParameterAttributeExceptionUnitTest
            ()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            target.GetCommandRule(typeof (TestCommands0).GetMethod("CommandWithOneParameterWithoutParameterAttribute"));
        }

        [Test]
        [ExpectedException(typeof (DuplicateCommandParameterAttributeException))]
        public static void
            GetCommandRuleMetodHasCommandWithNonAllowedDuplicateAttributesParametersThrowMissingCommandParameterAttributeExceptionUnitTest
            ()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            target.GetCommandRule(typeof (TestCommands0).GetMethod("CommandWithNonAllowedDuplicateAttributesParameters"));
        }

        [Test]
        [ExpectedException(typeof (RequiredParameterFoundAfterOptionalParameterExecption))]
        public static void
            GetCommandRuleMetodHasCommandWithIncorrectlyOrderedParametersThrowMissingCommandParameterAttributeExceptionUnitTest
            ()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            target.GetCommandRule(typeof (TestCommands0).GetMethod("CommandWithIncorrectlyOrderedParameters"));
        }

        [Test]
        public static void
            GetCommandRuleMetodHasCommandWithTwoRequiredParameterAndOneOptionalParameterVerifyCommandRuleSucessUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            string expectedCommandName = "CommandWithTwoRequiredParameterAndOneOptionalParameter";
            CommandRule commandRule = target.GetCommandRule(typeof (TestCommands0).GetMethod(expectedCommandName));
            Assert.AreEqual(expectedCommandName, commandRule.Command.Name, "Command name was not correct.");
            Assert.AreEqual(2, commandRule.Command.RequiredParameters.Count,
                            "Number of required parameters was not correct.");
            Assert.AreEqual(1, commandRule.Command.OptionalParameters.Count,
                            "Number of optional parameters was not correct.");
            //Console.WriteLine(commandRule.Help());
        }

        [Test]
        public static void GetCommandRulesTargetTypeWithFiveCommands()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            List<CommandRule> actual = target.GetCommandRules(typeof (FiveTestCommands));
            Assert.AreEqual(5, actual.Count, "Count of command rules");
            Assert.IsTrue(actual[0].Command.Name == "Command1", "Name of command 1");
            Assert.IsTrue(actual[1].Command.Name == "Command2", "Name of command 2");
            Assert.IsTrue(actual[2].Command.Name == "Command3", "Name of command 3");
            Assert.IsTrue(actual[3].Command.Name == "Command4", "Name of command 4");
            Assert.IsTrue(actual[4].Command.Name == "Command5", "Name of command 5");

            Assert.IsTrue(actual[0].Command.Description == "Command 1 description", "Description of command 1");
            Assert.IsTrue(actual[1].Command.Description == "Command 2 description", "Description of command 2");
            Assert.IsTrue(actual[2].Command.Description == "Command 3 description", "Description of command 3");
            Assert.IsTrue(actual[3].Command.Description == "Command 4 description", "Description of command 4");
            Assert.IsTrue(actual[4].Command.Description == "Command 5 description", "Description of command 5");
        }

        internal class FiveTestCommands
        {
            public static void NoCommand()
            {
            }

            [Command(Description = "Command 1 description")]
            public static void Command1()
            {
            }

            [Command(Description = "Command 2 description")]
            public static void Command2()
            {
            }

            [Command(Description = "Command 3 description")]
            public static void Command3()
            {
            }

            [Command(Description = "Command 4 description")]
            public static void Command4()
            {
            }

            [Command(Description = "Command 5 description")]
            public static void Command5()
            {
            }
        }
    }
}