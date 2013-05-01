// File: InvalidConversionException.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
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