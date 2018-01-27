// File: CommandRuleProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public class CommandRuleProvider : ICommandRuleProvider
    {
        public Result<CommandRule> GetCommandRule(MethodInfo methodInfo)
        {
            return GetCommandRule(methodInfo, null);
        }
        
        public Result<CommandRule> GetCommandRule(MethodInfo methodInfo, object targetObject)
        {
            if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));
            var customAttributes = methodInfo.GetCustomAttributes(false);
            CommandAttribute commandAttribute = null;
            foreach (var customAttribute in customAttributes)
            {
                var attribute = customAttribute as CommandAttribute;
                if (attribute != null)
                {                    
                    commandAttribute = attribute;
                }
            }
            if (commandAttribute == null)
                return Result.Fail<CommandRule>(new MissingCommandAttributeException("Method is not decorate with the [Command] attribute: " + methodInfo.Name));
            var commandRule = new CommandRule
            {
                Method = methodInfo,
                Instance = targetObject,
                Command = new Command
                    {
                        Name = methodInfo.Name,
                        Description = commandAttribute.Description,
                        Summary = commandAttribute.Summary
                    }
            };
            var optionalParamterFound = false;
            foreach (var parameter in methodInfo.GetParameters())
            {
                var attributes = parameter.GetCustomAttributes(typeof (CommandParameterAttribute), false).ToList();
                if (attributes.Count == 0)
                {
                    return Result.Fail<CommandRule>(new MissingCommandParameterAttributeException($"Command parameter attribute is not decorating the parameter '{parameter.Name}' in the method '{methodInfo.Name}'"));
                }
                if (attributes.Count > 1)
                {
                    return Result.Fail<CommandRule>(new DuplicateCommandParameterAttributeException($"More than one command parameter attribute decorates the parameter '{parameter.Name}' in the method '{methodInfo.Name}'."));
                }
                var attribute = attributes[0] as OptionalCommandParameterAttribute;
                if (attribute != null)
                {
                    var optionalCommandParameterResult = GetOptionalCommandParameterAttribute(parameter.Name,attribute);
                    if (optionalCommandParameterResult.IsFailure)
                        return Result.Fail<CommandRule>(optionalCommandParameterResult.Exception);
                    optionalParamterFound = true;
                    commandRule.Command.OptionalParameters.Add(optionalCommandParameterResult.Value);
                }
                else if (attributes[0] is RequiredCommandParameterAttribute)
                {
                    if (optionalParamterFound)
                        return Result.Fail<CommandRule>(new RequiredParameterFoundAfterOptionalParameterExecption($"Required parameter '{parameter.Name}' in method '{methodInfo.Name}' must be defined before any optional parameters."));
                    var requiredCommandParameterAttribute =
                        (RequiredCommandParameterAttribute) attributes[0];

                    commandRule.Command.RequiredParameters.Add(new RequiredCommandParameter
                        {
                            Name = parameter.Name,
                            Description = requiredCommandParameterAttribute.Description,
                            ExampleValue = requiredCommandParameterAttribute.ExampleValue,
                            AlternativeName = requiredCommandParameterAttribute.AlternativeName
                        });
                }
            }
            return Result.Ok(commandRule);
        }

        private Result<OptionalCommandParameter> GetOptionalCommandParameterAttribute(string parameterName, OptionalCommandParameterAttribute optionalCommandParameterAttribute)
        {
            try
            {
                var optionalCommandParameter = new OptionalCommandParameter(optionalCommandParameterAttribute.DefaultValue)
                {
                    Name = parameterName,
                    Description = optionalCommandParameterAttribute.Description,
                    ExampleValue = optionalCommandParameterAttribute.ExampleValue,
                    AlternativeName = optionalCommandParameterAttribute.AlternativeName
                };
                return Result.Ok(optionalCommandParameter);
            }
            catch (MissingDefaultValueException ex)
            {
                return Result.Fail<OptionalCommandParameter>(ex);
            }
        }

        public Result<List<CommandRule>> GetCommandRules(Type targetType)
        {
            return GetCommandRules(new[] {targetType});
        }

        public Result<List<CommandRule>> GetCommandRules(object[] targetObjects)
        {
            var commandRules = new List<CommandRule>();
            foreach (var targetObject in targetObjects)
            {
                var methods = targetObject.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                var commandRulesResult = GetCommandRulesFromMethods(methods, targetObject, commandRules);
                if (commandRulesResult.IsFailure)
                    return commandRulesResult;
                commandRules= commandRulesResult.Value;
            }
            return Result.Ok(commandRules);
        }

        public Result<List<CommandRule>> GetCommandRules(Type[] targetTypes)
        {
            List<CommandRule> commandRules = new List<CommandRule>();
            foreach (var targetType in targetTypes)
            {
                var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                var commandRulesResult = GetCommandRulesFromMethods(methods, null, commandRules);
                if (commandRulesResult.IsFailure)
                    return commandRulesResult;
                commandRules = commandRulesResult.Value;
            }
            return Result.Ok(commandRules);
        }

        private Result<List<CommandRule>> GetCommandRulesFromMethods(IEnumerable<MethodInfo> methods, object targetObject, List<CommandRule> commandRules)
        {
            foreach (var method in methods)
            {
                var customAttributes = method.GetCustomAttributes(false);
                foreach (var customAttribute in customAttributes)
                {
                    if (customAttribute is CommandAttribute)
                    {
                        var newCommandRuleResult = GetCommandRule(method, targetObject);
                        if (newCommandRuleResult.IsFailure)
                            return Result.Fail <List < CommandRule >> (newCommandRuleResult.Exception);
                        var existingCommandRule = commandRules.Find(rule => rule.Command.Name == newCommandRuleResult.Value.Command.Name);
                        if (existingCommandRule != null)
                        {
                            return Result.Fail<List<CommandRule>>(new DuplicateCommandException("A duplicate command has been defined: " + newCommandRuleResult.Value.Command.Name));
                        }
                        commandRules.Add(newCommandRuleResult.Value);
                    }
                }
            }
            return Result.Ok(commandRules);
        }

    }
}