// File: ParseArrayTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using NCmdLiner.Exceptions;

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
    public class ParseArrayTest
    {
        #region Semicolon tests

        [Test]
        public void ParseArraySimpleSemicolonSeparationSuccess()
        {
            const string arrayString = "12;13;14;15";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseArraySimpleSemicolonSeparationEndingWithSemiColonSuccess()
        {
            const string arrayString = "12;13;14;15;";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15", "" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseArrayQuotedSemicolonSeparationEndingWithSemiColonSuccess()
        {
            const string arrayString = "'12';'13';'14';'15';";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15", "" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseArrayParantesis1SimpleSemicolonSeparationSuccess()
        {
            const string arrayString = "{12;13;14;15}";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseArrayParantesis2SimpleSemicolonSeparationSuccess()
        {
            const string arrayString = "[12;13;14;15]";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15" };
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Plus tests

        [Test]
        public void ParseArraySimplePlusSeparationSuccess()
        {
            const string arrayString = "12+13+14+15";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseArrayParantesis1SimplePlusSeparationSuccess()
        {
            const string arrayString = "{12+13+14+15}";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseArrayParantesis2SimplePlusSeparationSuccess()
        {
            const string arrayString = "[12+13+14+15]";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12", "13", "14", "15" };
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Non supported tests

        [Test]
        public void ParseArrayNonSupportedDelimiterReturnOneItemArray()
        {
            const string arrayString = "12-13-14-15";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new[] { "12-13-14-15" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseArrayEmptyArrayStringReturnZeroItemArray()
        {
            const string arrayString = "";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new string[] { };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Parse2ItemArrayStringReturn2ItemArray()
        {
            const string arrayString = "{'MS.*.dll';'MS.*.exe'}";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new string[] { "MS.*.dll", "MS.*.exe" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ParseItemArrayStringWithRegularExpressionReturnItemArray()
        {
            const string arrayString = @"{'^.+-133-3\d+-.+$'}";
            IArrayParser target = new ArrayParser();
            var actual = target.Parse(arrayString);
            var expected = new string[] { @"^.+-133-3\d+-.+$" };
            Assert.AreEqual(expected, actual);
        }

        #endregion

        #region Corrupt tests

        [Test]
        public void ParseArraySemicolonSeparationCorruptThrowInvalidArrayParseExeption()
        {
            const string arrayString = "'123';'21';'142;'5'";
            IArrayParser target = new ArrayParser();

            Assert.Throws<InvalidArrayParseException>(() =>
            {
                var actual = target.Parse(arrayString);
            });
        }

        #endregion
    }
}