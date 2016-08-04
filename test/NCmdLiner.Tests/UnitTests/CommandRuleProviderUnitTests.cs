// File: CommandRuleProviderUnitTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Moq;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;
using NCmdLiner;
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

    [TestFixture]
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
        public static void GetCommandRuleMetodHasCommandWithTwoRequiredParameterAndOneOptionalParameterVerifyCommandRuleSucessUnitTest()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            string expectedCommandName = "CommandWithTwoRequiredParameterAndOneOptionalParameter";
            CommandRule commandRule = target.GetCommandRule(typeof(TestCommands0).GetMethodEx(expectedCommandName));
            Assert.AreEqual(expectedCommandName, commandRule.Command.Name);
            Assert.AreEqual(2, commandRule.Command.RequiredParameters.Count);
            Assert.AreEqual(1, commandRule.Command.OptionalParameters.Count);
            //Console.WriteLine(commandRule.Help());
        }

        [Test]
        public static void GetCommandRulesTargetTypeWithFiveCommands()
        {
            CommandRuleProvider target = new CommandRuleProvider();
            List<CommandRule> actual = target.GetCommandRules(typeof(FiveTestCommands));
            Assert.AreEqual(5, actual.Count);
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

        [Test]
        public static void GetCommandRuleWithBothDescriptionAndSummaryDefinedTest()
        {
            var target = new CommandRuleProvider();
            string expectedCommandName = "CommandWithBothDescriptionAndSummaryDefined";
            var actualCommandRule = target.GetCommandRule(typeof(TestCommands9).GetMethodEx(expectedCommandName));
            Assert.AreEqual(expectedCommandName, actualCommandRule.Command.Name);
            Assert.AreEqual(0, actualCommandRule.Command.RequiredParameters.Count);
            Assert.AreEqual(0, actualCommandRule.Command.OptionalParameters.Count);
            var expectedDescription = "Command with both summary and description defined";
            var epectedSummary = "Summary of command";
            Assert.AreEqual(expectedDescription, actualCommandRule.Command.Description);
            Assert.AreEqual(epectedSummary, actualCommandRule.Command.Summary);
        }

        [Test]
        public static void GetCommandRuleWithOnlyDescriptionAndNoSummaryDefinedTest()
        {
            var target = new CommandRuleProvider();
            string expectedCommandName = "CommandWithOnlyDescriptionAndNoSummaryDefined";
            var actualCommandRule = target.GetCommandRule(typeof(TestCommands9).GetMethodEx(expectedCommandName));
            Assert.AreEqual(expectedCommandName, actualCommandRule.Command.Name);
            Assert.AreEqual(0, actualCommandRule.Command.RequiredParameters.Count);
            Assert.AreEqual(0, actualCommandRule.Command.OptionalParameters.Count);
            var expectedDescription = "Command with only description defined. Summary should the be set to the same as the description.";
            var epectedSummary = "Command with only description defined. Summary should the be set to the same as the description.";
            Assert.AreEqual(expectedDescription, actualCommandRule.Command.Description);
            Assert.AreEqual(epectedSummary, actualCommandRule.Command.Summary);
        }

        [Test]
        public static void GetCommandRulesHelpProviderTest()
        {
            var target = new CommandRuleProvider();
            var actualCommandRules = target.GetCommandRules(typeof(TestCommands9));
            var messenger = new StringMessenger();
            var helpProvider = new HelpProvider(new Func<IMessenger>(() => messenger));
            helpProvider.ShowHelp(actualCommandRules,null,new ApplicationInfo());
            Assert.Contains("CommandWithBothDescriptionAndSummaryDefined              Summary of command", messenger.Message.ToString());
            Assert.Contains("CommandWithOnlyDescriptionAndNoSummaryDefined            Command with only", messenger.Message.ToString());
            Assert.Contains("CommandWithTwoRequiredParameterAndOneOptionalParameter   Summary of", messenger.Message.ToString());

            Assert.Contains("CommandWithBothDescriptionAndSummaryDefined              Command with both", messenger.Message.ToString());
            Assert.Contains("CommandWithOnlyDescriptionAndNoSummaryDefined            Command with only", messenger.Message.ToString());
            Assert.Contains("CommandWithTwoRequiredParameterAndOneOptionalParameter   Command with two", messenger.Message.ToString());
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