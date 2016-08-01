// File: CommandRuleProviderUnitTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;
using System.ComponentModel;
using Moq;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;
using NCmdLiner;
using NCmdLiner.Tests.UnitTests.TestCommands;
using Xunit;
using Test = Xunit.FactAttribute;

namespace NCmdLiner.Tests.UnitTests
{
    public class CommandRuleProviderUnitTests
    {
        [Test]
        public static void GetCommandRuleMetodHasNoCommandAttributeThrowCommandMehtodNotStaticExceptionUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            target.GetCommandRule(typeof(TestCommands0).GetMethodEx("CommandNotStatic"));
        }

        [Test]
        public static void GetCommandRuleMetodHasNoCommandAttributeThrowMissingCommandAttributeExceptionExceptionUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            Assert.Throws<MissingCommandAttributeException>(() =>
            {
                target.GetCommandRule(typeof(TestCommands0).GetMethodEx("CommandWithoutCommandAttribute"));
            });

        }

        [Test]
        public static void GetCommandRuleMetodHasValidCommandWithOneParameterWithoutParameterAttributeThrowMissingCommandParameterAttributeExceptionUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            Assert.Throws<MissingCommandParameterAttributeException>(() =>
            {
                target.GetCommandRule(typeof(TestCommands0).GetMethodEx("CommandWithOneParameterWithoutParameterAttribute"));
            });
        }

        [Test]
        public static void GetCommandRuleMetodHasCommandWithNonAllowedDuplicateAttributesParametersThrowMissingCommandParameterAttributeExceptionUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            Assert.Throws<DuplicateCommandParameterAttributeException>(() =>
            {
                target.GetCommandRule(typeof(TestCommands0).GetMethodEx("CommandWithNonAllowedDuplicateAttributesParameters"));
            });
        }

        [Test]
        public static void
            GetCommandRuleMetodHasCommandWithIncorrectlyOrderedParametersThrowMissingCommandParameterAttributeExceptionUnitTest
            ()
        {
            CommandRuleProvider target = new CommandRuleProvider();

            Assert.Throws<RequiredParameterFoundAfterOptionalParameterExecption>(() =>
{
    target.GetCommandRule(typeof(TestCommands0).GetMethodEx("CommandWithIncorrectlyOrderedParameters"));
});
        }

        [Test]
        public static void
            GetCommandRuleMetodHasCommandWithTwoRequiredParameterAndOneOptionalParameterVerifyCommandRuleSucessUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            string expectedCommandName = "CommandWithTwoRequiredParameterAndOneOptionalParameter";
            CommandRule commandRule = target.GetCommandRule(typeof(TestCommands0).GetMethodEx(expectedCommandName));
            Assert.Equal(expectedCommandName, commandRule.Command.Name);
            Assert.Equal(2, commandRule.Command.RequiredParameters.Count);
            Assert.Equal(1, commandRule.Command.OptionalParameters.Count);
            //Console.WriteLine(commandRule.Help());
        }

        [Test]
        public static void GetCommandRulesTargetTypeWithFiveCommands()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            List<CommandRule> actual = target.GetCommandRules(typeof(FiveTestCommands));
            Assert.Equal(5, actual.Count);
            Assert.True(actual[0].Command.Name == "Command1", "Name of command 1");
            Assert.True(actual[1].Command.Name == "Command2", "Name of command 2");
            Assert.True(actual[2].Command.Name == "Command3", "Name of command 3");
            Assert.True(actual[3].Command.Name == "Command4", "Name of command 4");
            Assert.True(actual[4].Command.Name == "Command5", "Name of command 5");

            Assert.True(actual[0].Command.Description == "Command 1 description", "Description of command 1");
            Assert.True(actual[1].Command.Description == "Command 2 description", "Description of command 2");
            Assert.True(actual[2].Command.Description == "Command 3 description", "Description of command 3");
            Assert.True(actual[3].Command.Description == "Command 4 description", "Description of command 4");
            Assert.True(actual[4].Command.Description == "Command 5 description", "Description of command 5");
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