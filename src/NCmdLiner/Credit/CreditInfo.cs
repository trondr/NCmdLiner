// File: CreditInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Text;

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
#if !NETSTANDARD1_6 && !NETCOREAPP1_0 && !NETCOREAPP1_1 && !NETCOREAPP2_0
    [Serializable]
#endif
    public class CreditInfo : ICreditInfo
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

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(ProductName + Environment.NewLine);
            sb.Append("".PadLeft(40, '-') + Environment.NewLine);
            sb.Append(CreditText + Environment.NewLine);
            sb.Append("".PadLeft(40, '-') + Environment.NewLine + Environment.NewLine);
            return sb.ToString();
        }
    }
}