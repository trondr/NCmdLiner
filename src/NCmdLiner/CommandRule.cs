// File: CommandRule.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public class CommandRule
    {
        public Command Command { get; set; }

        public MethodInfo Method { get; set; }

        public object[] BuildMethodParameters()
        {
            if (!_validated)
                throw new CommandRuleNotValidatedExption(
                    "Unable to build parameter array before command rule has been validated.");
            if (Method == null) throw new ArgumentNullException("Method", "Method property has not been initialiazed.");

            List<object> parameterValues = new List<object>();
            ParameterInfo[] methodParameters = Method.GetParameters();

            StringToObject stringToObject = new StringToObject(new ArrayParser(),
                                                               System.Threading.Thread.CurrentThread.CurrentCulture);

            for (int i = 0; i < Command.RequiredParameters.Count; i++)
            {
                parameterValues.Add(stringToObject.ConvertValue(Command.RequiredParameters[i].Value,
                                                                methodParameters[i].ParameterType));
            }
            for (int i = 0; i < Command.OptionalParameters.Count; i++)
            {
                parameterValues.Add(stringToObject.ConvertValue(Command.OptionalParameters[i].Value,
                                                                methodParameters[i + Command.RequiredParameters.Count]
                                                                    .ParameterType));
            }
            return parameterValues.ToArray();
        }

        public void Validate(string[] args)
        {
            if (args == null) throw new ArgumentNullException("args");
            if (Command == null) throw new NullReferenceException("Command object has not been initialized.");
            if (args.Length == 0) throw new MissingCommandException("No command was specified.");
            string command = args[0];
            if (Command.Name.ToLower() != command.ToLower())
                throw new InvalidCommandException("Invalid command: " + command + ". Valid command is: " + Command.Name);
            if (!(Command.RequiredParameters.Count == 0 && Command.OptionalParameters.Count == 0))
            {
                Dictionary<string, CommandLineParameter> commandLineParameters = GetCommandLineParameters(args);
                Dictionary<string, CommandParameter> validCommandParameters = GetValidCommandParameters(Command);
                foreach (string commandLineParameterName in commandLineParameters.Keys)
                {
                    if (!validCommandParameters.ContainsKey(commandLineParameterName))
                    {
                        throw new InvalidCommandParameterException("Invalid command line parameter: " +
                                                                   commandLineParameterName);
                    }
                }

                foreach (CommandParameter requiredParameter in Command.RequiredParameters)
                {
                    bool commandLineHasParameterName = commandLineParameters.ContainsKey(requiredParameter.ToString());
                    bool commandLineHasAlternativeParameterName =
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
                    else if (commandLineHasAlternativeParameterName)
                        requiredParameter.Value = commandLineParameters[requiredParameter.AlternativeName].Value;

                    //Check if example value has been specified
                    if (requiredParameter.ExampleValue == null)
                        throw new MissingExampleValueException(
                            string.Format("Example vaue has not been specified for parameter '{0}' in command '{1}'",
                                          requiredParameter, Command.Name));
                }

                foreach (OptionalCommandParameter optionaParameter in Command.OptionalParameters)
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
                                          optionaParameter, Command.Name));
                }
            }
            _validated = true;
        }

        private string ExeName
        {
            get
            {
                if (string.IsNullOrEmpty(_exeName))
                {
                    _exeName = new FileInfo(Process.GetCurrentProcess().MainModule.FileName).Name;
                }
                return _exeName;
            }
            set { _exeName = value; }
        }

        public object Instance { get; set; }

        private string _exeName;
        private static bool _validated;

        private string GetValidValuesHelp(IEnumerable<string> validValues)
        {
            if (validValues == null) throw new ArgumentNullException("validValues");
            StringBuilder sb = new StringBuilder();
            foreach (string validValue in validValues)
            {
                sb.Append(validValue + "|");
            }
            return sb.ToString().TrimEnd('|');
        }

        #region Private methods

        /// <summary> Gets a valid command parameters. </summary>
        ///
        /// <remarks> Trond, 02.10.2012. </remarks>
        ///
        /// <param name="command">   The command. </param>
        ///
        /// <returns> The valid command parameters. </returns>
        private Dictionary<string, CommandParameter> GetValidCommandParameters(Command command)
        {
            Dictionary<string, CommandParameter> validCommandParameters = new Dictionary<string, CommandParameter>();
            foreach (CommandParameter requiredParameter in command.RequiredParameters)
            {
                validCommandParameters.Add(requiredParameter.ToString(), requiredParameter);
                if (!string.IsNullOrEmpty(requiredParameter.AlternativeName))
                {
                    if (validCommandParameters.ContainsKey(requiredParameter.AlternativeName))
                    {
                        throw new DuplicateCommandParameterException(
                            string.Format("Alternative parameter name '{0}' is allready in use in command '{1}'",
                                          requiredParameter.AlternativeName, command.Name));
                    }
                    validCommandParameters.Add(requiredParameter.AlternativeName, requiredParameter);
                }
            }
            foreach (CommandParameter optionalParamter in command.OptionalParameters)
            {
                validCommandParameters.Add(optionalParamter.ToString(), optionalParamter);
                if (!string.IsNullOrEmpty(optionalParamter.AlternativeName))
                {
                    if (validCommandParameters.ContainsKey(optionalParamter.AlternativeName))
                    {
                        throw new DuplicateCommandParameterException(
                            string.Format("Alternative parameter name '{0}' is allready in use in command '{1}'",
                                          optionalParamter.AlternativeName, command.Name));
                    }
                    validCommandParameters.Add(optionalParamter.AlternativeName, optionalParamter);
                }
            }
            return validCommandParameters;
        }

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
        private Dictionary<string, CommandLineParameter> GetCommandLineParameters(string[] args)
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
                        throw new InvalidCommandParameterFormatException(
                            string.Format(
                                "Invalid command line parameter format: '{0}'. Commandline parameter must be on the format '/ParameterName=ParameterValue' or /ParameterName=\"Parameter Value\"'",
                                args[i]));
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

        #endregion
    }
}