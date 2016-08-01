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

        public int Run(List<CommandRule> commandRules, string[] args)
        {
            var applicationInfo = _applicationInfoFactory.Invoke();
            var helpProvider = _helpProviderFactory.Invoke();
            if (args.Length == 0)
            {
                helpProvider.ShowHelp(commandRules, null, applicationInfo);
                throw new MissingCommandException("Command not specified.");
            }
            string commandName = args[0];
            if (helpProvider.IsHelpRequested(commandName))
            {
                CommandRule helpForCommandRule = null;
                if (args.Length > 1)
                {
                    string helpForCommandName = args[1];
                    helpForCommandRule = commandRules.Find(rule => rule.Command.Name == helpForCommandName);
                }
                helpProvider.ShowHelp(commandRules, helpForCommandRule, applicationInfo);
                return 0;
            }
            if (helpProvider.IsLicenseRequested(commandName))
            {
                helpProvider.ShowLicense(applicationInfo);
                return 0;
            }
            if (helpProvider.IsCreditsRequested(commandName))
            {
                helpProvider.ShowCredits(applicationInfo);
                return 0;
            }

            CommandRule commandRule = commandRules.Find(rule => rule.Command.Name == commandName);
            if (commandRule == null) throw new UnknownCommandException("Unknown command: " + commandName);
            _commandRuleValidator.Validate(args, commandRule);
            object[] parameterArrray =  _methodParameterBuilder.BuildMethodParameters(commandRule);
            try
            {
                object returnValue = commandRule.Method.Invoke(commandRule.Instance, parameterArrray);
                if (returnValue is int)
                {
                    return (int)returnValue;
                }
                return 0;
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    var innerException = ex.InnerException;
                    //var preparedException = ex.InnerException.PrepForRemoting();
                    //throw preparedException;
                    //MethodInfo prepForRemoting = typeof(Exception).GetMethodEx("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance);
                    //if (prepForRemoting != null)
                    //{
                    //    //Preserve stack trace before re-throwing inner exception
                    //    prepForRemoting.Invoke(ex.InnerException, new object[0]);                    
                    //    throw ex.InnerException;
                    //}
                    innerException.PrepForRemotingAndThrow();
                    //var prepForRemoting = typeof(Exception).GetMethodEx("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance);
                    //if (prepForRemoting != null)
                    //{
                    //    prepForRemoting.Invoke(innerException, new object[0]);
                    //    throw innerException;
                    //}
                    //else
                    //{
                    //    var exceptionDispatchInfo = ExceptionDispatchInfo.Capture(innerException);
                    //    exceptionDispatchInfo.Throw();
                    //}
                }
                throw;
            }
        }
    }
}