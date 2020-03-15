// File: ILicenseInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.License
{
    /// <summary>
    /// Interface defining method for printing license
    /// </summary>
    public interface ILicenseInfo
    {
        /// <summary>
        /// Get/set product name
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        /// Get/set product url
        /// </summary>
        string ProductHome { get; set; }

        /// <summary>  Gets or sets the licence. </summary>
        ///
        /// <value> The license. </value>
        string License { get; set; }

        /// <summary> 
        /// Gets/set license text.
        /// </summary>
        /// <value> The text. </value>
        string LicenseText { get; set; }
    }
}