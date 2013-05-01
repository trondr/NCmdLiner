// File: EmbeddedResource.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.IO;
using System.Reflection;

namespace NCmdLiner.Resources
{
    /// <summary>  Embedded resource.  </summary>
    ///
    /// <seealso cref="IEmbeddedResource"/>
    public class EmbeddedResource : IEmbeddedResource
    {
        #region Implementation of IEmbededResource

        /// <summary>
        /// Extract embeded resource
        /// </summary>
        /// <param name="name">Name of embedded resource to extracted from assembly. Example: "My.Name.Space.MyPicture.bmp"</param>
        /// <param name="assembly">Assembly where resource is embedded</param>      
        public Stream ExtractToStream(string name, Assembly assembly)
        {
            if (name == null) throw new ArgumentNullException("name");
            if (assembly == null) throw new ArgumentNullException("assembly");
            Stream resourceStream = assembly.GetManifestResourceStream(name);
            if (resourceStream == null)
            {
                string msg = string.Format("Failed to extract embedded resource '{0}' from assembly '{1}'.", name,
                                           assembly.FullName);
#if DEBUG
                Console.WriteLine(msg);
                string[] resourceNames = assembly.GetManifestResourceNames();
                Console.WriteLine("Assembly '{0}' has {1} embedded resources.", assembly.GetName(), resourceNames.Length);

                foreach (string manifestResourceName in resourceNames)
                {
                    Console.WriteLine("Embedded resource: {0}", manifestResourceName);
                }
#endif
                throw new Exception(msg);
            }
            return resourceStream;
        }

        /// <summary>  Extracts embedded resource to file. </summary>
        ///
        /// <param name="name">       Name of embedded resource to extracted from assembly. Example:
        ///                           "My.Name.Space.MyPicture.bmp". </param>
        /// <param name="assembly">   Assembly where resource is embedded. </param>
        /// <param name="fileName">   Filename of the file. </param>
        public void ExtractToFile(string name, Assembly assembly, string fileName)
        {
            using (
                FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                                       FileShare.None))
            {
                using (Stream stream = ExtractToStream(name, assembly))
                {
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Failed to extract embeded resource to file.", fileName);
            }
        }

        #endregion
    }
}