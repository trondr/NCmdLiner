// File: OptionalCommandParameter.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;

namespace NCmdLiner
{
    public class OptionalCommandParameter : CommandParameter
    {
        // ReSharper disable once UnusedMember.Local
        private OptionalCommandParameter()
        {
        }

        public OptionalCommandParameter(object defaultValue)
        {
            if(defaultValue == null) throw new ArgumentNullException(nameof(defaultValue),"Default value is null");
            DefaultValue = defaultValue;
        }

        public object DefaultValue { get; set; }

        public new string Value
        {
            get
            {
                if (_value == null)
                {
                    var valueConverter = new ValueConverter();
                    _value = valueConverter.ObjectValue2String(DefaultValue);                    
                }
                return _value;
            }
            set { _value = value; }
        }

        private string _value;
    }
}