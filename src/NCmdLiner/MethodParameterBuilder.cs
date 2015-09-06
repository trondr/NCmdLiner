using System;
using System.Collections.Generic;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    internal class MethodParameterBuilder : IMethodParameterBuilder
    {
        private readonly IStringToObject _stringToObject;

        public MethodParameterBuilder(IStringToObject stringToObject)
        {
            _stringToObject = stringToObject;
        }

        public object[] BuildMethodParameters(CommandRule commandRule)
        {
            if (!commandRule.IsValid)
            {
                throw new CommandRuleNotValidatedExption("Unable to build parameter array before command rule has been validated.");
            }
            if (commandRule.Method == null) throw new NullReferenceException("Method property has not been initialiazed.");

            var parameterValues = new List<object>();
            var methodParameters = commandRule.Method.GetParameters();

            //var stringToObject = new StringToObject(new ArrayParser());

            for (int i = 0; i < commandRule.Command.RequiredParameters.Count; i++)
            {
                parameterValues.Add(_stringToObject.ConvertValue(commandRule.Command.RequiredParameters[i].Value, methodParameters[i].ParameterType));
            }
            for (int i = 0; i < commandRule.Command.OptionalParameters.Count; i++)
            {
                parameterValues.Add(_stringToObject.ConvertValue(commandRule.Command.OptionalParameters[i].Value, methodParameters[i + commandRule.Command.RequiredParameters.Count].ParameterType));
            }
            return parameterValues.ToArray();
        }
    }
}