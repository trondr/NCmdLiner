// File: CommandsAttibute.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;

namespace NCmdLiner.Attributes
{
    /// <summary>Classes with static methods decorated with the CommandAttribute can optionally be decorated with the CommandsAttribute. This can be used for detecting all classes having commands using reflection.</summary>    
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class CommandsAttribute : Attribute
    {
        
    }
}