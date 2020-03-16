// File: Program.cs
// Project Name: MyUtil
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Reflection;
using System.Threading.Tasks;
using LanguageExt;
using MyUtil.Commands;
using NCmdLiner;


namespace MyUtil
{
    internal class Program
    {
        private static TryAsync<int> TryRun(string[] args) => () =>
        {
            //Optional: Override some application info to modify the header of the auto documentation
            IApplicationInfo exampleApplicationInfo = new ApplicationInfo();
            //Set the optional authors property
            exampleApplicationInfo.Authors = "example@example.com, example2@example.com";
            //Override the assembly info copyright property
            exampleApplicationInfo.Copyright = "Copyright © ExampleCompany 2013";
            
            //Parse and run the command line using specified assembly, CommandLinery will find all commands using reflection.
            //return CmdLinery.RunEx(Assembly.GetEntryAssembly(), args, exampleApplicationInfo);

            //Parse and run the command line using specified list of target types
            //return CmdLinery.RunEx(new Type[] { typeof(ExampleCommands1), typeof(ExampleCommands2) }, args, exampleApplicationInfo);

            //By default the application info will be extracted from the executing assembly meta data (assembly info)
            //and the help text will be output using the default ConsoleMessenger. If the default behaviour
            //is ok, the call to CmdLinery.Run(...) can be simplified to the following:
            return CmdLinery.RunEx(typeof(ExampleCommands1), args);
        };
        
        private static async Task<int> Main(string[] args)
        {
            var exitCode = 
                await TryRun(args).Match(
                                    i => i, 
                                    exception =>
                                        {
                                            Console.WriteLine(@"ERROR: " + exception.Message);
                                            return 1;
                                        });
            Console.WriteLine(@"Press ENTER to terminate...");
            Console.ReadLine();
            return exitCode;
        }
    }
}