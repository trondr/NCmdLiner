// File: CommandRuleProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright � <github.com/trondr> 2013 
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


        public CommandRule GetCommandRule(MethodInfo methodInfo)
        {
            return GetCommandRule(methodInfo, null);
        }
        
        public CommandRule GetCommandRule(MethodInfo methodInfo, object targetObject)
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
                throw new MissingCommandAttributeException("Method is not decorate with the [Command] attribute: " +
                                                           methodInfo.Name);
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
                    throw new MissingCommandParameterAttributeException(
                        string.Format(
                            "Command parameter attribute is not decorating the parameter '{0}' in the method '{1}'",
                            parameter.Name, methodInfo.Name));
                }
                if (attributes.Count > 1)
                {
                    throw new DuplicateCommandParameterAttributeException(
                        string.Format(
                            "More than one command parameter attribute decorates the parameter '{0}' in the method '{1}'.",
                            parameter.Name, methodInfo.Name));
                }
                var attribute = attributes[0] as OptionalCommandParameterAttribute;
                if (attribute != null)
                {
                    optionalParamterFound = true;
                    var optionalCommandParameterAttribute = attribute;        
                    commandRule.Command.OptionalParameters.Add(
                        new OptionalCommandParameter(optionalCommandParameterAttribute.DefaultValue)
                            {
                                Name = parameter.Name,
                                Description = optionalCommandParameterAttribute.Description,
                                ExampleValue = optionalCommandParameterAttribute.ExampleValue,
                                AlternativeName = optionalCommandParameterAttribute.AlternativeName
                            });
                }
                else if (attributes[0] is RequiredCommandParameterAttribute)
                {
                    if (optionalParamterFound)
                        throw new RequiredParameterFoundAfterOptionalParameterExecption(
                            string.Format(
                                "Required parameter '{0}' in method '{1}' must be defined before any optional parameters.",
                                parameter.Name, methodInfo.Name));
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
            return commandRule;
        }

        public List<CommandRule> GetCommandRules(Type targetType)
        {
            return GetCommandRules(new[] {targetType});
        }

        public List<CommandRule> GetCommandRules(object[] targetObjects)
        {
            var commandRules = new List<CommandRule>();
            foreach (var targetObject in targetObjects)
            {
                var methods = targetObject.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                GetCommandRulesFromMethods(methods, targetObject, commandRules);
            }
            return commandRules;
        }

        public List<CommandRule> GetCommandRules(Type[] targetTypes)
        {
            var commandRules = new List<CommandRule>();
            foreach (var targetType in targetTypes)
            {
                var methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);                
                GetCommandRulesFromMethods(methods, null, commandRules);
            }
            return commandRules;
        }

        private void GetCommandRulesFromMethods(IEnumerable<MethodInfo> methods, object targetObject, List<CommandRule> commandRules)
        {
            foreach (var method in methods)
            {
                var customAttributes = method.GetCustomAttributes(false);
                foreach (var customAttribute in customAttributes)
                {
                    if (customAttribute is CommandAttribute)
                    {
                        var newCommandRule = GetCommandRule(method, targetObject);
                        var existingCommandRule = commandRules.Find(rule => rule.Command.Name == newCommandRule.Command.Name);
                        if (existingCommandRule != null)
                        {
                            throw new DuplicateCommandException("A duplicate command has been defined: " + newCommandRule.Command.Name);
                        }
                        commandRules.Add(newCommandRule);
                    }
                }
            }
        }

    }
}