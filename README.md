# NCmdLiner

NCmdLiner is a self documenting command line parser for .NET

Commands and parameters are specified as attributes on methods of a static class.

Parameters can be required or optional.

Parameters can be of primitiv types such as: string, integer, decimal, float, double, boolean and array of all these.

The automatic documentation outputs ready-to-use examples of each command and its parameters.

## Credits

NCmdLiner is derived from the NConsoler project http://nconsoler.csharpus.com

## Supported runtimes

* Mono 4.0
* .NET 2.0
* .NET 3.5
* .NET 3.5 Client
* .NET 4.0
* .NET 4.0 Client
* .NET 4.0.3
* .NET 4.0.3 Client
* .NET 4.5

## Installation

NCmdLiner will soon be available via Nuget Package Manager.

## Example

```csharp
using System;
using NCmdLiner;
using NCmdLiner.Attributes;

namespace NCmdLiner.Example
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                CmdLinery.Run(typeof(ExampleCommands), args);
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
                ExampleValue = new[] { "string1", "string2", "string3" },
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
                ExampleValue = new[] { 1.5678, 23.4253360, 126.105 },
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
```

## Example help output

```
NCmdLiner Example 1.0.0.0 for .NET - Example of how to use NCmdLiner
Copyright © examplecompany 2013
Authors: example@example.com, example2@example.com
Usage: NCmdLiner.Example.exe <command> [parameters]

Commands:
---------
Help                     Display this help text
License                  Display license
Credits                  Display credits
ExampleCommand1          ExampleCommand1 will only echo value of the input
                         parameteres.
ExampleCommand2          ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameteres.
ExampleCommand3          ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameteres.

Commands and parameters:
------------------------
ExampleCommand1          ExampleCommand1 will only echo value of the input
                         parameteres.
   /parameter1           [Required] parameter1 is a required string paramter
                         and must be specified. Alternative parameter name:
                         /p1
   /parameter2           [Required] parameter2 is required string paramter
                         and must be specified. Alternative parameter name:
                         /p2
   /parameter3           [Required] Parameter3 is required integer paramter
                         and must be specified. Alternative parameter name:
                         /p3
   /parameter4           [Optional] Parameter4 is an optional string paramter
                         and will have the default value set if not
                         specified. Alternative parameter name: /p4. Default
                         value: Some default value for parameter4
   /parameter5           [Optional] Parameter5 is an optional boolean
                         parameter and will have the default value set if not
                         specified. Alternative parameter name: /p5. Default
                         value:

   Example: NCmdLiner.Example.exe ExampleCommand1 /parameter1="Some example parameter1 value" /parameter2="Some example parameter2 value" /parameter3="" /parameter4="Some example value for parameter4" /parameter5="" 
   Example (alternative): NCmdLiner.Example.exe ExampleCommand1 /p1="Some example parameter1 value" /p2="Some example parameter2 value" /p3="" /p4="Some example value for parameter4" /p5="" 


ExampleCommand2          ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameteres.
   /parameter1           [Required] parameter1 is a required string paramter
                         and must be specified. Alternative parameter name:
                         /p1
   /parameter2           [Required] parameter2 is required string paramter
                         and must be specified. Alternative parameter name:
                         /p2
   /parameter3           [Required] Parameter3 is required integer paramter
                         and must be specified. Alternative parameter name:
                         /p3
   /parameter4           [Optional] Parameter4 is an optional string paramter
                         and will have the default value set if not
                         specified. Alternative parameter name: /p4. Default
                         value: Some default value for parameter4
   /parameter5           [Optional] Parameter5 is an optional boolean
                         parameter and will have the default value set if not
                         specified. Alternative parameter name: /p5. Default
                         value:

   Example: NCmdLiner.Example.exe ExampleCommand2 /parameter1="Some example parameter1 value" /parameter2="Some example parameter2 value" /parameter3="" /parameter4="Some example value for parameter4" /parameter5="" 
   Example (alternative): NCmdLiner.Example.exe ExampleCommand2 /p1="Some example parameter1 value" /p2="Some example parameter2 value" /p3="" /p4="Some example value for parameter4" /p5="" 


ExampleCommand3          ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameteres.
   /parameter1           [Required] parameter1 is a required string paramter
                         and must be specified. Alternative parameter name:
                         /p1
   /stringArrayParameter2[Required] stringArrayParameter2 is a required
                         string array and must be specified. Alternative
                         parameter name: /ap2
   /booleanArrayParameter3[Required] booleanArrayParameter3 is a required
                         bolean array and must be specified. Alternative
                         parameter name: /ap3
   /integerArrayParameter4[Optional] integerArrayParameter4 is a optional
                         integer array. Alternative parameter name: /ap4.
                         Default value: [1;2;3]
   /doubleArrayParameter5[Optional] doubleArrayParameter5 is a optional
                         integer array. Alternative parameter name: /ap5.
                         Default value: [1,1;2,2;3,3]

   Example: NCmdLiner.Example.exe ExampleCommand3 /parameter1="Some example parameter1 value" /stringArrayParameter2="['string1';'string2';'string3']" /booleanArrayParameter3="[True;False;True]" /integerArrayParameter4="[11;21;32]" /doubleArrayParameter5="[1,5678;23,425336;126,105]" 
   Example (alternative): NCmdLiner.Example.exe ExampleCommand3 /p1="Some example parameter1 value" /ap2="['string1';'string2';'string3']" /ap3="[True;False;True]" /ap4="[11;21;32]" /ap5="[1,5678;23,425336;126,105]"
```




