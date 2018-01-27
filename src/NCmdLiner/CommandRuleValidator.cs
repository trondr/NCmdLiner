using System;
using System.Collections.Generic;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public class CommandRuleValidator : ICommandRuleValidator
    {
        public Result<int> Validate(string[] args, CommandRule commandRule)
        {
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (commandRule == null) throw new ArgumentNullException(nameof(commandRule));
            if (commandRule.Command == null) throw new NullReferenceException("Command object has not been initialized.");
            if (args.Length == 0)
                return Result.Fail<int>(new MissingCommandException( "Command not specified."));
            var command = args[0];
            if (commandRule.Command.Name.ToLower() != command.ToLower())
                return Result.Fail<int>(new InvalidCommandException( "Invalid command: " + command + ". Valid command is: " + commandRule.Command.Name));
            if (!(commandRule.Command.RequiredParameters.Count == 0 && commandRule.Command.OptionalParameters.Count == 0))
            {
                var commandLineParameters = new ArgumentsParser().GetCommandLineParameters(args);
                if (commandLineParameters.IsFailure)
                    return Result.Fail<int>(commandLineParameters.Exception);

                var validCommandParameters = GetValidCommandParameters(commandRule.Command);
                if (validCommandParameters.IsFailure)
                    return Result.Fail<int>(validCommandParameters.Exception);

                foreach (var commandLineParameterName in commandLineParameters.Value.Keys)
                {
                    if (!validCommandParameters.Value.ContainsKey(commandLineParameterName))
                    {
                        return Result.Fail<int>(new InvalidCommandParameterException("Invalid command line parameter: " + commandLineParameterName));
                    }
                }

                foreach (var requiredParameter in commandRule.Command.RequiredParameters)
                {
                    var commandLineHasParameterName = commandLineParameters.Value.ContainsKey(requiredParameter.ToString());
                    var commandLineHasAlternativeParameterName =
                        !(string.IsNullOrEmpty(requiredParameter.AlternativeName)) &&
                        commandLineParameters.Value.ContainsKey(requiredParameter.AlternativeName);
                    if (
                        !commandLineHasParameterName &&
                        !commandLineHasAlternativeParameterName
                    )
                    {
                        return Result.Fail<int>(new MissingCommandParameterException("Required parameter is missing: " + requiredParameter.Name));
                    }
                    if (commandLineHasParameterName)
                        requiredParameter.Value = commandLineParameters.Value[requiredParameter.ToString()].Value;
                    else requiredParameter.Value = commandLineParameters.Value[requiredParameter.AlternativeName].Value;

                    //Check if example value has been specified
                    if (requiredParameter.ExampleValue == null)
                        return Result.Fail<int>(new MissingExampleValueException($"Example vaue has not been specified for parameter '{requiredParameter}' in command '{commandRule.Command.Name}'"));
                }

                foreach (var optionaParameter in commandRule.Command.OptionalParameters)
                {
                    if (commandLineParameters.Value.ContainsKey(optionaParameter.ToString()))
                    {
                        optionaParameter.Value = commandLineParameters.Value[optionaParameter.ToString()].Value;
                    }
                    else if (!(string.IsNullOrEmpty(optionaParameter.AlternativeName)) &&
                             commandLineParameters.Value.ContainsKey(optionaParameter.AlternativeName))
                    {
                        optionaParameter.Value = commandLineParameters.Value[optionaParameter.AlternativeName].Value;
                    }
                    if (optionaParameter.Value == null)
                        return Result.Fail<int>(new MissingCommandParameterException("Optional parameter does not have a value: " + optionaParameter.Name));
                    //Check if example value has been specified
                    if (optionaParameter.ExampleValue == null)
                        return Result.Fail<int>(new MissingExampleValueException($"Example vaue has not been specified for parameter '{optionaParameter}' in command '{commandRule.Command.Name}'"));
                }
            }
            commandRule.IsValid = true;
            return Result.Ok(0);
        }

        /// <summary> Gets a valid command parameters. </summary>
        ///
        /// <remarks> Trond, 02.10.2012. </remarks>
        ///
        /// <param name="command">   The command. </param>
        ///
        /// <returns> The valid command parameters. </returns>
        private Result<Dictionary<string, CommandParameter>> GetValidCommandParameters(Command command)
        {
            var validCommandParameters = new Dictionary<string, CommandParameter>();
            foreach (var requiredParameter in command.RequiredParameters)
            {
                validCommandParameters.Add(requiredParameter.ToString(), requiredParameter);
                if (!string.IsNullOrEmpty(requiredParameter.AlternativeName))
                {
                    if (validCommandParameters.ContainsKey(requiredParameter.AlternativeName))
                    {
                        return Result.Fail<Dictionary<string, CommandParameter>>(new DuplicateCommandParameterException($"Alternative parameter name '{requiredParameter.AlternativeName}' is allready in use in command '{command.Name}'"));
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
                        return Result.Fail<Dictionary<string, CommandParameter>>(new DuplicateCommandParameterException($"Alternative parameter name '{optionalParamter.AlternativeName}' is allready in use in command '{command.Name}'"));
                    }
                    validCommandParameters.Add(optionalParamter.AlternativeName, optionalParamter);
                }
            }
            return Result.Ok(validCommandParameters);
        }
    }
}