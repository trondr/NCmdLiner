// File: AssemblyInfo_Static.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Reflection;
using System.Runtime.CompilerServices;

#if NET20
[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 2.0")]
#elif NET35
   [assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 3.5")]
#elif NET35CLIENT
   [assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 3.5 Client Profile")]
#elif NET40
[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4")]
#elif NET40CLIENT
   [assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4 Client Profile")]
#elif NET403
[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.0.3")]
#elif NET403CLIENT
   [assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.0.3 Client Profile")]
#elif NET45MONO
   [assembly: AssemblyTitle("NCmdLiner.Tests for Mono 4.5")]
#elif NET40MONO
   [assembly: AssemblyTitle("NCmdLiner.Tests for Mono 4.0")]
#elif MONO20
   [assembly: AssemblyTitle("NCmdLiner.Tests for Mono 2.0")]
#elif NET45
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.5")]
#elif NET451
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.5.1")]
#elif NET452
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.5.2")]
#elif NET46
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.6")]
#elif NET461
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.6.1")]
#elif NET462
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Framework 4.6.2")]
#elif NETSTANDARD1_6
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Standard Library 1.6")]
#elif NETCOREAPP1_0
	[assembly: AssemblyTitle("NCmdLiner.Tests for .NET Core App 1.0")]
#else
#error Unknown target framework found in AssemblyInfo.cs
#endif

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif