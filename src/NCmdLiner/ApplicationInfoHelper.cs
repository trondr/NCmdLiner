// File: ApplicationInfoHelper.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.IO;
using System.Reflection;

namespace NCmdLiner
{
    public static class ApplicationInfoHelper
    {
        private static string _applicationName;
        /// <summary>
        /// Get exe file path
        /// </summary>
        public static string ApplicationName
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationName))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(ExeFilePath);
                    if (fileNameWithoutExtension != null)
                        _applicationName =
                            fileNameWithoutExtension.Replace('.', ' ').Replace("Gui", "").Replace("Console", "");
                }
                return _applicationName;
            }
        }

        private static string _applicationVersion;
        /// <summary>
        /// Get application version
        /// </summary>
        public static string ApplicationVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationVersion))
                {
                    Assembly assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
                    AssemblyInformationalVersionAttribute informationalVersionAttribute = Attribute.GetCustomAttribute(assembly, typeof(AssemblyInformationalVersionAttribute), false) as AssemblyInformationalVersionAttribute;
                    if (informationalVersionAttribute != null)
                    {
                        _applicationVersion = informationalVersionAttribute.InformationalVersion;    
                    }
                    if (string.IsNullOrEmpty(_applicationVersion))
                    {
                        _applicationVersion = assembly.GetName().Version.ToString();
                    }
                }
                return _applicationVersion;
            }
        }
        
        private static string _exeFileName;
        /// <summary>
        /// Get exe file path
        /// </summary>
        public static string ExeFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_exeFileName))
                {
                    _exeFileName = Path.GetFileName(ExeFilePath);
                }
                return _exeFileName;
            }
        }

        private static string _exeFilePath;
        
        /// <summary>
        /// Get exe file path
        /// </summary>
        public static string ExeFilePath
        {
            get
            {
                if (string.IsNullOrEmpty(_exeFilePath))
                {
                    Assembly exeAssembly = Assembly.GetEntryAssembly();
                    if (exeAssembly == null)
                    {
                        return string.Empty;
                        //throw new Exception("Failed to find path to the exe file of the current process.");
                    }
                    _exeFilePath = exeAssembly.Location;
                    if (!File.Exists(_exeFilePath))
                    {
                        throw new FileNotFoundException("Could not find exe file path: " + _exeFilePath);
                    }
                }
                return _exeFilePath;
            }
        }

        public static string ApplicationCopyright
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationCopyright))
                {
                    Assembly assembly = Assembly.GetEntryAssembly();
                    if (assembly == null)
                    {
                        assembly = Assembly.GetExecutingAssembly();                        
                    }                    
                    _applicationCopyright = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyCopyrightAttribute), false)).Copyright;
                }
                return _applicationCopyright;
            }
            
        }
        private static string _applicationCopyright;

        public static string ApplicationDescription
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationDescription))
                {
                    Assembly assembly = Assembly.GetEntryAssembly();
                    if (assembly == null)
                    {
                        assembly = Assembly.GetExecutingAssembly();
                    }
                    _applicationDescription = ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyDescriptionAttribute), false)).Description;
                }
                return _applicationDescription;
            }            
        }
        private static string _applicationDescription;
    }
}