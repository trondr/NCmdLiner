// File: OptionalCommandParameterAttribute.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.Attributes
{
    /// <summary>
    /// Marks an Action method parameter as optional
    /// </summary>
    public sealed class OptionalCommandParameterAttribute : CommandParameterAttribute
    {
        /// <summary>
        /// Get default value
        /// </summary>
        public object DefaultValue { get; set; }


        public OptionalCommandParameterAttribute()
        {
        }

        /// <param name="defaultValue">Default value if client doesn't pass this value</param>      
        public OptionalCommandParameterAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
    }
}