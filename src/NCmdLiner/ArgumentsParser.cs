using System.Collections.Generic;
using System.Text.RegularExpressions;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    internal class ArgumentsParser
    {
        /// <summary> Gets a command line parameters. </summary>
        ///
        /// <remarks> Trond, 02.10.2012. </remarks>
        ///
        /// <exception cref="InvalidCommandParameterFormatException"> Thrown when an invalid command
        ///                                                           parameter format error condition
        ///                                                           occurs. </exception>
        /// <exception cref="DuplicateCommandParameterException">     Thrown when a duplicate command
        ///                                                           parameter error condition occurs. </exception>
        ///
        /// <param name="args">   The arguments. </param>
        ///
        /// <returns> The command line parameters. </returns>
        public Dictionary<string, CommandLineParameter> GetCommandLineParameters(string[] args)
        {
            Dictionary<string, CommandLineParameter> commandLineParameters =
                new Dictionary<string, CommandLineParameter>();
            if (args.Length >= 2)
            {
                Regex parameterRegex = new Regex("[/-](.+?)=(.+)");
                for (int i = 1; i < args.Length; i++)
                {
                    Match match = parameterRegex.Match(args[i]);
                    if (!match.Success)
                    {
                        throw new InvalidCommandParameterFormatException(string.Format("Invalid command line parameter format: '{0}'. Commandline parameter must be on the format '/ParameterName=ParameterValue' or '/ParameterName=\"Parameter Value\"'", args[i]));
                    }
                    CommandLineParameter commandLineParameter = new CommandLineParameter();
                    commandLineParameter.Name = match.Groups[1].Value;
                    commandLineParameter.Value = match.Groups[2].Value.Trim('"').Trim('\'');
                    if (commandLineParameters.ContainsKey(commandLineParameter.ToString()))
                    {
                        throw new DuplicateCommandParameterException(
                            "Command line parameter appeared more than once: " + commandLineParameter.Name);
                    }
                    commandLineParameters.Add(commandLineParameter.ToString(), commandLineParameter);
                }
            }
            return commandLineParameters;
        }
    }
}