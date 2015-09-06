// File: CommandRule.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Reflection;

namespace NCmdLiner
{
    public class CommandRule
    {
        public Command Command { get; set; }

        public MethodInfo Method { get; set; }

        public bool IsValid { get; set; }

        public object Instance { get; set; }

    }
}