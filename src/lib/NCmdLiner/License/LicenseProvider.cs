// File: LicenseProvider.cs
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
    /// The license provider extracts license information from embedded resources in the specified assembly and all referenced assemblies.
    /// </summary>
    public class LicenseProvider : ILicenseProvider
    {
        /// <summary>
        /// Get licenses from resources embedded in the specified assembly. If assembly is null, licenses are loaded from the assembly containing the InfoProvider implementation.
        /// </summary>
        /// <returns></returns>
        public List<LicenseInfo> GetLicenses(Assembly assembly = null)
        {
            return InfoProvider.GetEmbeddedInfo<LicenseInfo>(InfoProvider.EmbeddedResourceFileNamePostFix.LicenseFilePostfix, assembly);
        }
    }
}