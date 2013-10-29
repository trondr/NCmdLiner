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
using System.Runtime.InteropServices;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public class CmdLinery
    {
        /// <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        ///
        /// <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        /// <param name="args">         The command line arguments. </param>
        ///
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args)
        {
            return Run(new[] { targetType }, args, new ApplicationInfo(), new ConsoleMessenger());
        }

        /// <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        ///
        /// <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>

        /// <param name="args">         The command line arguments. </param>
        ///
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args)
        {
            return Run(targetTypes, args, new ApplicationInfo(), new ConsoleMessenger());
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo)
        {
            return Run(new[] { targetType }, args, applicationInfo, new ConsoleMessenger());
        }

        /// <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        ///
        /// <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo)
        {
            return Run(targetTypes, args, applicationInfo, new ConsoleMessenger());
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IMessenger messenger)
        {
            return Run(new[]{targetType}, args, new ApplicationInfo(), messenger);
        }

        /// <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        ///
        /// <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IMessenger messenger)
        {
            return Run(targetTypes, args, new ApplicationInfo(), messenger);
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            return Run(new[] { targetType }, args, new ApplicationInfo(), messenger);
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        /// <param name="args">         The command line arguments. </param>
        ///
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args)
        {
            return Run(assembly, args, new ApplicationInfo(), new ConsoleMessenger());
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>        
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo)
        {
            return Run(assembly, args, applicationInfo, new ConsoleMessenger());
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>        
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IMessenger messenger)
        {
            return Run(assembly, args, new ApplicationInfo(), messenger);
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            var targetTypes = GetTargetTypesFromAssembly(assembly);
            return Run(targetTypes.ToArray(), args, applicationInfo, messenger);
        }

        /// <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        ///
        /// <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            CommandRuleProvider commandRuleProvider = new CommandRuleProvider();
            List<CommandRule> commandRules = commandRuleProvider.GetCommandRules(targetTypes);
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
                object returnValue = commandRule.Method.Invoke(null, parameterArrray);
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

        #region Privat methods
        private static List<Type> GetTargetTypesFromAssembly(Assembly assembly)
        {
            List<Type> targetTypes = new List<Type>();
            foreach (Type type in assembly.GetTypes())
            {
                object[] attributes = type.GetCustomAttributes(typeof(CommandsAttribute), true);
                if (attributes.Length == 1)
                    targetTypes.Add(type);
            }
            return targetTypes;
        }
        #endregion
    }
}