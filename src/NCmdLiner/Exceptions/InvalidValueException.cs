// File: InvalidValueException.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.Exceptions
{
    /// <summary>   Exception for signalling invalid value errors. </summary>
    ///
    /// <remarks>   trondr, 2013-05-01. </remarks>
    public sealed class InvalidValueException : NCmdLinerException
    {
        public InvalidValueException(string message, params object[] arguments) : base(message, arguments)
        {
        }
    }
}