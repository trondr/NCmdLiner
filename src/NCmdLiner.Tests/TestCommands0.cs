// File: TestCommands0.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using NCmdLiner.Attributes;

namespace NCmdLiner.Tests
{
    internal class TestCommands0
    {
        [Command(Description = "Invalid non static command")]
        public void CommandNotStatic()
        {
            throw new NotImplementedException();
        }

        public static void CommandWithoutCommandAttribute()
        {
            throw new NotImplementedException();
        }

        [Command(Description = "Command without parameter attributes description")]
        public static void CommandWithOneParameterWithoutParameterAttribute(string someParameter)
        {
            throw new NotImplementedException();
        }

        [Command(Description = "Command with incorrectly ordered parameters description")]
        public static void CommandWithNonAllowedDuplicateAttributesParameters(
            [OptionalCommandParameter(DefaultValue = "some default value", Description = "Some optional parameter")] [RequiredCommandParameter(Description = "Some required parameter description",
                ExampleValue = "Some example value")] string someOptionalParameter,
            [RequiredCommandParameter(Description = "Some required parameter description",
                ExampleValue = "Some example value")] string someRequiredParameter
            )
        {
            throw new NotImplementedException();
        }

        [Command(Description = "Command with incorrectly ordered parameters description")]
        public static void CommandWithIncorrectlyOrderedParameters(
            [OptionalCommandParameter(DefaultValue = "some default value",
                ExampleValue = "someOptionalParameter example value")] string someOptionalParameter,
            [RequiredCommandParameter(Description = "Some required parameter description",
                ExampleValue = "Some example value")] string someRequiredParameter
            )
        {
            throw new NotImplementedException();
        }

        [Command(Description = "Command with two required parameters and one optional paramter descritpion")]
        public static void CommandWithTwoRequiredParameterAndOneOptionalParameter
            (
            [RequiredCommandParameter(Description = "Some required parameter 1 description",
                ExampleValue = "Example value 1")] string someRequiredParameter1,
            [RequiredCommandParameter(Description = "Some required parameter 2 description",
                ExampleValue = "Example value 2")] string someRequiredParameter2,
            [OptionalCommandParameter(DefaultValue = false, ExampleValue = true)] bool someOptionalParameter
            )
        {
            throw new NotImplementedException();
        }
    }
}