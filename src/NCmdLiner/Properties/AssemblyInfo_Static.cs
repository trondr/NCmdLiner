// File: AssemblyInfo_Static.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr
// License: New BSD License (BSD) http://www.opensource.org/licenses/BSD-3-Clause
// Credits: See the credits folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System.Reflection;
using System.Runtime.CompilerServices;

#if NET20

[assembly: AssemblyTitle("NCmdLiner for .NET Framework 2.0")]
#elif NET35
   [assembly: AssemblyTitle("NCmdLiner for .NET Framework 3.5")]
#elif NET35CLIENT
   [assembly: AssemblyTitle("NCmdLiner for .NET Framework 3.5 Client Profile")]
#elif NET40
[assembly: AssemblyTitle("NCmdLiner for .NET Framework 4")]
#elif NET40CLIENT
   [assembly: AssemblyTitle("NCmdLiner for .NET Framework 4 Client Profile")]
#elif NET403
[assembly: AssemblyTitle("NCmdLiner for .NET Framework 4.0.3")]
#elif NET403CLIENT
   [assembly: AssemblyTitle("NCmdLiner for .NET Framework 4.0.3 Client Profile")]
#elif NET40MONO
   [assembly: AssemblyTitle("NCmdLiner for .NET Framework 4 Mono")]
#elif MONO20
   [assembly: AssemblyTitle("NCmdLiner for Mono 2.0")]
#else
#error Unknown target framework found in AssemblyInfo.cs
#endif

#if DEBUG

[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly:
    InternalsVisibleTo(
        "NCmdLiner.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100772b0d0b36e4db8fb74130d2242d2b72b43d083579ac055f85a35539389656df57ea599e48e88a4d4872bb38dd12cb4ff68305197874a74e87e410043d4a7132f895548197c3fd58cffaa19845fbcfa4ca6091bd294cf74b80cb60eaec04b952814fa0d55b99dbacf54f9a7c9fd4bc37fd14e99099a912954c7ea1ab2fd671c8"
        )]