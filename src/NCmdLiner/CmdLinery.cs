// File: CmdLinery.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Reflection;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public class CmdLinery
    {
        public static void Run(Type targetType, string[] args)
        {
            Run(targetType,args,new ApplicationInfo(), new ConsoleMessenger());
        }

        public static void Run(Type targetType, string[] args, IApplicationInfo applicationInfo)
        {
            Run(targetType, args, applicationInfo, new ConsoleMessenger());
        }

        public static void Run(Type targetType, string[] args, IMessenger messenger)
        {
            Run(targetType, args, new ApplicationInfo(), messenger);
        }

        public static void Run(Type targetType, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            CommandRuleProvider commandRuleProvider = new CommandRuleProvider();
            List<CommandRule> commandRules = commandRuleProvider.GetCommandRules(targetType);
            HelpProvider helpProvider = new HelpProvider(messenger);
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
                return;
            }
            if (helpProvider.IsLicenseRequested(commandName))
            {
                helpProvider.ShowLicense(applicationInfo);                
                return;
            }
            if (helpProvider.IsCreditsRequested(commandName))
            {
                helpProvider.ShowCredits(applicationInfo);                
                return;
            }

            CommandRule commandRule = commandRules.Find(rule => rule.Command.Name == commandName);
            if (commandRule == null) throw new UnknownCommandException("Unknown command: " + commandName);
            commandRule.Validate(args);
            object[] parameterArrray = commandRule.BuildMethodParameters();
            try
            {
                commandRule.Method.Invoke(null, parameterArrray);
            }
            catch (TargetInvocationException ex)
            {
                if (ex.InnerException != null)
                {
                    throw ex.InnerException;
                }
                throw;
            }
        }
    }
}