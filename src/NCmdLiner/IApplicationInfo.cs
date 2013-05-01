// File: IApplicationInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

namespace NCmdLiner
{
    public interface IApplicationInfo
    {
        string Name { get; set; }
        string Version { get; set; }
        string Copyright { get; set; }
        string ProgrammedBy { get; set; }
        string Description { get; set; }
        string ExeFileName { get; set; }
    }
}