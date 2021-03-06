// File: TextFormaterTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright � <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;
using System.ComponentModel;
using Moq;

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
    public class TextFormatterTests
    {

        [Test]
        public void JustifyText1Success()
        {
            TextFormatter target = new TextFormatter();
            string actual = target.Justify("This is a test of a line to be fitted to a 80 character line.", 80);
            const string expected = "This  is  a  test  of  a   line   to    be   fitted  to  a  80  character  line.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextLessThanHalfOfWidthSuccess()
        {
            TextFormatter target = new TextFormatter();
            string actual = target.Justify("This line will not be justified.", 80);
            const string expected = "This line will not be justified.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextMoreThanHalfOfWidthSuccess()
        {
            TextFormatter target = new TextFormatter();
            string actual = target.Justify("This line will be justified because it is 45.", 80);
            const string expected = "This   line    will     be         justified          because     it    is   45.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextOneLongWordSuccess()
        {
            TextFormatter target = new TextFormatter();
            string actual = target.Justify("Thislinewillnotbejustifiedbecauseitisonlyoneword.", 80);
            const string expected = "Thislinewillnotbejustifiedbecauseitisonlyoneword.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextOneShortWordSuccess()
        {
            TextFormatter target = new TextFormatter();
            string actual = target.Justify("Thislinewillnotbejustified.", 80);
            const string expected = "Thislinewillnotbejustified.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines1ShortWord80Width1LineSuccess()
        {
            TextFormatter target = new TextFormatter();
            List<string> actual = target.BreakIntoLines("This", 80);
            List<string> expected = new List<string>() { "This" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines1LongWord80Width1LineSuccess()
        {
            TextFormatter target = new TextFormatter();
            List<string> actual =
                target.BreakIntoLines(
                    "Thisisaveryveryveryveryverylonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglongword",
                    80);
            List<string> expected = new List<string>()
                {
                    "Thisisaveryveryveryveryverylonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglongword"
                };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines2Word80Width1LineSuccess()
        {
            TextFormatter target = new TextFormatter();
            List<string> actual = target.BreakIntoLines("This is", 80);
            List<string> expected = new List<string>() { "This is" };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines10Words80Width3LinesSuccess()
        {
            TextFormatter target = new TextFormatter();
            List<string> actual =
                target.BreakIntoLines(
                    "This is text should give no meaning what so ever. We are in this moment changing to second line. We should now be on the second line and if we write som more we get to the third line.",
                    80);
            List<string> expected = new List<string>()
                {
                    "This is text should give no meaning what so ever. We are in this moment",
                    "changing to second line. We should now be on the second line and if we write",
                    "som more we get to the third line."
                };
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Justify2Words19to19_Success()
        {
            TextFormatter target = new TextFormatter();
            string actual = target.Justify("[Required] Required", 29);
            const string expected = "[Required] Required";
            Assert.AreEqual(expected, actual);
        }
    }
}