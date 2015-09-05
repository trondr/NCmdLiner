using System;
using System.Collections.Generic;
using NCmdLiner.Exceptions;
using NUnit.Framework;
using Rhino.Mocks;
using TinyIoC;

namespace NCmdLiner.Tests
{
    [TestFixture, Category(TestCategory.UnitTests)]
    public class ArgumentsParserTests
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
        [ExpectedException(typeof(InvalidCommandParameterFormatException))]
        public void InvalidCommandLineParametersThrow()
        {
            using (var testBootStrapper = new TestBootStrapper(GetType()))
            {
                string[] args = {"12;13;14;15", "12"};
                var target = new ArgumentsParser();
                var actual = target.GetCommandLineParameters(args);
                
                //Should have thrown an exception by now
            }
        }
        
        [Test]        
        public void ValidCommandLineParameters()
        {
            using (var testBootStrapper = new TestBootStrapper(GetType()))
            {
                string[] args = {"Command", "/name1=vaule1", "/name2=vaule2"};
                var target = testBootStrapper.Container.Resolve<IArgumentsParser>();
                var actual = target.GetCommandLineParameters(args);
                Dictionary<string, CommandLineParameter> expected = new Dictionary<string, CommandLineParameter>
                {
                    {"name1", new CommandLineParameter() {Name = "name1", Value = "value1"}},
                    {"name2", new CommandLineParameter() {Name = "name2", Value = "value2"}}
                };
                CollectionAssert.AreEquivalent(expected.Keys, actual.Keys, "Keys were not equivalent");
                CollectionAssert.AreEquivalent(expected.Values, actual.Values, "Values were not equivalent");

                Assert.IsTrue(expected.ContainsKey("name1"), "name1 not found");
                Assert.AreEqual("name1", expected["name1"].Name);
                Assert.AreEqual("value1", expected["name1"].Value);

                Assert.IsTrue(expected.ContainsKey("name2"), "name2 not found");
                Assert.AreEqual("name2", expected["name2"].Name);
                Assert.AreEqual("value2", expected["name2"].Value);

            }
        }

        [Test]
        public void CommandLineParametersWithMultipleEqualCharcters()
        {
            using (var testBootStrapper = new TestBootStrapper(GetType()))
            {
                string[] args = {"Command", "/name1=vaule1=1=1", "/name2=vaule2=2=2"};
                var target = testBootStrapper.Container.Resolve<IArgumentsParser>();
                var actual = target.GetCommandLineParameters(args);
                var expected = new Dictionary<string, CommandLineParameter>
                {
                    {"name1", new CommandLineParameter() {Name = "name1", Value = "value1=1=1"}},
                    {"name2", new CommandLineParameter() {Name = "name2", Value = "value2=2=2"}}
                };

                CollectionAssert.AreEquivalent(expected.Keys, actual.Keys, "Keys were not equivalent");
                CollectionAssert.AreEquivalent(expected.Values, actual.Values, "Values were not equivalent");

                Assert.IsTrue(expected.ContainsKey("name1"), "name1 not found");
                Assert.AreEqual("name1", expected["name1"].Name);
                Assert.AreEqual("value1=1=1", expected["name1"].Value);

                Assert.IsTrue(expected.ContainsKey("name2"), "name2 not found");
                Assert.AreEqual("name2", expected["name2"].Name);
                Assert.AreEqual("value2=2=2", expected["name2"].Value);
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
                        _container.AutoRegister(new[] {_container.GetType().Assembly});                    
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