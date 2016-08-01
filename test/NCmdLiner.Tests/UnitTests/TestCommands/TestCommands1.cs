// File: TestCommands1.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using NCmdLiner.Tests.Common;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;

namespace NCmdLiner.Tests.UnitTests.TestCommands
{
    public class TestCommands1
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandWithNoParameters description")]
        public static void CommandWithNoParametersThrowingException()
        {
            string msg = string.Format("Running CommandWithNoParametersThrowingException");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
            throw new NCmdLinerException("Concoler test exception message");
        }

        [Command(Description = "CommandWithNoParameters description")]
        public static void CommandWithNoParameters()
        {
            string msg = string.Format("Running CommandWithNoParameters");
            Console.WriteLine(msg);
            TestLogger.Write(msg);
        }

        [Command(Description = "CommandWithRequiredStringParameter description")]
        public static void CommandWithRequiredStringParameter(
            [RequiredCommandParameter(Description = "Required parameter1 description",
                ExampleValue = "parameter 1 example value")] string parameter1)
        {
            string msg = string.Format("Running CommandWithRequiredStringParameter(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
        }

        [Command(Description = "CommandWithOptionalStringParameter description")]
        public static void CommandWithOptionalStringParameter(
            [OptionalCommandParameter(Description = "Optional parameter1 description",
                ExampleValue = "parameter 1 example value", DefaultValue = "parameter1 default value")] string
                parameter1)
        {
            string msg = string.Format("Running CommandWithOptionalStringParameter(\"{0}\")", parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
        }

        [Command(Description = "CommandWithOneRequiredAndOptionalStringParameter description")]
        public static void CommandWithOneRequiredAndOptionalStringParameter(
            [RequiredCommandParameter(Description = "Required parameter 1 description",
                ExampleValue = "parameter 1 example value")] string parameter1,
            [OptionalCommandParameter(Description = "Optional parameter 2 description",
                DefaultValue = "parameter 2 default value", ExampleValue = "parameter 2 example value")] string
                parameter2)
        {
            string msg = string.Format("Running CommandWithOneRequiredAndOptionalStringParameter(\"{0}\",\"{1}\")",
                                       parameter1, parameter2);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
        }
    }
}