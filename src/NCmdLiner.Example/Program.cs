// File: Program.cs
// Project Name: NCmdLiner.Example
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using NCmdLiner.Attributes;

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

                //Now parse and run the command line
                CmdLinery.Run(typeof(ExampleCommands), args, exampleApplicationInfo, messenger);
                
                //By default he application info will be exctracted from the executing assembly meta data (assembly info)
                //and the help text will be output using the default ConsoleMessenger. If the default behaviour
                //is ok, the call to CmdLinery.Run(...) can be simplified to the following:
                //CmdLinery.Run(typeof(ExampleCommands), args);
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

    public class ExampleCommands
    {
        [Command(Description = "ExampleCommand1 will only echo value of the input parameteres.")]
        public static void ExampleCommand1(
            [RequiredCommandParameter(
                Description = "parameter1 is a required string paramter and must be specified.",
                ExampleValue = "Some example parameter1 value",
                AlternativeName = "p1"
                )] string parameter1,
            [RequiredCommandParameter(
                Description = "parameter2 is required string paramter and must be specified.",
                ExampleValue = "Some example parameter2 value",
                AlternativeName = "p2"
                )] string parameter2,
            [RequiredCommandParameter(
                Description = "Parameter3 is required integer paramter and must be specified.",
                ExampleValue = 10,
                AlternativeName = "p3"
                )] int parameter3,
            [OptionalCommandParameter(
                Description =
                "Parameter4 is an optional string paramter and will have the default value set if not specified.",
                ExampleValue = "Some example value for parameter4",
                DefaultValue = "Some default value for parameter4",
                AlternativeName = "p4"
                )] string parameter4,
            [OptionalCommandParameter(
                Description =
                "Parameter5 is an optional boolean parameter and will have the default value set if not specified.",
                ExampleValue = true,
                DefaultValue = false,
                AlternativeName = "p5"
                )] bool parameter5
            )
        {
            Console.WriteLine("ExampleCommand1 just echoing the input parameters...");
            Console.WriteLine("parameter1={0}", parameter1);
            Console.WriteLine("parameter2={0}", parameter2);
            Console.WriteLine("parameter3={0}", parameter3);
            Console.WriteLine("parameter4={0}", parameter4);
            Console.WriteLine("parameter5={0}", parameter5);
            Console.WriteLine("Finished echoing the input parameters.");
        }

        [Command(
            Description =
                "ExampleCommand2 will do the same as ExampleCommand1 only echo value of the input parameteres.")]
        public static void ExampleCommand2(
            [RequiredCommandParameter(
                Description = "parameter1 is a required string paramter and must be specified.",
                ExampleValue = "Some example parameter1 value",
                AlternativeName = "p1"
                )] string parameter1,
            [RequiredCommandParameter(
                Description = "parameter2 is required string paramter and must be specified.",
                ExampleValue = "Some example parameter2 value",
                AlternativeName = "p2"
                )] string parameter2,
            [RequiredCommandParameter(
                Description = "Parameter3 is required integer paramter and must be specified.",
                ExampleValue = 10,
                AlternativeName = "p3"
                )] int parameter3,
            [OptionalCommandParameter(
                Description =
                "Parameter4 is an optional string paramter and will have the default value set if not specified.",
                ExampleValue = "Some example value for parameter4",
                DefaultValue = "Some default value for parameter4",
                AlternativeName = "p4"
                )] string parameter4,
            [OptionalCommandParameter(
                Description =
                "Parameter5 is an optional boolean parameter and will have the default value set if not specified.",
                ExampleValue = true,
                DefaultValue = false,
                AlternativeName = "p5"
                )] bool parameter5            
            )
        {
            Console.WriteLine("ExampleCommand2 just echoing the input parameters...");
            Console.WriteLine("parameter1={0}", parameter1);
            Console.WriteLine("parameter2={0}", parameter2);
            Console.WriteLine("parameter3={0}", parameter3);
            Console.WriteLine("parameter4={0}", parameter4);
            Console.WriteLine("parameter5={0}", parameter5);
            Console.WriteLine("Finished echoing the input parameters.");
        }


        [Command(
            Description =
                "ExampleCommand2 will do the same as ExampleCommand1 only echo value of the input parameteres.")]
        public static void ExampleCommand3(
            [RequiredCommandParameter(
                Description = "parameter1 is a required string paramter and must be specified.",
                ExampleValue = "Some example parameter1 value",
                AlternativeName = "p1"
                )] string parameter1,
            [RequiredCommandParameter(
                Description = "stringArrayParameter2 is a required string array and must be specified.",
                ExampleValue = new[]{"string1","string2","string3"},
                AlternativeName = "ap2"
                )] string[] stringArrayParameter2,
            [RequiredCommandParameter(
                Description = "booleanArrayParameter3 is a required bolean array and must be specified.",
                ExampleValue = new[] { true, false, true },
                AlternativeName = "ap3"
                )] bool[] booleanArrayParameter3,
            [OptionalCommandParameter(
                Description = "integerArrayParameter4 is a optional integer array.",
                ExampleValue = new[] { 11, 21, 32 },
                DefaultValue = new[] { 1, 2, 3 },
                AlternativeName = "ap4"
                )] int[] integerArrayParameter4,
            [OptionalCommandParameter(
                Description = "doubleArrayParameter5 is a optional integer array.",
                ExampleValue = new[] { 1.5678, 23.4253360, 126.105},
                DefaultValue = new[] { 1.1, 2.2, 3.3 },
                AlternativeName = "ap5"
                )] double[] doubleArrayParameter5
            )
        {
            Console.WriteLine("ExampleCommand3 just echoing the input parameters...");
            Console.WriteLine("parameter1={0}", parameter1);
            for (int i = 0; i < stringArrayParameter2.Length; i++)
            {
                Console.WriteLine("stringArrayParameter2[{0}]={1}", i, stringArrayParameter2[i]);
            }
            for (int i = 0; i < booleanArrayParameter3.Length; i++)
            {
                Console.WriteLine("booleanArrayParameter3[{0}]={1}", i, booleanArrayParameter3[i]);
            }
            for (int i = 0; i < integerArrayParameter4.Length; i++)
            {
                Console.WriteLine("integerArrayParameter4[{0}]={1}", i, integerArrayParameter4[i]);
            }
            for (int i = 0; i < doubleArrayParameter5.Length; i++)
            {
                Console.WriteLine("doubleArrayParameter5[{0}]={1}", i, doubleArrayParameter5[i]);
            }
            Console.WriteLine("Finished echoing the input parameters.");
        }

    }
}