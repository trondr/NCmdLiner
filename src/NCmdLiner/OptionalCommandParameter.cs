// File: OptionalCommandParameter.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
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