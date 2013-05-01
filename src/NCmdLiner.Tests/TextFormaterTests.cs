// File: TextFormaterTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;

namespace NCmdLiner.Tests
{
    [TestFixture, Category(TestCategory.UnitTests)]
    public class TextFormaterTests
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
        public void JustifyText1Success()
        {
            TextFormater target = new TextFormater();
            string actual = target.Justify("This is a test of a line to be fitted to a 80 character line.", 80);
            const string expected = "This  is  a  test  of  a   line   to    be   fitted  to  a  80  character  line.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextLessThanHalfOfWidthSuccess()
        {
            TextFormater target = new TextFormater();
            string actual = target.Justify("This line will not be justified.", 80);
            const string expected = "This line will not be justified.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextMoreThanHalfOfWidthSuccess()
        {
            TextFormater target = new TextFormater();
            string actual = target.Justify("This line will be justified because it is 45.", 80);
            const string expected = "This   line    will     be         justified          because     it    is   45.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextOneLongWordSuccess()
        {
            TextFormater target = new TextFormater();
            string actual = target.Justify("Thislinewillnotbejustifiedbecauseitisonlyoneword.", 80);
            const string expected = "Thislinewillnotbejustifiedbecauseitisonlyoneword.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void JustifyTextOneShortWordSuccess()
        {
            TextFormater target = new TextFormater();
            string actual = target.Justify("Thislinewillnotbejustified.", 80);
            const string expected = "Thislinewillnotbejustified.";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines1ShortWord80Width1LineSuccess()
        {
            TextFormater target = new TextFormater();
            List<string> actual = target.BreakIntoLines("This", 80);
            List<string> expected = new List<string>() {"This"};
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines1LongWord80Width1LineSuccess()
        {
            TextFormater target = new TextFormater();
            List<string> actual =
                target.BreakIntoLines(
                    "Thisisaveryveryveryveryverylonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglongword",
                    80);
            List<string> expected = new List<string>()
                {
                    "Thisisaveryveryveryveryverylonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglonglongword"
                };
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines2Word80Width1LineSuccess()
        {
            TextFormater target = new TextFormater();
            List<string> actual = target.BreakIntoLines("This is", 80);
            List<string> expected = new List<string>() {"This is"};
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void BreakIntoLines10Words80Width3LinesSuccess()
        {
            TextFormater target = new TextFormater();
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
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Justify2Words19to19_Success()
        {
            TextFormater target = new TextFormater();
            string actual = target.Justify("[Required] Required", 29);
            const string expected = "[Required] Required";
            Assert.AreEqual(expected, actual);
        }
    }
}