// File: Command.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;

namespace NCmdLiner
{
    public class Command
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<RequiredCommandParameter> RequiredParameters
        {
            get
            {
                if (_requiredParameters == null)
                {
                    _requiredParameters = new List<RequiredCommandParameter>();
                }
                return _requiredParameters;
            }
        }

        private List<RequiredCommandParameter> _requiredParameters;

        public List<OptionalCommandParameter> OptionalParameters
        {
            get
            {
                if (_optionalParameters == null)
                {
                    _optionalParameters = new List<OptionalCommandParameter>();
                }

                return _optionalParameters;
            }
        }

        private List<OptionalCommandParameter> _optionalParameters;
    }
}