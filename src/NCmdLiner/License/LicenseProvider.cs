// File: LicenseProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NCmdLiner.Resources;

namespace NCmdLiner.License
{
    /// <summary>
    /// The licesense provider extracts license information from embedded resources in the specified assembly and all referenced assemblies.
    /// </summary>
    public class LicenseProvider : ILicenseProvider
    {
        #region Implementation of ILicenseProvider

        /// <summary>
        /// Get licenses from resources embedded in the specified assembly. If assembly is null, licenses are loaded from the assembly containing the LicenseProvider implementation.
        /// </summary>
        /// <returns></returns>
        public List<ILicenseInfo> GetLicenses(Assembly assembly = null)
        {
            assembly = GetAssembly(assembly);
            List<Assembly> assemblies = new List<Assembly>();
            assemblies.Add(assembly);
            AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {
                Assembly referencedAssembly = Assembly.Load(assemblyName);
                assemblies.Add(referencedAssembly);
            }
            List<ILicenseInfo> licenses = new List<ILicenseInfo>();
            foreach (Assembly a in assemblies)
            {
                string[] resourceNames = a.GetManifestResourceNames();
                IEmbeddedResource embeddedResource = new EmbeddedResource();
                List<string> resourceNameList = new List<string>(resourceNames.Length);
                resourceNameList.AddRange(resourceNames);
                resourceNameList.Sort();
                foreach (string resourceName in resourceNameList)
                {
                    if (resourceName.ToLower().EndsWith("license.xml"))
                    {
                        using (Stream resourceStream = embeddedResource.ExtractToStream(resourceName, a))
                        {
                            try
                            {
                                ILicenseInfo licenseInfo = LicenseInfo.DeSerialize(resourceStream);
                                licenses.Add(licenseInfo);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Failed to deserialize embedded resource '{0}' in '{1}'. {2}",
                                                  resourceName, a.FullName, ex.Message);
                            }
                        }
                    }
                }
            }
            return licenses;
        }

        #endregion

        #region Private methods

        /// <summary>  Gets an assembly. </summary>
        ///
        /// <param name="assembly">   The assembly. </param>
        ///
        /// <returns>  The assembly. </returns>
        private Assembly GetAssembly(Assembly assembly)
        {
            return assembly ?? (Assembly.GetCallingAssembly());
        }

        #endregion
    }
}