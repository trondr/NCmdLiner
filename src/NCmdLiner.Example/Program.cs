// File: Program.cs
// Project Name: NCmdLiner.Example
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Reflection;

namespace NCmdLiner.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //Optional: Override some application info to modify the header of the auto documentation
                IApplicationInfo exampleApplicationInfo = new ApplicationInfo();
                //Set the optional authors property
                exampleApplicationInfo.Authors = "example@example.com, example2@example.com";
                //Override the assembly info copyright property
                exampleApplicationInfo.Copyright = "Copyright © examplecompany 2013";

                //Optional: Extend the default console messenger to show the help text in a form as well as the default console
                IMessenger messenger = new MyDialogMessenger(new ConsoleMessenger());

                //Parse and run the command line using specified target types
                //Type[] targetTypes = new Type[] { typeof(ExampleCommands1), typeof(ExampleCommands2) };
                //CmdLinery.Run(targetTypes, args, exampleApplicationInfo, messenger);

                //Parse and run the command line using specified assembly, CommandLinery will find all commands using reflection.
                CmdLinery.Run(Assembly.GetEntryAssembly(), args, exampleApplicationInfo, messenger);
                
                //By default he application info will be exctracted from the executing assembly meta data (assembly info)
                //and the help text will be output using the default ConsoleMessenger. If the default behaviour
                //is ok, the call to CmdLinery.Run(...) can be simplified to the following:
                //CmdLinery.Run(typeof(ExampleCommands1), args);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Press ENTER to terminate...");
                Console.ReadLine();
            }
        }
    }
}