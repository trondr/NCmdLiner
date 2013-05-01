// File: OptionalCommandParameterAttribute.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
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