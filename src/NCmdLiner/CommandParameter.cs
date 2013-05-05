// File: CommandParameter.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;

namespace NCmdLiner
{
    public abstract class CommandParameter
    {
        public string Name { get; set; }

        public string AlternativeName { get; set; }

        public string Value { get; set; }

        public List<string> ValidValues
        {
            get
            {
                if (_validValues == null)
                {
                    _validValues = new List<string>();
                }
                return _validValues;
            }
            set { _validValues = value; }
        }

        private List<string> _validValues;

        public string Description { get; set; }

        public object ExampleValue { get; set; }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return ToString().Equals(obj.ToString());
        }

        public override string ToString()
        {
            return Name;
        }
    }
}