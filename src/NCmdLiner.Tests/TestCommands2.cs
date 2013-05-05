// File: TestCommands2.cs
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
    public class TestCommands2
    {
        public static ITestLogger TestLogger;

        [Command(Description = "CommandWithOneRequiredStringParameterWithoutExampleValue description")]
        public static void CommandWithOneRequiredStringParameterWithoutExampleValue(
            [RequiredCommandParameter(Description = "Required parameter 1 description")] string parameter1
            )
        {
            string msg = string.Format("Running CommandWithOneRequiredStringParameterWithoutExampleValue(\"{0}\")",
                                       parameter1);
            Console.WriteLine(msg);
            TestLogger.Write(msg);
        }
    }
}