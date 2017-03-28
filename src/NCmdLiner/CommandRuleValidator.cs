using System;
using System.Collections.Generic;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    internal class CommandRuleValidator : ICommandRuleValidator
    {
        public void Validate(string[] args, CommandRule commandRule)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (commandRule == null) throw new ArgumentNullException(nameof(commandRule));
            if (commandRule.Command == null) throw new NullReferenceException("Command object has not been initialized.");
            if (args.Length == 0) throw new MissingCommandException("No command was specified.");
            var command = args[0];
            if (commandRule.Command.Name.ToLower() != command.ToLower()) throw new InvalidCommandException("Invalid command: " + command + ". Valid command is: " + commandRule.Command.Name);
            if (!(commandRule.Command.RequiredParameters.Count == 0 && commandRule.Command.OptionalParameters.Count == 0))
            {
                var commandLineParameters = new ArgumentsParser().GetCommandLineParameters(args);
                var validCommandParameters = GetValidCommandParameters(commandRule.Command);
                foreach (var commandLineParameterName in commandLineParameters.Keys)
                {
                    if (!validCommandParameters.ContainsKey(commandLineParameterName))
                    {
                        throw new InvalidCommandParameterException("Invalid command line parameter: " +
                                                                   commandLineParameterName);
                    }
                }

                foreach (var requiredParameter in commandRule.Command.RequiredParameters)
                {
                    var commandLineHasParameterName = commandLineParameters.ContainsKey(requiredParameter.ToString());
                    var commandLineHasAlternativeParameterName =
                        !(string.IsNullOrEmpty(requiredParameter.AlternativeName)) &&
                        commandLineParameters.ContainsKey(requiredParameter.AlternativeName);
                    if (
                        !commandLineHasParameterName &&
                        !commandLineHasAlternativeParameterName
                    )
                    {
                        throw new MissingCommandParameterException("Required parameter is missing: " +
                                                                   requiredParameter.Name);
                    }
                    if (commandLineHasParameterName)
                        requiredParameter.Value = commandLineParameters[requiredParameter.ToString()].Value;
                    else requiredParameter.Value = commandLineParameters[requiredParameter.AlternativeName].Value;

                    //Check if example value has been specified
                    if (requiredParameter.ExampleValue == null)
                        throw new MissingExampleValueException(
                            string.Format("Example vaue has not been specified for parameter '{0}' in command '{1}'",
                                requiredParameter, commandRule.Command.Name));
                }

                foreach (var optionaParameter in commandRule.Command.OptionalParameters)
                {
                    if (commandLineParameters.ContainsKey(optionaParameter.ToString()))
                    {
                        optionaParameter.Value = commandLineParameters[optionaParameter.ToString()].Value;
                    }
                    else if (!(string.IsNullOrEmpty(optionaParameter.AlternativeName)) &&
                             commandLineParameters.ContainsKey(optionaParameter.AlternativeName))
                    {
                        optionaParameter.Value = commandLineParameters[optionaParameter.AlternativeName].Value;
                    }
                    if (optionaParameter.Value == null)
                        throw new MissingCommandParameterException("Optional parameter does not have a value: " +
                                                                   optionaParameter.Name);
                    //Check if example value has been specified
                    if (optionaParameter.ExampleValue == null)
                        throw new MissingExampleValueException(
                            string.Format("Example vaue has not been specified for parameter '{0}' in command '{1}'",
                                optionaParameter, commandRule.Command.Name));
                }
            }
            commandRule.IsValid = true;
        }

        /// <summary> Gets a valid command parameters. </summary>
        ///
        /// <remarks> Trond, 02.10.2012. </remarks>
        ///
        /// <param name="command">   The command. </param>
        ///
        /// <returns> The valid command parameters. </returns>
        private Dictionary<string, CommandParameter> GetValidCommandParameters(Command command)
        {
            var validCommandParameters = new Dictionary<string, CommandParameter>();
            foreach (var requiredParameter in command.RequiredParameters)
            {
                validCommandParameters.Add(requiredParameter.ToString(), requiredParameter);
                if (!string.IsNullOrEmpty(requiredParameter.AlternativeName))
                {
                    if (validCommandParameters.ContainsKey(requiredParameter.AlternativeName))
                    {
                        throw new DuplicateCommandParameterException(string.Format("Alternative parameter name '{0}' is allready in use in command '{1}'", requiredParameter.AlternativeName, command.Name));
                    }
                    validCommandParameters.Add(requiredParameter.AlternativeName, requiredParameter);
                }
            }
            foreach (var optionalParamter in command.OptionalParameters)
            {
                validCommandParameters.Add(optionalParamter.ToString(), optionalParamter);
                if (!string.IsNullOrEmpty(optionalParamter.AlternativeName))
                {
                    if (validCommandParameters.ContainsKey(optionalParamter.AlternativeName))
                    {
                        throw new DuplicateCommandParameterException(string.Format("Alternative parameter name '{0}' is allready in use in command '{1}'", optionalParamter.AlternativeName, command.Name));
                    }
                    validCommandParameters.Add(optionalParamter.AlternativeName, optionalParamter);
                }
            }
            return validCommandParameters;
        }
    }
}