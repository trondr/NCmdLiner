// File: TestApplicationInfo.cs
// Project Name: NCmdLiner.Tests
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner.Tests
{
    public class TestApplicationInfo : IApplicationInfo
    {
        private string _name = "Test Application";
        private string _version = "1.0.0.0";
        private string _copyright = "Copyright \u00a9 2012";
        private string _programmedBy = "mail@somedomain.com";
        private string _description = "Test Application Description";
        private string _exeFileName = "TestApplication.exe";

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Version
        {
            get { return _version; }
            set { _version = value; }
        }

        public string Copyright
        {
            get { return _copyright; }
            set { _copyright = value; }
        }

        public string ProgrammedBy
        {
            get { return _programmedBy; }
            set { _programmedBy = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string ExeFileName
        {
            get { return _exeFileName; }
            set { _exeFileName = value; }
        }
    }
}