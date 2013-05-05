// File: TestCommands3.cs
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
    public class TestCommands3
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandWithOneOptionalStringParameterWithoutExampleValue description")]
        public static void CommandWithOneOptionalStringParameterWithoutExampleValue(
            [OptionalCommandParameter(Description = "Optional parameter 1 description")] string parameter1
            )
        {
            string msg = string.Format("Running CommandWithOneOptionalStringParameterWithoutExampleValue(\"{0}\")",
                                       parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
        }
    }
}