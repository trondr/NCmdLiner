using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using LanguageExt.Common;
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

        public async Task<Result<int>> Run(List<CommandRule> commandRules, string[] args)
        {
            var applicationInfo = _applicationInfoFactory.Invoke();
            var helpProvider = _helpProviderFactory.Invoke();
            if (args.Length == 0)
            {
                helpProvider.ShowHelp(commandRules, null, applicationInfo);
                return new Result<int>(new MissingCommandException("Command not specified."));
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
                return new Result<int>(0);
            }
            if (helpProvider.IsLicenseRequested(commandName))
            {
                helpProvider.ShowLicense(applicationInfo);
                return new Result<int>(0);
            }
            if (helpProvider.IsCreditsRequested(commandName))
            {
                helpProvider.ShowCredits(applicationInfo);
                return new Result<int>(0);
            }

            var commandRule = commandRules.Find(rule => rule.Command.Name == commandName);
            if (commandRule == null)
                return new Result<int>(new UnknownCommandException("Unknown command: " + commandName));
            var validateResult = _commandRuleValidator.Validate(args, commandRule);
            if (validateResult.IsFaulted)
                return validateResult;
            
            var parameterArrayResult = _methodParameterBuilder.BuildMethodParameters(commandRule);
            if (parameterArrayResult.IsFaulted)
                return new Result<int>(parameterArrayResult.ToException());
            try
            {
                var returnValue = commandRule.Method.Invoke(commandRule.Instance, parameterArrayResult.ToValue());
                if (returnValue is Task<int> task)
                {
                    return await task;
                }
                if (returnValue is Task<Result<int>> taskResult)
                {
                    return await taskResult;
                }
                if (returnValue is Result<int> result)
                {
                    return result;
                }
                if (returnValue is int i)
                {
                    return new Result<int>(i);
                }
                return new Result<int>(0);
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
                        return new Result<int>(e);
                    }
                }
                return new Result<int>(ex);
            }
        }
    }
}