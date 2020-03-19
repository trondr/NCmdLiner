# NCmdLiner

NCmdLiner features a command line parser with auto documentation capabilities.

Commands and parameters are specified as attributes on methods of one or more classes.

Parameters can be required or optional.

Parameters can be of primitive types such as: string, integer, decimal, float, double, boolean and array of all these.

The automatic documentation outputs ready-to-use examples of each command and its parameters

## Credits

NCmdLiner is a rewrite of the NConsoler project http://nconsoler.csharpus.com

## Supported target frameworks NCmdLiner 1.0.*

* Mono 4
* .NET 2.0
* .NET 3.5
* .NET 3.5 Client
* .NET 4.0
* .NET 4.0 Client
* .NET 4.0.3
* .NET 4.0.3 Client
* .NET 4.5

## Supported target frameworks NCmdLiner 1.1.*

* Mono 4
* .NET 3.5
* .NET 3.5 Client
* .NET 4.0
* .NET 4.0 Client
* .NET 4.0.3
* .NET 4.0.3 Client
* .NET 4.5

## Supported target frameworks NCmdLiner 1.2.*

* Mono 4
* .NET 3.5
* .NET 4.5.2
* .NET 4.6.1
* .NET Core 1.0

## Supported target frameworks NCmdLiner 1.3.*

* Mono 4.5
* .NET 3.5
* .NET 4.5.2
* .NET 4.6.1
* .NET 4.6.2
* .NETStandard 1.6
* .NET Core 1.0
* .NET Core 1.1
* .NET Core 2.0

## Supported target frameworks NCmdLiner 2.0.*

* Mono 4.5
* .NET 3.5
* .NET 4.5.2
* .NET 4.6.1
* .NET 4.6.2
* .NETStandard 1.6
* .NET Core 1.0
* .NET Core 1.1
* .NET Core 2.0

## Supported target frameworks NCmdLiner 3.0.*

* .NET 4.6.1
* .NETStandard 2.0
* .NETStandard 2.1
* .NET Core 2.0
* .NET Core 3.0

## Installation

NCmdLiner is installed into your project by Nuget Package Manager. 

```PowerShell
Install-Package NCmdLiner
```

## Example

```csharp
using System;
using NCmdLiner;
using NCmdLiner.Attributes;

namespace NCmdLiner.Example
{
    internal class Program
    {
        private static TryAsync<int> TryRun(string[] args) => () =>
        {
            IApplicationInfo exampleApplicationInfo = new ApplicationInfo();
            exampleApplicationInfo.Authors = "example@example.com, example2@example.com";
            exampleApplicationInfo.Copyright = "Copyright © ExampleCompany 2013";
            return CmdLinery.Run(typeof(ExampleCommands), args, exampleApplicationInfo);
        };
        
        private static int ErrorHandler(Exception ex, int exitCode)
        {
            Console.WriteLine(@"ERROR: " + ex.Message);
            return exitCode;
        }
        
        private static async Task<int> Main(string[] args)
        {
            return await TryRun(args).Match(ec => ec, exception => ErrorHandler(exception, 1));
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
MyUtil NET48 1.0.0.0 for .NET - Example of how to use NCmdLiner
Copyright © ExampleCompany 2013
Authors: example@example.com, example2@example.com
Usage: MyUtil.NET48.exe <command> [parameters]

Commands:
---------
Help                     Display this help text
License                  Display license
Credits                  Display credits
ExampleCommand1_1        ExampleCommand1 will only echo value of the input
                         parameters.
ExampleCommand1_2        ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameters.
ExampleCommand1_3        ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameters.

Commands and parameters:
------------------------
ExampleCommand1_1        ExampleCommand1 will only echo value of the input
                         parameters.
   /parameter1           [Required] parameter1 is a required string parameter
                         and must be specified. Alternative parameter name:
                         /p1
   /parameter2           [Required] parameter2 is required string parameter
                         and must be specified. Alternative parameter name:
                         /p2
   /parameter3           [Required] Parameter3 is required integer parameter
                         and must be specified. Alternative parameter name:
                         /p3
   /parameter4           [Optional] Parameter4 is an optional string
                         parameter and will have the default value set if not
                         specified. Alternative parameter name: /p4. Default
                         value: Some default value for parameter4
   /parameter5           [Optional] Parameter5 is an optional boolean
                         parameter and will have the default value set if not
                         specified. Alternative parameter name: /p5. Default
                         value: False

   Example: MyUtil.NET48.exe ExampleCommand1_1 /parameter1="Some example parameter1 value" /parameter2="Some example parameter2 value" /parameter3="10" /parameter4="Some example value for parameter4" /parameter5="True" 
   Example (alternative): MyUtil.NET48.exe ExampleCommand1_1 /p1="Some example parameter1 value" /p2="Some example parameter2 value" /p3="10" /p4="Some example value for parameter4" /p5="True" 


ExampleCommand1_2        ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameters.
   /parameter1           [Required] parameter1 is a required string parameter
                         and must be specified. Alternative parameter name:
                         /p1
   /parameter2           [Required] parameter2 is required string parameter
                         and must be specified. Alternative parameter name:
                         /p2
   /parameter3           [Required] Parameter3 is required integer parameter
                         and must be specified. Alternative parameter name:
                         /p3
   /parameter4           [Optional] Parameter4 is an optional string
                         parameter and will have the default value set if not
                         specified. Alternative parameter name: /p4. Default
                         value: Some default value for parameter4
   /parameter5           [Optional] Parameter5 is an optional boolean
                         parameter and will have the default value set if not
                         specified. Alternative parameter name: /p5. Default
                         value: False

   Example: MyUtil.NET48.exe ExampleCommand1_2 /parameter1="Some example parameter1 value" /parameter2="Some example parameter2 value" /parameter3="10" /parameter4="Some example value for parameter4" /parameter5="True" 
   Example (alternative): MyUtil.NET48.exe ExampleCommand1_2 /p1="Some example parameter1 value" /p2="Some example parameter2 value" /p3="10" /p4="Some example value for parameter4" /p5="True" 


ExampleCommand1_3        ExampleCommand2 will do the same as ExampleCommand1
                         only echo value of the input parameters.
   /parameter1           [Required] parameter1 is a required string parameter
                         and must be specified. Alternative parameter name:
                         /p1
   /stringArrayParameter2[Required] stringArrayParameter2 is a required
                         string array and must be specified. Alternative
                         parameter name: /ap2
   /booleanArrayParameter3[Required] booleanArrayParameter3 is a required
                         boolean array and must be specified. Alternative
                         parameter name: /ap3
   /integerArrayParameter4[Optional] integerArrayParameter4 is a optional
                         integer array. Alternative parameter name: /ap4.
                         Default value: [1;2;3]
   /doubleArrayParameter5[Optional] doubleArrayParameter5 is a optional
                         integer array. Alternative parameter name: /ap5.
                         Default value: [1,1;2,2;3,3]

   Example: MyUtil.NET48.exe ExampleCommand1_3 /parameter1="Some example parameter1 value" /stringArrayParameter2="['string1';'string2';'string3']" /booleanArrayParameter3="[True;False;True]" /integerArrayParameter4="[11;21;32]" /doubleArrayParameter5="[1,5678;23,425336;126,105]" 
   Example (alternative): MyUtil.NET48.exe ExampleCommand1_3 /p1="Some example parameter1 value" /ap2="['string1';'string2';'string3']" /ap3="[True;False;True]" /ap4="[11;21;32]" /ap5="[1,5678;23,425336;126,105]" 
```

## Building NCmdLiner 2.0.*

* Install Visual Studio 2017
* Install Mono 4.5 (http://www.mono-project.com/download/) + Run ..\tools\Mono Target\Mono Target.cmd
* Run .\Build.cmd

## Build Environment Setup 3.0.*

Run from an admin command prompt:

```batch
@"%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command "iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))" && SET "PATH=%PATH%;%ALLUSERSPROFILE%\chocolatey\bin"
choco feature enable -n allowGlobalConfirmation
choco install fake
choco upgrade fake
REM choco install visualstudio2019community
REM choco install visualstudio2019professional
choco install visualstudio2019enterprise
choco install visualstudio2019-workload-netcoretools
choco install git
choco feature disable -n allowGlobalConfirmation
```

## Build 3.0.*

Run from an standard command prompt.

```batch
mkdir c:\dev\github.trondr
cd c:\dev\github.trondr
git clone https://github.com/trondr/NCmdLiner.git ./NCmdLiner
cd c:\dev\github.trondr\NCmdLiner
Build.cmd
```
