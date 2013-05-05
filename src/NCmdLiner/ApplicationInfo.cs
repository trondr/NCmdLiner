// File: ApplicationInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner
{
    public class ApplicationInfo : IApplicationInfo
    {
        /// <summary>   Gets or sets the name. </summary>
        ///
        /// <value> The name. </value>
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    _name = ApplicationInfoHelper.ApplicationName;
                }
                return _name;
            }
            set { _name = value; }
        }

        private string _name;

        /// <summary>   Gets or sets the version. </summary>
        ///
        /// <value> The version. </value>
        public string Version
        {
            get
            {
                if (string.IsNullOrEmpty(_version))
                {
                    _version = ApplicationInfoHelper.ApplicationVersion;
                }
                return _version;
            }
            set { _version = value; }
        }

        private string _version;

        /// <summary>   Gets or sets the copyright. </summary>
        ///
        /// <value> The copyright. </value>
        public string Copyright
        {
            get
            {
                if (string.IsNullOrEmpty(_copyright))
                {
                    _copyright = ApplicationInfoHelper.ApplicationCopyright;
                }
                return _copyright;
            }
            set { _copyright = value; }
        }

        private string _copyright;

        public string Authors { get; set; }

        /// <summary>   Gets or sets the description. </summary>
        ///
        /// <value> The description. </value>
        public string Description
        {
            get
            {
                if (string.IsNullOrEmpty(_description))
                {
                    _description = ApplicationInfoHelper.ApplicationDescription;
                }
                return _description;
            }
            set { _description = value; }
        }

        private string _description;
        private string _exeFileName;

        /// <summary>   Gets or sets the filename of the executable file. </summary>
        ///
        /// <value> The filename of the executable file. </value>
        public string ExeFileName
        {
            get
            {
                if (string.IsNullOrEmpty(_exeFileName))
                {
                    _exeFileName = ApplicationInfoHelper.ExeFileName;
                }
                return _exeFileName;
            }
            set { _exeFileName = value; }
        }
    }
}