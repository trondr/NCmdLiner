// File: ParseArrayTests.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using Xunit;
using Test = Xunit.FactAttribute;

namespace NCmdLiner.Tests.UnitTests
{
    public class ValueConverterTests
    {
        [Test]
        [Trait("Category", "UnitTests")]
        public void StringObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String("1043");
            var expected = "1043";
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]

        public void StringArrayObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(new[] { "string2", "string45", "string26" });
            var expected = "['string2';'string45';'string26']";
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]
        public void IntegerObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(1043);
            var expected = 1043.ToString();
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]
        public void IntegerArrayObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(new[] { 2, 45, 26 });
            var expected = "[2;45;26]";
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]
        public void BooleanObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(true);
            var expected = true.ToString();
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]
        public void BoleanArrayObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(new[] { true, false, true });
            var expected = "[True;False;True]";
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]
        public void FloatObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(1.3456f);
            var expected = 1.3456f.ToString();
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]

        public void FloatArrayObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(new[] { 1.3456f, 2.3456f, 3.3456f });
            var expected = "[" + 1.3456f.ToString() + ";" + 2.3456f.ToString() + ";" + 3.3456f.ToString() + "]";
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]

        public void DoubleObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(1.3456d);
            var expected = 1.3456d.ToString();
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]

        public void DoubleArrayObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(new[] { 1.3456d, 2.3456d, 3.3456d });
            var expected = "[" + 1.3456d.ToString() + ";" + 2.3456d.ToString() + ";" + 3.3456d.ToString() + "]";
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]

        public void EnumObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(TestEnum.Value2);
            var expected = TestEnum.Value2.ToString();
            Assert.Equal(expected, actual);
        }

        [Test]
        [Trait("Category", "UnitTests")]

        public void EnumArrayObjectValue2String()
        {
            IValueConverter target = new ValueConverter();
            var actual = target.ObjectValue2String(new[] { TestEnum.Value2, TestEnum.Value3, TestEnum.Value1 });
            var expected = "[" + TestEnum.Value2.ToString() + ";" + TestEnum.Value3.ToString() + ";" + TestEnum.Value1.ToString() + "]";
            Assert.Equal(expected, actual);
        }
        private enum TestEnum
        {
            Value1,
            Value2,
            Value3
        }
    }
}