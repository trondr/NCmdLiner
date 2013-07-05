// File: OptionalCommandParameterAttribute.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using NCmdLiner.Exceptions;

namespace NCmdLiner.Attributes
{
    /// <summary>
    /// Marks an Action method parameter as optional
    /// </summary>
    public sealed class OptionalCommandParameterAttribute : CommandParameterAttribute
    {
        private object _defaultValue;
        private bool _defaultValueHasBeenSet;

        /// <summary>
        /// Get default value
        /// </summary>
        public object DefaultValue
        {
            get
            {
                if (!_defaultValueHasBeenSet)
                {
                    throw new MissingDefaultValueException(string.Format("Missing default value for optional parameter with alternative name '{0}'",this.AlternativeName));
                }
                return _defaultValue;
            }
            set
            {
                _defaultValue = value;
                _defaultValueHasBeenSet = true;
            }
        }

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