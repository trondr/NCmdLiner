// File: CommandRuleProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
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
            if (methodInfo == null) throw new ArgumentNullException("methodInfo");
            object[] customAttributes = methodInfo.GetCustomAttributes(false);
            CommandAttribute commandAttribute = null;
            foreach (object customAttribute in customAttributes)
            {
                if (customAttribute is CommandAttribute)
                {
                    //if (!methodInfo.IsStatic)throw new CommandMehtodNotStaticException("Command method is not a static method: " + methodInfo.Name);
                    commandAttribute = (CommandAttribute) customAttribute;
                }
            }
            if (commandAttribute == null)
                throw new MissingCommandAttributeException("Method is not decorate with the [Command] attribute: " +
                                                           methodInfo.Name);
            CommandRule commandRule = new CommandRule();
            commandRule.Method = methodInfo;
            commandRule.Instance = targetObject;
            commandRule.Command = new Command {Name = methodInfo.Name, Description = commandAttribute.Description};
            bool optionalParamterFound = false;
            foreach (ParameterInfo parameter in methodInfo.GetParameters())
            {
                object[] attributes = parameter.GetCustomAttributes(typeof (CommandParameterAttribute), false);
                if (attributes.Length == 0)
                {
                    throw new MissingCommandParameterAttributeException(
                        string.Format(
                            "Command parameter attribute is not decorating the parameter '{0}' in the method '{1}'",
                            parameter.Name, methodInfo.Name));
                }
                if (attributes.Length > 1)
                {
                    throw new DuplicateCommandParameterAttributeException(
                        string.Format(
                            "More than one command parameter attribute decorates the parameter '{0}' in the method '{1}'.",
                            parameter.Name, methodInfo.Name));
                }
                if (attributes[0] is OptionalCommandParameterAttribute)
                {
                    optionalParamterFound = true;
                    OptionalCommandParameterAttribute optionalCommandParameterAttribute = (OptionalCommandParameterAttribute) attributes[0];        
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
                    RequiredCommandParameterAttribute requiredCommandParameterAttribute =
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
            return GetCommandRules(new Type[] {targetType});
        }

        public List<CommandRule> GetCommandRules(object[] targetObjects)
        {
            List<CommandRule> commandRules = new List<CommandRule>();
            foreach (object targetObject in targetObjects)
            {
                MethodInfo[] methods = targetObject.GetType().GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                GetCommandRulesFromMethods(methods, targetObject, commandRules);
            }
            return commandRules;
        }

        public List<CommandRule> GetCommandRules(Type[] targetTypes)
        {
            List<CommandRule> commandRules = new List<CommandRule>();
            foreach (Type targetType in targetTypes)
            {
                MethodInfo[] methods = targetType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);                
                GetCommandRulesFromMethods(methods, null, commandRules);
            }
            return commandRules;
        }

        private void GetCommandRulesFromMethods(IEnumerable<MethodInfo> methods, object targetObject, List<CommandRule> commandRules)
        {
            foreach (MethodInfo method in methods)
            {
                object[] customAttributes = method.GetCustomAttributes(false);
                foreach (object customAttribute in customAttributes)
                {
                    if (customAttribute is CommandAttribute)
                    {
                        CommandRule newCommandRule = GetCommandRule(method, targetObject);
                        CommandRule existingCommandRule = commandRules.Find(rule => rule.Command.Name == newCommandRule.Command.Name);
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