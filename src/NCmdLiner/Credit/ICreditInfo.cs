// File: ICreditInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.Credit
{
    /// <summary>
    /// Interface defining method for printing license
    /// </summary>
    public interface ICreditInfo
    {
        /// <summary>
        /// Get/set product name
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        /// Get/set product url
        /// </summary>
        string ProductHome { get; set; }

        /// <summary>  Gets or sets the credit text. </summary>
        ///
        /// <value> The credit text. </value>
        string CreditText { get; set; }
    }
}