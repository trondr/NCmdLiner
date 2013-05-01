﻿// File: IEmbeddedResource.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.IO;
using System.Reflection;

namespace NCmdLiner.Resources
{
    /// <summary>  Interface for embedded resource.  </summary>
    public interface IEmbeddedResource
    {
        /// <summary>
        /// Extract embedded resource to stream
        /// </summary>
        /// <param name="name">Name of embedded resource to extracted from assembly. Example: "My.Name.Space.MyPicture.bmp"</param>
        /// <param name="assembly">Assembly where resource is embedded</param>      
        Stream ExtractToStream(string name, Assembly assembly);

        /// <summary>  Extracts embedded resource to file. </summary>
        ///
        /// <param name="name">       Name of embedded resource to extracted from assembly. Example:
        ///                           "My.Name.Space.MyPicture.bmp". </param>
        /// <param name="assembly">   Assembly where resource is embedded. </param>
        /// <param name="fileName">   Filename of the file. </param>
        void ExtractToFile(string name, Assembly assembly, string fileName);
    }
}