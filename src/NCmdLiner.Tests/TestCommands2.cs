// File: TestCommands2.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
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