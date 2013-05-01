// File: OptionalCommandParameter.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner
{
    public class OptionalCommandParameter : CommandParameter
    {
        private OptionalCommandParameter()
        {
        }

        public OptionalCommandParameter(object defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public object DefaultValue { get; set; }

        public new string Value
        {
            get
            {
                if (_value == null)
                {
                    return DefaultValue.ToString();
                }
                return _value;
            }
            set { _value = value; }
        }

        private string _value;
    }
}