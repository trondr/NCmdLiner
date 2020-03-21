// File: CreditProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Reflection;
using NCmdLiner.Resources;

namespace NCmdLiner.Credit
{
    /// <summary>
    /// The credit provider extracts credit information from embedded resources in the specified assembly and all referenced assemblies.
    /// </summary>
    public class CreditProvider : ICreditProvider
    {
        /// <summary>
        /// Get credit info from resources embedded in the specified assembly. If assembly is null, credits are loaded from the assembly containing the InfoProvider implementation.
        /// </summary>
        /// <returns></returns>
        public List<CreditInfo> GetCredits(Assembly assembly = null)
        {
            return InfoProvider.GetEmbeddedInfo<CreditInfo>(InfoProvider.EmbeddedResourceFileNamePostFix.CreditFilePostfix, assembly);
        }
    }
}