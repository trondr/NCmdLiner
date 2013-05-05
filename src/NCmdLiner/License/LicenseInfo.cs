// File: LicenseInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace NCmdLiner.License
{
    /// <summary>
    /// Text files ending with *License.xml and build as an embedded resource into a
    /// library or console application will be automatically be printed when the 
    /// ILicenseInfo.PrintLicenses() method is called by the library or executable.
    ///
    /// Procedure:
    ///
    /// 1. Create a License folder in your project (library or console application)
    ///
    /// 2. Add a license txt file on the format: 
    ///      {index}{name of licensed component} License.xml
    ///   where index is a sort index you can use to control the order in wich the licenses 
    ///   should be printed. Example: "0.MyLibrary License.xml"
    ///
    /// 3. In properties for the added license file, set build action to: Embedded Resource
    ///
    /// Usage in your code:
    ///
    /// ILicenseInfo licenseInfo = new LicenseInfo();
    /// Console.WriteLine(licenseInfo);
    /// </summary>
    [Serializable]
    public class LicenseInfo : ILicenseInfo
    {
        #region Implementation of ILicenseInfo

        /// <summary>
        /// Get/set product name
        /// </summary>
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }

        private string _productName;

        /// <summary>
        /// Get/set product url
        /// </summary>
        public string ProductHome
        {
            get { return _productHome; }
            set { _productHome = value; }
        }

        private string _productHome;

        /// <summary> 
        /// Get/set credit text.
        /// </summary>      
        public string CreditText
        {
            get { return _creditText; }
            set { _creditText = value; }
        }

        private string _creditText;

        /// <summary> 
        /// Get/set license.
        /// </summary>      
        public string License
        {
            get { return _license; }
            set { _license = value; }
        }

        private string _license;

        /// <summary> 
        /// Get/set license text.
        /// </summary>
        /// <value> The text. </value>
        public string LicenseText
        {
            get { return _licenseText; }
            set { _licenseText = value; }
        }

        private string _licenseText;

        #endregion

        #region Public static methods

        /// <summary>  Serialize license. </summary>
        ///
        /// <param name="fileName">      File name. </param>
        /// <param name="licenseInfo">   Information describing the license. </param>
        public static void Serialize(string fileName, LicenseInfo licenseInfo)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.UTF8))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof (LicenseInfo));
                xmlSerializer.Serialize(streamWriter, licenseInfo);
            }
        }

        /// <summary>
        /// Deserialize license
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static LicenseInfo DeSerialize(string fileName)
        {
            using (StreamReader streamReader = new StreamReader(fileName, Encoding.UTF8))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof (LicenseInfo));
                return xmlSerializer.Deserialize(streamReader) as LicenseInfo;
            }
        }

        /// <summary>
        /// Deserialize license
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal static LicenseInfo DeSerialize(Stream stream)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof (LicenseInfo));
            return xmlSerializer.Deserialize(stream) as LicenseInfo;
        }

        #endregion

        /// <summary>
        /// Returns a <see cref="T:System.String" /> that represents the current
        /// <see cref="T:System.Object" />.
        /// </summary>
        ///
        /// <returns>
        /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
        /// </returns>
        ///
        /// <seealso cref="System.Object.ToString()"/>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.ProductName + Environment.NewLine);
            sb.Append("".PadLeft(40, '-') + Environment.NewLine);
            sb.Append(this.LicenseText + Environment.NewLine);
            sb.Append("".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine);
            return sb.ToString();
        }
    }
}