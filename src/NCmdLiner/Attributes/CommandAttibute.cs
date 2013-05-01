// File: CommandAttibute.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;

namespace NCmdLiner.Attributes
{
    /// <summary>
    /// Every action method should be marked with this attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class CommandAttribute : Attribute
    {
        /// <summary>  Default constructor. </summary>
        public CommandAttribute()
        {
            Description = String.Empty;
        }

        /// <summary>  Constructor. </summary>
        ///
        /// <param name="description">   The description. </param>
        public CommandAttribute(string description)
        {
            Description = description;
        }

        /// <summary>
        /// Description is used for help messages
        /// </summary>
        public string Description { get; set; }
    }
}