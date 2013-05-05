// File: CommandParameterAttribute.cs
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
    /// Should not be used directly
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public abstract class CommandParameterAttribute : Attribute
    {
        /// <summary>
        /// Description is used in help message
        /// </summary>
        public string Description { get; set; }

        /// <summary>  Gets or sets example value used in help message. </summary>      
        public object ExampleValue { get; set; }

        /// <summary>   Gets or sets the alternative parameter name. </summary>
        ///
        /// <value> The name of the alternative parameter name. </value>
        public string AlternativeName { get; set; }

        /// <summary>  Default constructor. </summary>
        protected CommandParameterAttribute()
        {
            Description = String.Empty;
        }
    }
}