// File: ILicenseProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
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