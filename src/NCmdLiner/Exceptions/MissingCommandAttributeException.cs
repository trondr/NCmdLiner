// File: MissingCommandAttributeException.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.Exceptions
{
    public class MissingCommandAttributeException : NCmdLinerException
    {
        public MissingCommandAttributeException(string message) : base(message)
        {
        }
    }
}