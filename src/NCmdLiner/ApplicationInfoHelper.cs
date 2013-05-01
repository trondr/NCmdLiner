// File: ApplicationInfoHelper.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.IO;
using System.Reflection;

namespace NCmdLiner
{
    public static class ApplicationInfoHelper
    {
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

        private static string _applicationName;

        /// <summary>
        /// Get application version
        /// </summary>
        public static string ApplicationVersion
        {
            get
            {
                if (string.IsNullOrEmpty(_applicationVersion))
                {
                    Assembly assembly = Assembly.GetEntryAssembly();
                    if (assembly == null)
                    {
                        assembly = Assembly.GetExecutingAssembly();
                    }
                    _applicationVersion = assembly.GetName().Version.ToString();
                }
                return _applicationVersion;
            }
        }

        private static string _applicationVersion;

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

        private static string _exeFileName;

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

        private static string _exeFilePath;
    }
}