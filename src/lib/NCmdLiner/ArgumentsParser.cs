using System.Collections.Generic;
using System.Text.RegularExpressions;
using LanguageExt.Common;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public class ArgumentsParser : IArgumentsParser
    {
        public Result<Dictionary<string, CommandLineParameter>> GetCommandLineParameters(string[] args)
        {
            var commandLineParameters = new Dictionary<string, CommandLineParameter>();
            if (args.Length >= 2)
            {
                var parameterRegex = new Regex("[/-](.+?)=(.+)");
                for (var i = 1; i < args.Length; i++)
                {
                    var match = parameterRegex.Match(args[i]);
                    if (!match.Success)
                    {
                        return new Result<Dictionary<string, CommandLineParameter>>(new InvalidCommandParameterFormatException(
                            $"Invalid command line parameter format: '{args[i]}'. Commandline parameter must be on the format '/ParameterName=ParameterValue' or '/ParameterName=\"Parameter Value\"'"));
                    }
                    var commandLineParameter = new CommandLineParameter();
                    commandLineParameter.Name = match.Groups[1].Value;
                    commandLineParameter.Value = match.Groups[2].Value.Trim('"').Trim('\'');
                    if (commandLineParameters.ContainsKey(commandLineParameter.ToString()))
                    {
                        return new Result<Dictionary<string, CommandLineParameter>>(new DuplicateCommandParameterException(
                            "Command line parameter appeared more than once: " + commandLineParameter.Name));
                    }
                    commandLineParameters.Add(commandLineParameter.ToString(), commandLineParameter);
                }
            }
            return new Result<Dictionary<string, CommandLineParameter>>(commandLineParameters);
        }
    }
}