using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NCmdLiner.Exceptions;
using Xunit;
using Test = Xunit.FactAttribute;
using TinyIoC;
using NCmdLiner;

namespace NCmdLiner.Tests.UnitTests
{
    public class ArgumentsParserTests
    {
        [Test]
        [Trait("Category", "UnitTests")]
        public void InvalidCommandLineParametersThrow()
        {
            using (var testBootStrapper = new TestBootStrapper(GetType()))
            {
                string[] args = { "12;13;14;15", "12" };
                var target = new ArgumentsParser();
                Assert.Throws<InvalidCommandParameterFormatException>(() =>
                {
                    var actual = target.GetCommandLineParameters(args);
                });
            }
        }

        [Test]
        [Trait("Category", "UnitTests")]
        public void ValidCommandLineParameters()
        {
            using (var testBootStrapper = new TestBootStrapper(GetType()))
            {
                string[] args = { "Command", "/name1=vaule1", "/name2=vaule2" };
                var target = testBootStrapper.Container.Resolve<IArgumentsParser>();
                var actual = target.GetCommandLineParameters(args);
                Dictionary<string, CommandLineParameter> expected = new Dictionary<string, CommandLineParameter>
                {
                    {"name1", new CommandLineParameter() {Name = "name1", Value = "value1"}},
                    {"name2", new CommandLineParameter() {Name = "name2", Value = "value2"}}
                };

                var sortedExpected = expected.ToImmutableSortedDictionary();
                var sortedActual = actual.ToImmutableSortedDictionary();
                Assert.Equal(sortedExpected.Keys, sortedActual.Keys);
                Assert.Equal(sortedExpected.Values, sortedActual.Values);

                Assert.True(actual.ContainsKey("name1"));
                Assert.Equal("name1", expected["name1"].Name);
                Assert.Equal("value1", expected["name1"].Value);

                Assert.True(actual.ContainsKey("name2"));
                Assert.Equal("name2", expected["name2"].Name);
                Assert.Equal("value2", expected["name2"].Value);

            }
        }

        [Test]
        [Trait("Category", "UnitTests")]
        public void CommandLineParametersWithMultipleEqualCharcters()
        {
            using (var testBootStrapper = new TestBootStrapper(GetType()))
            {
                string[] args = { "Command", "/name1=vaule1=1=1", "/name2=vaule2=2=2" };
                var target = testBootStrapper.Container.Resolve<IArgumentsParser>();
                var actual = target.GetCommandLineParameters(args);
                var expected = new Dictionary<string, CommandLineParameter>
                        {
                            {"name1", new CommandLineParameter() {Name = "name1", Value = "value1=1=1"}},
                            {"name2", new CommandLineParameter() {Name = "name2", Value = "value2=2=2"}}
                        };

                var sortedExpected = expected.ToImmutableSortedDictionary();
                var sortedActual = actual.ToImmutableSortedDictionary();

                Assert.Equal(sortedExpected.Keys, sortedActual.Keys);
                Assert.Equal(sortedExpected.Values, sortedActual.Values);

                Assert.True(expected.ContainsKey("name1"), "name1 not found");
                Assert.Equal("name1", expected["name1"].Name);
                Assert.Equal("value1=1=1", expected["name1"].Value);

                Assert.True(expected.ContainsKey("name2"), "name2 not found");
                Assert.Equal("name2", expected["name2"].Name);
                Assert.Equal("value2=2=2", expected["name2"].Value);
            }
        }

        internal class TestBootStrapper : IDisposable
        {
            private TinyIoCContainer _container;

            public TestBootStrapper(Type type)
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