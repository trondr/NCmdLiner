# NCmdLiner

NCmdLiner is a self documenting command line parser for .NET

Commands and parameters are specified as attributes on methods of a static class.

Parameters can be required or optional.

Parameters can be of types: string, integer, decimal, float, double, boolean, DateTime and array of all these.

The automatic documentation outputs ready-to-use examples of each command and its parameters.

## Credits

NCmdLiner is derived from the NConsoler project http://nconsoler.csharpus.com

## Example

```csharp
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
                //Override some application info to modify the header of the auto documentation
                IApplicationInfo exampleApplicationInfo = new ApplicationInfo();
                exampleApplicationInfo.Authors = "example@example.com, example2@example.com"; //Set the optional authors property
                exampleApplicationInfo.Copyright = "Copyright © examplecompany 2013"; //Override the assembly info copyright property
                
                //Extend the default console messenger to show the help text in Notepad.exe as well as the default console:
                IMessenger notepadMessenger = new NotepadMessenger(new ConsoleMessenger());

                //Now parse and run the command line
                CmdLinery.Run(typeof(ExampleCommands), args, exampleApplicationInfo, notepadMessenger);
                
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
    }

    public class NotepadMessenger: IMessenger
    {
        private readonly StringBuilder _message = new StringBuilder();
        private readonly IMessenger _defaultMessenger;

        public NotepadMessenger(IMessenger defaultMessenger)
        {            
            _defaultMessenger = defaultMessenger;
        }

        public void Write(string formatMessage, params object[] args)
        {
            _message.Append(string.Format(formatMessage,args));
            _defaultMessenger.Write(formatMessage,args);
        }

        public void WriteLine(string formatMessage, params object[] args)
        {
            _message.Append(string.Format(formatMessage, args) + Environment.NewLine);
            _defaultMessenger.WriteLine(formatMessage, args);
        }

        public void Show()
        {
            string tempFileName = Path.GetTempFileName();
            using (StreamWriter sw = new StreamWriter(tempFileName))
            {
                sw.Write(_message.ToString());
            }
            Process.Start("Notepad.exe", tempFileName);
            Thread.Sleep(2000); //Wait until Notepad has started before deleting the temporary file
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
            _defaultMessenger.Show();
        }
    }
}

```

## Example output

```
NCmdLiner Example 1.0.0.0 for .NET - Example of how to use NCmdLiner
Copyright © examplecompany 2013
Authors: example@example.com, example2@example.com
Usage: NCmdLiner.Example.exe <command> [parameters]

Commands:
---------
Help              Display this help text
License           Display license
Credits           Display credits
ExampleCommand1   ExampleCommand1 will only echo value of the input
                  parameteres.
ExampleCommand2   ExampleCommand2 will do the same as ExampleCommand1 only
                  echo value of the input parameteres.

Commands and parameters:
------------------------
ExampleCommand1   ExampleCommand1 will only echo value of the input
                  parameteres.
   /parameter1    [Required] parameter1 is a required string paramter and
                  must be specified. Alternative parameter name: /p1
   /parameter2    [Required] parameter2 is required string paramter and must
                  be specified. Alternative parameter name: /p2
   /parameter3    [Required] Parameter3 is required integer paramter and must
                  be specified. Alternative parameter name: /p3
   /parameter4    [Optional] Parameter4 is an optional string paramter and
                  will have the default value set if not specified.
                  Alternative parameter name: /p4. Default value: Some
                  default value for parameter4
   /parameter5    [Optional] Parameter5 is an optional boolean parameter and
                  will have the default value set if not specified.
                  Alternative parameter name: /p5. Default value: False

   Example: NCmdLiner.Example.exe ExampleCommand1 /parameter1="Some example parameter1 value" /parameter2="Some example parameter2 value" /parameter3="10" /parameter4="Some example value for parameter4" /parameter5="True" 
   Example (alternative): NCmdLiner.Example.exe ExampleCommand1 /p1="Some example parameter1 value" /p2="Some example parameter2 value" /p3="10" /p4="Some example value for parameter4" /p5="True" 


ExampleCommand2   ExampleCommand2 will do the same as ExampleCommand1 only
                  echo value of the input parameteres.
   /parameter1    [Required] parameter1 is a required string paramter and
                  must be specified. Alternative parameter name: /p1
   /parameter2    [Required] parameter2 is required string paramter and must
                  be specified. Alternative parameter name: /p2
   /parameter3    [Required] Parameter3 is required integer paramter and must
                  be specified. Alternative parameter name: /p3
   /parameter4    [Optional] Parameter4 is an optional string paramter and
                  will have the default value set if not specified.
                  Alternative parameter name: /p4. Default value: Some
                  default value for parameter4
   /parameter5    [Optional] Parameter5 is an optional boolean parameter and
                  will have the default value set if not specified.
                  Alternative parameter name: /p5. Default value: False

   Example: NCmdLiner.Example.exe ExampleCommand2 /parameter1="Some example parameter1 value" /parameter2="Some example parameter2 value" /parameter3="10" /parameter4="Some example value for parameter4" /parameter5="True" 
   Example (alternative): NCmdLiner.Example.exe ExampleCommand2 /p1="Some example parameter1 value" /p2="Some example parameter2 value" /p3="10" /p4="Some example value for parameter4" /p5="True"

```




