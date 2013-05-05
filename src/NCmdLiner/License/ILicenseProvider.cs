// File: ILicenseProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Collections.Generic;
using System.Reflection;

namespace NCmdLiner.License
{
    /// <summary>
    /// License provider class
    /// </summary>
    public interface ILicenseProvider
    {
        /// <summary>
        /// Get licenses
        /// </summary>
        /// <returns></returns>
        List<ILicenseInfo> GetLicenses(Assembly assembly = null);
    }
}