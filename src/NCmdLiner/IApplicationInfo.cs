// File: IApplicationInfo.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
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