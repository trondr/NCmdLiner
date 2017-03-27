// File: OptionalCommandParameter.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Linq;

namespace NCmdLiner
{
    public class OptionalCommandParameter : CommandParameter
    {
        private OptionalCommandParameter()
        {
        }

        public OptionalCommandParameter(object defaultValue)
        {
            if(defaultValue == null) throw new ArgumentNullException("defaultValue","Default value is null");
            DefaultValue = defaultValue;
        }

        public object DefaultValue { get; set; }

        public new string Value
        {
            get
            {
                if (_value == null)
                {
                    if (DefaultValue is Array)
                    {
                        var array = (Array)DefaultValue;
                        var stringArray = array.OfType<object>().Select(o => o.ToString()).ToArray();
                        var defaultValue = "['" + string.Join("';'", stringArray) + "']";
                        return defaultValue;
                    }
                    return DefaultValue.ToString();
                }
                return _value;
            }
            set { _value = value; }
        }

        private string _value;
    }
}