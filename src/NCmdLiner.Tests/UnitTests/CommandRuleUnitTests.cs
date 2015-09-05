// File: CommandRuleUnitTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using NCmdLiner.Exceptions;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCmdLiner.Tests.UnitTests
{
    [TestFixture, Category(TestCategory.UnitTests)]
    public class CommandRuleUnitTests
    {
        private static MockRepository _mockRepository;
        private static CommandRule _commandRule;

        [SetUp]
        public static void SetUp()
        {
            _mockRepository = new MockRepository();

            _commandRule = new CommandRule();
            _commandRule.Command = new Command {Name = "SomeValidCommand", Description = "Some command description"};
            _commandRule.Command.RequiredParameters.Add(new RequiredCommandParameter
                {
                    Name = "InputFile",
                    Description = "Input file description",
                    ExampleValue = "c:\\temp\\input.txt"
                });
            _commandRule.Command.RequiredParameters.Add(new RequiredCommandParameter
                {
                    Name = "OutputFile",
                    Description = "Output file description",
                    ExampleValue = "c:\\temp\\output.txt"
                });
            _commandRule.Command.OptionalParameters.Add(new OptionalCommandParameter("false")
                {
                    Name = "OverwriteOutput",
                    Description = "Owerwrite output file",
                    ExampleValue = "true"
                });
        }

        [TearDown]
        public static void TearDown()
        {
            _mockRepository.BackToRecordAll();
            _mockRepository = null;
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public static void ValidateArgsNullThrowArgumentNullExceptionTest()
        {
            CommandRule target = new CommandRule();
            target.Validate(null);
        }

        [Test]
        [ExpectedException(typeof (NullReferenceException))]
        public static void ValidateCommandNotInitializedThrowNullReferenceExceptionTest()
        {
            CommandRule target = new CommandRule();
            target.Validate(new string[] {"SomeCommand", "/SomeRequiredParameter=\"SomeRequiredValue\""});
        }

        [Test]
        [ExpectedException(typeof (MissingCommandException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsIsEmptyListThrowMissingCommandExceptionTest()
        {
            CommandRule target = _commandRule;
            target.Validate(new string[] {});
        }

        [Test]
        [ExpectedException(typeof (InvalidCommandException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasUnknownCommandThrowInvalidCommandExceptionTest()
        {
            CommandRule target = _commandRule;
            target.Validate(new string[] {"SomeUnknownCommand", "/SomeUnknownRequiredParameter=\"SomeRequiredValue\""});
        }

        [Test]
        [ExpectedException(typeof (InvalidCommandParameterFormatException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndInvalidFormatedParameterThrowInvalidCommandParameterFormatExceptionTest
            ()
        {
            CommandRule target = _commandRule;
            target.Validate(new string[] {"SomeValidCommand", "/SomeUnknownRequiredParameter"});
        }

        [Test]
        [ExpectedException(typeof (InvalidCommandParameterException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndInvalidParameterThrowInvalidCommandParameterExceptionTest
            ()
        {
            CommandRule target = _commandRule;
            target.Validate(new string[] {"SomeValidCommand", "/SomeUnknownRequiredParameter=\"SomeUnknownValue\""});
        }

        [Test]
        [ExpectedException(typeof (DuplicateCommandParameterException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndDuplicateParameterThrowDuplicateCommandParameterExceptionTest
            ()
        {
            CommandRule target = _commandRule;
            target.Validate(new string[]
                {
                    "SomeValidCommand", "/SomeUnknownRequiredParameter=\"SomeUnknownValue\"",
                    "/SomeUnknownRequiredParameter=\"SomeUnknownValue\""
                });
        }

        [Test]
        [ExpectedException(typeof (MissingCommandParameterException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndMissingParameterThrowMissingCommandParameterExceptionTest
            ()
        {
            CommandRule target = _commandRule;
            target.Validate(new string[] {"SomeValidCommand"});
        }

        [Test]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndAllRequiredParametersAndNoOptionalParametersSuccessTest
            ()
        {
            CommandRule target = _commandRule;
            Assert.IsTrue(target.Command.RequiredParameters.Count == 2, "Number of required parameters");
            Assert.IsTrue(target.Command.OptionalParameters.Count == 1, "Number of optional parameters");
            Assert.IsNull(target.Command.RequiredParameters[0].Value);
            Assert.IsNull(target.Command.RequiredParameters[1].Value);
            Assert.IsNotNull(target.Command.OptionalParameters[0].Value);
            target.Validate(new string[]
                {"SomeValidCommand", "/InputFile=\"c:\\temp\\input.txt\"", "/OutputFile=\"c:\\temp\\output.txt\""});
            Assert.IsTrue(target.Command.RequiredParameters.Count == 2, "Number of required parameters");
            Assert.IsTrue(target.Command.OptionalParameters.Count == 1, "Number of optional parameters");
            Assert.IsNotNull(target.Command.RequiredParameters[0].Value);
            Assert.IsNotNull(target.Command.RequiredParameters[1].Value);
            Assert.IsNotNull(target.Command.OptionalParameters[0].Value);
        }

        [Test]
        public static void HelpTest()
        {
            CommandRule target = _commandRule;
            //target.Validate(new string[] { "SomeValidCommand", "/InputFile=\"c:\\temp\\input.txt\"", "/OutputFile=\"c:\\temp\\output.txt\"" });
            //Console.WriteLine(target.Help());
        }
    }
}