// File: CreditInfo.cs
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

namespace NCmdLiner.Credit
{
    /// <summary>
    /// Text files ending with *Credit.xml and build as an embedded resource into a
    /// library or console application will be automatically be printed when the 
    /// creditInfo.ToString() method is called by the library or executable.
    ///
    /// Procedure:
    ///
    /// 1. Create a Credits folder in your project (library or console application)
    ///
    /// 2. Add a credits xml file on the format: 
    ///      {index}{name of credited component} Credit.xml
    ///   where index is a sort index you can use to control the order in wich the credits 
    ///   should be printed. Example: "0.MyLibrary Credit.xml"
    ///
    /// 3. In properties for the added credit file, set build action to: Embedded Resource
    ///
    /// Usage in your code:
    ///
    /// ICreditInfo creditInfo = new CreditInfo();
    /// Console.WriteLine(creditInfo);
    /// </summary>
    #if !NETSTANDARD1_6
    [Serializable]
    #endif
    public class CreditInfo : ICreditInfo
    {
        #region Implementation of ICreditInfo

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
            sb.Append(this.CreditText + Environment.NewLine);
            sb.Append("".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine);
            return sb.ToString();
        }
    }
}