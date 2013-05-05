// File: NCmdLinerException.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;

namespace NCmdLiner.Exceptions
{
    /// <summary>  Exception for signalling NCmdLiner errors. </summary>
    ///
    /// <remarks>  Trond, 03.10.2012. </remarks>
    public class NCmdLinerException : Exception
    {
        public NCmdLinerException(string message, params object[] arguments) : base(string.Format(message, arguments))
        {
        }
    }
}