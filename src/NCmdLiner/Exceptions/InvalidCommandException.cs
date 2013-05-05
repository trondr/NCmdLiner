// File: InvalidCommandException.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright � <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.Exceptions
{
    public class InvalidCommandException : NCmdLinerException
    {
        public InvalidCommandException(string message) : base(message)
        {
        }
    }
}