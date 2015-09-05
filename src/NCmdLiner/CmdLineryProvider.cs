using System;
using System.Collections.Generic;
using System.Reflection;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public class CmdLineryProvider : ICmdLineryProvider
    {
        private readonly Func<IHelpProvider> _helpProviderFactory;
        private readonly Func<IApplicationInfo> _applicationInfoFactory;

        public CmdLineryProvider(Func<IHelpProvider> helpProviderFactory, Func<IApplicationInfo> applicationInfoFactory)
        {
            _helpProviderFactory = helpProviderFactory;
            _applicationInfoFactory = applicationInfoFactory;
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
            commandRule.Validate(args);
            object[] parameterArrray = commandRule.BuildMethodParameters();
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
                    MethodInfo prepForRemoting = typeof(Exception).GetMethod("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (prepForRemoting != null)
                    {
                        //Preserve stack trace before re-throwing inner exception
                        prepForRemoting.Invoke(ex.InnerException, new object[0]);
                        throw ex.InnerException;
                    }
                }
                throw;
            }
        }
    }
}