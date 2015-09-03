using System.Collections.Generic;
using NCmdLiner.Exceptions;
using NUnit.Framework;
using Rhino.Mocks;

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
            string[] args = { "12;13;14;15", "12" };
            var target = new ArgumentsParser();
            var actual = target.GetCommandLineParameters(args);
            var expected = new[] { "12", "13", "14", "15" };
            CollectionAssert.AreEqual(expected, actual);
        }


        [Test]
        [ExpectedException(typeof(InvalidCommandParameterFormatException))]
        public void ValidCommandLineParameters()
        {
            string[] args = { "ExeFile", "Command", "/name1=vaule1", "/name2=vaule2" };
            var target = new ArgumentsParser();
            var actual = target.GetCommandLineParameters(args);
            Dictionary<string,CommandLineParameter> expected = new Dictionary<string, CommandLineParameter> { { "name1", new CommandLineParameter() { Name = "name1", Value = "value1" }}, { "name2", new CommandLineParameter() { Name = "name2", Value = "value2" } } };
            CollectionAssert.AreEquivalent(expected.Keys, actual.Keys, "Keys were not equivalent");
            CollectionAssert.AreEquivalent(expected.Values, actual.Values, "Values were not equivalent");
        }

        [Test]
        [ExpectedException(typeof(InvalidCommandParameterFormatException))]
        public void CommandLineParametersWithEqualCharcter()
        {
            string[] args = { "ExeFile", "Command", "/name1=vaule1=1", "/name2=vaule2=2" };
            var target = new ArgumentsParser();
            var actual = target.GetCommandLineParameters(args);
            Dictionary<string, CommandLineParameter> expected = new Dictionary<string, CommandLineParameter> { { "name1", new CommandLineParameter() { Name = "name1", Value = "value1=1" } }, { "name2", new CommandLineParameter() { Name = "name2", Value = "value2=2" } } };
            CollectionAssert.AreEquivalent(expected.Keys, actual.Keys, "Keys were not equivalent");
            CollectionAssert.AreEquivalent(expected.Values, actual.Values, "Values were not equivalent");
        }
    }
}