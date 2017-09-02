// File: LicenseInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Text;

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
#if !NETSTANDARD1_6 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0
    [Serializable]
#endif
    public class LicenseInfo : ILicenseInfo
    {
        public string ProductName
        {
            get { return _productName; }
            set { _productName = value; }
        }
        private string _productName;

        public string ProductHome
        {
            get { return _productHome; }
            set { _productHome = value; }
        }
        private string _productHome;

        public string CreditText
        {
            get { return _creditText; }
            set { _creditText = value; }
        }
        private string _creditText;

        public string License
        {
            get { return _license; }
            set { _license = value; }
        }
        private string _license;

        public string LicenseText
        {
            get { return _licenseText; }
            set { _licenseText = value; }
        }
        private string _licenseText;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(ProductName + Environment.NewLine);
            sb.Append("".PadLeft(40, '-') + Environment.NewLine);
            sb.Append(LicenseText + Environment.NewLine);
            sb.Append("".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine);
            return sb.ToString();
        }
    }
}