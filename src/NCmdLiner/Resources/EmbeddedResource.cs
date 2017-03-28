// File: EmbeddedResource.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
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
        /// <summary>
        /// Extract embeded resource
        /// </summary>
        /// <param name="name">Name of embedded resource to extracted from assembly. Example: "My.Name.Space.MyPicture.bmp"</param>
        /// <param name="assembly">Assembly where resource is embedded</param>      
        public Stream ExtractToStream(string name, Assembly assembly)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            var resourceStream = assembly.GetManifestResourceStream(name);
            if (resourceStream == null)
            {
                var msg = string.Format("Failed to extract embedded resource '{0}' from assembly '{1}'.", name,
                                           assembly.FullName);
#if DEBUG
                Console.WriteLine(msg);
                var resourceNames = assembly.GetManifestResourceNames();
                Console.WriteLine("Assembly '{0}' has {1} embedded resources.", assembly.GetName(), resourceNames.Length);

                foreach (var manifestResourceName in resourceNames)
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
            using (var fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                using (var stream = ExtractToStream(name, assembly))
                {
                    var buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    fileStream.Write(buffer, 0, buffer.Length);
                }
            }
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException("Failed to extract embeded resource to file.", fileName);
            }
        }
    }
}