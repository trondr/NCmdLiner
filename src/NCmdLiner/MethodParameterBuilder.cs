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

        public Result<object[]> BuildMethodParameters(CommandRule commandRule)
        {
            if (!commandRule.IsValid)
            {
                throw new CommandRuleNotValidatedExption("Unable to build parameter array before command rule has been validated.");
            }
            if (commandRule.Method == null) throw new NullReferenceException("Method property has not been initialiazed.");

            var parameterValues = new List<object>();
            var methodParameters = commandRule.Method.GetParameters();

            for (var i = 0; i < commandRule.Command.RequiredParameters.Count; i++)
            {
                var valueResult = _stringToObject.ConvertValue(commandRule.Command.RequiredParameters[i].Value,
                    methodParameters[i].ParameterType);
                if (valueResult.IsFailure)
                    return Result.Fail<object[]>(valueResult.Exception);
                parameterValues.Add(valueResult.Value.Value);
            }
            for (var i = 0; i < commandRule.Command.OptionalParameters.Count; i++)
            {
                var valueResult = _stringToObject.ConvertValue(commandRule.Command.OptionalParameters[i].Value,
                    methodParameters[i + commandRule.Command.RequiredParameters.Count].ParameterType);
                if (valueResult.IsFailure)
                    return Result.Fail<object[]>(valueResult.Exception);
                parameterValues.Add(valueResult.Value.HasNoValue ? null : valueResult.Value.Value);
            }
            return Result.Ok(parameterValues.ToArray());
        }
    }
}