// File: CommandRuleUnitTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using NCmdLiner;
using NCmdLiner.Exceptions;
using TinyIoC;

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
    public class CommandRuleValidatorUnitTests
    {
        private static CommandRule GetTestCommandRule()
        {
            var commandRule = new CommandRule();
            commandRule.Command = new Command { Name = "SomeValidCommand", Description = "Some command description" };
            commandRule.Command.RequiredParameters.Add(new RequiredCommandParameter
            {
                Name = "InputFile",
                Description = "Input file description",
                ExampleValue = "c:\\temp\\input.txt"
            });
            commandRule.Command.RequiredParameters.Add(new RequiredCommandParameter
            {
                Name = "OutputFile",
                Description = "Output file description",
                ExampleValue = "c:\\temp\\output.txt"
            });
            commandRule.Command.OptionalParameters.Add(new OptionalCommandParameter("false")
            {
                Name = "OverwriteOutput",
                Description = "Owerwrite output file",
                ExampleValue = "true"
            });
            return commandRule;
        }

        [Test]
        public static void ValidateArgsNullThrowArgumentNullExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                Assert.Throws<ArgumentNullException>(() =>
                {
                    target.Validate(null, new CommandRule());
                });
            }
        }

        [Test]
        public static void ValidateCommandNotInitializedThrowNullReferenceExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                Assert.Throws<NullReferenceException>(() =>
                {
                    target.Validate(new string[] { "SomeCommand", "/SomeRequiredParameter=\"SomeRequiredValue\"" }, new CommandRule());
                });
            }
        }

        [Test]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsIsEmptyListThrowMissingCommandExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                var commandRule = GetTestCommandRule();
                var result = target.Validate(new string[] { }, commandRule);
                Assert.IsFalse(result.IsSuccess);
                Assert.AreEqual(typeof(MissingCommandException), result.ToException().GetType());
                Assert.IsTrue(result.ToException().Message.StartsWith("Command not specified."));

            }
        }

        [Test]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasUnknownCommandThrowInvalidCommandExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                var commandRule = GetTestCommandRule();

                var result = target.Validate(new string[] { "SomeUnknownCommand", "/SomeUnknownRequiredParameter=\"SomeRequiredValue\"" }, commandRule);
                Assert.IsFalse(result.IsSuccess);
                Assert.AreEqual(typeof(InvalidCommandException), result.ToException().GetType());
                Assert.IsTrue(result.ToException().Message.StartsWith("Invalid command: SomeUnknownCommand. Valid command is: SomeValidCommand"));

            }
        }

        [Test]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndInvalidformattedParameterThrowInvalidCommandParameterFormatExceptionTest
            ()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                var commandRule = GetTestCommandRule();

                var result = target.Validate(new string[] { "SomeValidCommand", "/SomeUnknownRequiredParameter" }, commandRule);
                Assert.IsFalse(result.IsSuccess);
                Assert.AreEqual(typeof(InvalidCommandParameterFormatException), result.ToException().GetType());
                Assert.AreEqual("Invalid command line parameter format: '/SomeUnknownRequiredParameter'. Commandline parameter must be on the format '/ParameterName=ParameterValue' or '/ParameterName=\"Parameter Value\"'", result.ToException().Message);

            }
        }

        [Test]
        public static void ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndInvalidParameterThrowInvalidCommandParameterExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                var commandRule = GetTestCommandRule();

                var result = target.Validate(new string[] { "SomeValidCommand", "/SomeUnknownRequiredParameter=\"SomeUnknownValue\"" }, commandRule);
                Assert.IsFalse(result.IsSuccess);
                Assert.AreEqual(typeof(InvalidCommandParameterException), result.ToException().GetType());
                Assert.IsTrue(result.ToException().Message.StartsWith("Invalid command line parameter"));


            }
        }

        [Test]
        public static void
            ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndDuplicateParameterThrowDuplicateCommandParameterExceptionTest
            ()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                var commandRule = GetTestCommandRule();

                var result = target.Validate(new string[] { "SomeValidCommand", "/SomeUnknownRequiredParameter=\"SomeUnknownValue\"", "/SomeUnknownRequiredParameter=\"SomeUnknownValue\"" }, commandRule);
                Assert.IsFalse(result.IsSuccess);
                Assert.AreEqual(typeof(DuplicateCommandParameterException), result.ToException().GetType());
                Assert.AreEqual("Command line parameter appeared more than once: SomeUnknownRequiredParameter", result.ToException().Message);
            }
        }

        [Test]
        public static void ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndMissingParameterThrowMissingCommandParameterExceptionTest()
        {
            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                var commandRule = GetTestCommandRule();

                var result = target.Validate(new string[] { "SomeValidCommand" }, commandRule);
                Assert.IsFalse(result.IsSuccess);
                Assert.IsTrue(result.ToException().Message.StartsWith("Required parameter is missing"));
            }
        }

        [Test]
        public static void ValidateCommandHasTwoRequiredAndOneOtionalParameterArgsHasValidCommandAndAllRequiredParametersAndNoOptionalParametersSuccessTest()
        {
            var commandRule = GetTestCommandRule();
            Assert.IsTrue(commandRule.Command.RequiredParameters.Count == 2, "Number of required parameters");
            Assert.IsTrue(commandRule.Command.OptionalParameters.Count == 1, "Number of optional parameters");
            Assert.IsNull(commandRule.Command.RequiredParameters[0].Value);
            Assert.IsNull(commandRule.Command.RequiredParameters[1].Value);
            Assert.IsNotNull(commandRule.Command.OptionalParameters[0].Value);

            using (var testBootStrapper = new TestBootStrapper())
            {
                var target = testBootStrapper.Container.Resolve<ICommandRuleValidator>();
                var result = target.Validate(new string[] { "SomeValidCommand", "/InputFile=\"c:\\temp\\input.txt\"", "/OutputFile=\"c:\\temp\\output.txt\"" }, commandRule);
                Assert.IsTrue(result.IsSuccess);
            }

            Assert.IsTrue(commandRule.Command.RequiredParameters.Count == 2, "Number of required parameters");
            Assert.IsTrue(commandRule.Command.OptionalParameters.Count == 1, "Number of optional parameters");
            Assert.IsNotNull(commandRule.Command.RequiredParameters[0].Value);
            Assert.IsNotNull(commandRule.Command.RequiredParameters[1].Value);
            Assert.IsNotNull(commandRule.Command.OptionalParameters[0].Value);
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
                        _container.AutoRegister(new[] { _container.GetType().GetAssembly() });
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