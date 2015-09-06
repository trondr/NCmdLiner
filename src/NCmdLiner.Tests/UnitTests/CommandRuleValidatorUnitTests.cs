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
using TinyIoC;

namespace NCmdLiner.Tests.UnitTests
{
    [TestFixture, Category(TestCategory.UnitTests)]
    public class CommandRuleValidatorUnitTests
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
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(null, new CommandRule());
            }
        }

        [Test]
        [ExpectedException(typeof (NullReferenceException))]
        public static void ValidateCommandNotInitializedThrowNullReferenceExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[] { "SomeCommand", "/SomeRequiredParameter=\"SomeRequiredValue\"" }, new CommandRule());
            }            
        }

        [Test]
        [ExpectedException(typeof (MissingCommandException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsIsEmptyListThrowMissingCommandExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[] {}, _commandRule);
            }
        }

        [Test]
        [ExpectedException(typeof (InvalidCommandException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasUnknownCommandThrowInvalidCommandExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[] { "SomeUnknownCommand", "/SomeUnknownRequiredParameter=\"SomeRequiredValue\"" }, _commandRule);
            }
        }

        [Test]
        [ExpectedException(typeof (InvalidCommandParameterFormatException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndInvalidFormatedParameterThrowInvalidCommandParameterFormatExceptionTest
            ()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[] { "SomeValidCommand", "/SomeUnknownRequiredParameter" }, _commandRule);
            }
        }

        [Test]
        [ExpectedException(typeof (InvalidCommandParameterException))]
        public static void ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndInvalidParameterThrowInvalidCommandParameterExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[] { "SomeValidCommand", "/SomeUnknownRequiredParameter=\"SomeUnknownValue\"" }, _commandRule);
            }
        }

        [Test]
        [ExpectedException(typeof (DuplicateCommandParameterException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndDuplicateParameterThrowDuplicateCommandParameterExceptionTest
            ()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[]{ "SomeValidCommand", "/SomeUnknownRequiredParameter=\"SomeUnknownValue\"","/SomeUnknownRequiredParameter=\"SomeUnknownValue\"" }, _commandRule);
            }
        }

        [Test]
        [ExpectedException(typeof (MissingCommandParameterException))]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndMissingParameterThrowMissingCommandParameterExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[] {"SomeValidCommand"}, _commandRule);
            }
        }

        [Test]
        public static void ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndAllRequiredParametersAndNoOptionalParametersSuccessTest()
        {
            Assert.IsTrue(_commandRule.Command.RequiredParameters.Count == 2, "Number of required parameters");
            Assert.IsTrue(_commandRule.Command.OptionalParameters.Count == 1, "Number of optional parameters");
            Assert.IsNull(_commandRule.Command.RequiredParameters[0].Value);
            Assert.IsNull(_commandRule.Command.RequiredParameters[1].Value);
            Assert.IsNotNull(_commandRule.Command.OptionalParameters[0].Value);

            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                target.Validate(new string[] { "SomeValidCommand", "/InputFile=\"c:\\temp\\input.txt\"", "/OutputFile=\"c:\\temp\\output.txt\"" }, _commandRule);
            }
            
            Assert.IsTrue(_commandRule.Command.RequiredParameters.Count == 2, "Number of required parameters");
            Assert.IsTrue(_commandRule.Command.OptionalParameters.Count == 1, "Number of optional parameters");
            Assert.IsNotNull(_commandRule.Command.RequiredParameters[0].Value);
            Assert.IsNotNull(_commandRule.Command.RequiredParameters[1].Value);
            Assert.IsNotNull(_commandRule.Command.OptionalParameters[0].Value);
        }

        [Test]
        public static void HelpTest()
        {
            CommandRule target = _commandRule;
            //target.Validate(new string[] { "SomeValidCommand", "/InputFile=\"c:\\temp\\input.txt\"", "/OutputFile=\"c:\\temp\\output.txt\"" });
            //Console.WriteLine(target.Help());
        }

        internal class TestBootStrapper : IDisposable
        {
            private TinyIoCContainer _container;

            public TestBootStrapper()
            {

            }

            public TinyIoCContainer Container
            {
                get
                {
                    if (_container == null)
                    {
                        _container = new TinyIoCContainer();
                        _container.AutoRegister(new[] { _container.GetType().Assembly });
                    }
                    return _container;
                }
            }

            ~TestBootStrapper()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (_container != null)
                    {
                        _container.Dispose();
                        _container = null;
                    }
                }
            }
        }
    }
}