// File: InvalidConversionException.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.Exceptions
{
    /// <summary>   Exception for signalling invalid conversion errors. </summary>
    ///
    /// <remarks>   trond, 2013-05-01. </remarks>
    public sealed class InvalidConversionException : NCmdLinerException
    {
        public InvalidConversionException(string message, params object[] arguments) : base(message, arguments)
        {
        }
    }
}