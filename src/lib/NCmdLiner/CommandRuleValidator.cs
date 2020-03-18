using System;
using System.Collections.Generic;
using LanguageExt.Common;
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
                return new Result<int>(new MissingCommandException( "Command not specified."));
            var command = args[0];
            if (commandRule.Command.Name.ToLower() != command.ToLower())
                return new Result<int>(new InvalidCommandException( "Invalid command: " + command + ". Valid command is: " + commandRule.Command.Name));

            if (commandRule.Instance == null && commandRule.Method != null && !commandRule.Method.IsStatic)
            {
                return new Result<int>(new NCmdLinerException($"The command '{commandRule.Command.Name}' is defined as a non static method. Make the method static to fix the issue."));
            }
            
            if (!(commandRule.Command.RequiredParameters.Count == 0 && commandRule.Command.OptionalParameters.Count == 0))
            {
                var commandLineParametersResult = new ArgumentsParser().GetCommandLineParameters(args);
                if (commandLineParametersResult.IsFaulted)
                    return new Result<int>(commandLineParametersResult.Match(parameters => throw new InvalidOperationException("Success not expected"),exception => exception));
                var commandLineParameters = commandLineParametersResult.Match(parameters => parameters, exception => throw new InvalidOperationException("Failure not expected"));

                var validCommandParametersResult = GetValidCommandParameters(commandRule.Command);
                if (validCommandParametersResult.IsFaulted)
                    return new Result<int>(validCommandParametersResult.Match(parameters => throw new InvalidOperationException("Success not expected"), exception => exception));
                var validCommandParameters = validCommandParametersResult.Match(parameters => parameters, exception => throw new InvalidOperationException("Failure not expected"));

                foreach (var commandLineParameterName in commandLineParameters.Keys)
                {
                    if (!validCommandParameters.ContainsKey(commandLineParameterName))
                    {
                        return new Result<int>(new InvalidCommandParameterException("Invalid command line parameter: " + commandLineParameterName));
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
                        return new Result<int>(new MissingCommandParameterException("Required parameter is missing: " + requiredParameter.Name));
                    }
                    if (commandLineHasParameterName)
                        requiredParameter.Value = commandLineParameters[requiredParameter.ToString()].Value;
                    else requiredParameter.Value = commandLineParameters[requiredParameter.AlternativeName].Value;

                    //Check if example value has been specified
                    if (requiredParameter.ExampleValue == null)
                        return new Result<int>(new MissingExampleValueException($"Example value has not been specified for parameter '{requiredParameter}' in command '{commandRule.Command.Name}'"));
                }

                foreach (var optionalParameter in commandRule.Command.OptionalParameters)
                {
                    if (commandLineParameters.ContainsKey(optionalParameter.ToString()))
                    {
                        optionalParameter.Value = commandLineParameters[optionalParameter.ToString()].Value;
                    }
                    else if (!(string.IsNullOrEmpty(optionalParameter.AlternativeName)) &&
                             commandLineParameters.ContainsKey(optionalParameter.AlternativeName))
                    {
                        optionalParameter.Value = commandLineParameters[optionalParameter.AlternativeName].Value;
                    }
                    if (optionalParameter.Value == null)
                        return new Result<int>(new MissingCommandParameterException("Optional parameter does not have a value: " + optionalParameter.Name));
                    //Check if example value has been specified
                    if (optionalParameter.ExampleValue == null)
                        return new Result<int>(new MissingExampleValueException($"Example value has not been specified for parameter '{optionalParameter}' in command '{commandRule.Command.Name}'"));
                }
            }
            commandRule.IsValid = true;
            return new Result<int>(0);
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
                        return new Result<Dictionary<string, CommandParameter>>(new DuplicateCommandParameterException($"Alternative parameter name '{requiredParameter.AlternativeName}' is already in use in command '{command.Name}'"));
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
                        return new Result<Dictionary<string, CommandParameter>>(new DuplicateCommandParameterException($"Alternative parameter name '{optionalParamter.AlternativeName}' is already in use in command '{command.Name}'"));
                    }
                    validCommandParameters.Add(optionalParamter.AlternativeName, optionalParamter);
                }
            }
            return new Result<Dictionary<string, CommandParameter>>(validCommandParameters);
        }
    }
}