// File: CommandAttibute.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
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
        private string _summary;

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

        /// <summary>
        /// Summary is used to give summary description of a command. For use in the help message about the command.
        /// </summary>
        public string Summary
        {
            get
            {
                if (string.IsNullOrEmpty(_summary))
                {
                    _summary = Description;
                }
                return _summary;
            }
            set { _summary = value; }
        }
    }
}