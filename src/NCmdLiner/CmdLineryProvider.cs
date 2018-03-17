using System;
using System.Collections.Generic;
using System.Reflection;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    internal class CmdLineryProvider : ICmdLineryProvider
    {
        private readonly Func<IHelpProvider> _helpProviderFactory;
        private readonly Func<IApplicationInfo> _applicationInfoFactory;
        private readonly IMethodParameterBuilder _methodParameterBuilder;
        private readonly ICommandRuleValidator _commandRuleValidator;

        public CmdLineryProvider(Func<IHelpProvider> helpProviderFactory, Func<IApplicationInfo> applicationInfoFactory, IMethodParameterBuilder methodParameterBuilder, ICommandRuleValidator commandRuleValidator)
        {
            _helpProviderFactory = helpProviderFactory;
            _applicationInfoFactory = applicationInfoFactory;
            _methodParameterBuilder = methodParameterBuilder;
            _commandRuleValidator = commandRuleValidator;
        }

        public Result<int> Run(List<CommandRule> commandRules, string[] args)
        {
            var applicationInfo = _applicationInfoFactory.Invoke();
            var helpProvider = _helpProviderFactory.Invoke();
            if (args.Length == 0)
            {
                helpProvider.ShowHelp(commandRules, null, applicationInfo);
                return Result.Fail<int>(new MissingCommandException("Command not specified."));
            }
            var commandName = args[0];
            if (helpProvider.IsHelpRequested(commandName))
            {
                CommandRule helpForCommandRule = null;
                if (args.Length > 1)
                {
                    var helpForCommandName = args[1];
                    helpForCommandRule = commandRules.Find(rule => rule.Command.Name == helpForCommandName);
                }
                helpProvider.ShowHelp(commandRules, helpForCommandRule, applicationInfo);
                return Result.Ok(0);
            }
            if (helpProvider.IsLicenseRequested(commandName))
            {
                helpProvider.ShowLicense(applicationInfo);
                return Result.Ok(0);
            }
            if (helpProvider.IsCreditsRequested(commandName))
            {
                helpProvider.ShowCredits(applicationInfo);
                return Result.Ok(0);
            }

            var commandRule = commandRules.Find(rule => rule.Command.Name == commandName);
            if (commandRule == null)
                return Result.Fail<int>(new UnknownCommandException("Unknown command: " + commandName));
            var validateResult = _commandRuleValidator.Validate(args, commandRule);
            if (validateResult.IsFailure)
                return validateResult;
            
            var parameterArrrayResult = _methodParameterBuilder.BuildMethodParameters(commandRule);
            if (parameterArrrayResult.IsFailure)
                return Result.Fail<int>(parameterArrrayResult.Exception);
            try
            {
                var returnValue = commandRule.Method.Invoke(commandRule.Instance, parameterArrrayResult.Value);
                if (returnValue is Result<int> result)
                {
                    return result;
                }
                if (returnValue is int i)
                {
                    return Result.Ok(i);
                }
                return Result.Ok(0);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    var innerException = ex.InnerException;
                    try
                    {
                        innerException.PrepForRemotingAndThrow();
                    }
                    catch (Exception e)
                    {
                        return Result.Fail<int>(e);
                    }
                }
                return Result.Fail<int>(ex);
            }
        }
    }
}