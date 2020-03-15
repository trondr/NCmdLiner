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
using NCmdLiner.Attributes;
using TinyIoC;
using System.Linq;

namespace NCmdLiner
{
    public class CmdLinery
    {
        #region Run from TargetType
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            using (var container = GetContainer())
            {
                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type targetType, string[] args)
        {
            var exitCode = 0;
            RunEx(targetType, args)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args, IApplicationInfo applicationInfo)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);

                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo)
        {
            var exitCode = 0;
            RunEx(targetType, args, applicationInfo)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args, IMessenger messenger)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            using (var container = GetContainer())
            {
                container.Register(messenger);

                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type targetType, string[] args, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(targetType, args, messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);

                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args, IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(helpProvider);

                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(targetType, args, applicationInfo, messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args, IApplicationInfo applicationInfo,IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(helpProvider);

                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetType, args, applicationInfo, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(messenger);
                container.Register(helpProvider);

                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type targetType, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetType, args, messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type targetType, string[] args, IApplicationInfo applicationInfo, IMessenger messenger,
            IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException(nameof(targetType));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);
                container.Register(helpProvider);

                return RunEx(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>        
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetType, args, applicationInfo, messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        #endregion

        #region Run from Assembly
        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            using (var container = GetContainer())
            {
                return RunEx(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args)
        {
            var exitCode = 0;
            RunEx(assembly, args)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args, IApplicationInfo applicationInfo)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);

                return RunEx(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo)
        {
            var exitCode = 0;
            RunEx(assembly, args, applicationInfo)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args, IMessenger messenger)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            using (var container = GetContainer())
            {
                container.Register(messenger);

                return RunEx(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(assembly, args, messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);

                return RunEx(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(assembly, args, applicationInfo, messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args, IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(helpProvider);

                return RunEx(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(assembly, args, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args, IApplicationInfo applicationInfo,
            IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(helpProvider);

                return RunEx(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo,IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(assembly, args, applicationInfo, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(messenger);
                container.Register(helpProvider);

                return RunEx(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(assembly, args, messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Assembly assembly, string[] args, IApplicationInfo applicationInfo, IMessenger messenger,
            IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);
                container.Register(helpProvider);

                var targetTypes = GetTargetTypesFromAssembly(assembly);
                return RunEx(targetTypes.ToArray(), args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>        
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(assembly, args, applicationInfo, messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        #endregion

        #region Run from TargetTypes
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            using (var container = GetContainer())
            {
                return RunEx(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args)
        {
            var exitCode = 0;
            RunEx(targetTypes, args)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);

                return RunEx(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo)
        {
            var exitCode = 0;
            RunEx(targetTypes, args, applicationInfo)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args, IMessenger messenger)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            using (var container = GetContainer())
            {
                container.Register(messenger);

                return RunEx(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(targetTypes, args,  messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);

                return RunEx(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(targetTypes, args, applicationInfo, messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args, IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(helpProvider);

                return RunEx(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetTypes, args, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(helpProvider);

                return RunEx(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetTypes, args, applicationInfo, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));

            using (var container = GetContainer())
            {
                container.Register(messenger);
                container.Register(helpProvider);

                return RunEx(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetTypes, args, messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException(nameof(targetTypes));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));
            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);
                container.Register(helpProvider);

                var commandRuleProvider = container.Resolve<ICommandRuleProvider>();
                var cmdLineryProvider = container.Resolve<ICmdLineryProvider>();
                var commandRulesResult = commandRuleProvider.GetCommandRules(targetTypes);
                if (commandRulesResult.IsFailure)
                    return Result.Fail<int>(commandRulesResult.Exception);
                return cmdLineryProvider.Run(commandRulesResult.Value, args);
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetTypes, args, applicationInfo, messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        #endregion

        #region Run from targetObjects
        /// <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        ///
        /// <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            using (var container = GetContainer())
            {
                return RunEx(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args)
        {
            var exitCode = 0;
            RunEx(targetObjects, args)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args, IApplicationInfo applicationInfo)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));

            using (var container = GetContainer())
            {
                container.Register(applicationInfo);

                return RunEx(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo)
        {
            var exitCode = 0;
            RunEx(targetObjects, args, applicationInfo)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args, IMessenger messenger)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));

            using (var container = GetContainer())
            {
                container.Register(messenger);

                return RunEx(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(targetObjects, args, messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));

            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);

                return RunEx(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            var exitCode = 0;
            RunEx(targetObjects, args, applicationInfo, messenger)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));

            using (var container = GetContainer())
            {
                container.Register(helpProvider);

                return RunEx(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetObjects, args, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));

            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(helpProvider);

                return RunEx(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetObjects, args, applicationInfo, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));

            using (var container = GetContainer())
            {
                container.Register(messenger);
                container.Register(helpProvider);
                return RunEx(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetObjects, args, messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
            return exitCode;
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> Result with the user defined return code. Typically 0 means success. In case of failure the result contains the exception information </returns>
        public static Result<int> RunEx(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException(nameof(targetObjects));
            if (args == null) throw new ArgumentNullException(nameof(args));
            if (applicationInfo == null) throw new ArgumentNullException(nameof(applicationInfo));
            if (messenger == null) throw new ArgumentNullException(nameof(messenger));
            if (helpProvider == null) throw new ArgumentNullException(nameof(helpProvider));

            using (var container = GetContainer())
            {
                container.Register(applicationInfo);
                container.Register(messenger);
                container.Register(helpProvider);

                var commandRuleProvider = container.Resolve<ICommandRuleProvider>();
                var cmdLineryProvider = container.Resolve<ICmdLineryProvider>();
                var commandRulesResult = commandRuleProvider.GetCommandRules(targetObjects);
                if (commandRulesResult.IsFailure)
                    return Result.Fail<int>(commandRulesResult.Exception);
                return cmdLineryProvider.Run(commandRulesResult.Value, args);
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>        
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        [Obsolete("Run method is deprecated, please use RunEx method instead.")]
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            var exitCode = 0;
            RunEx(targetObjects, args, applicationInfo,messenger, helpProvider)
                .OnSuccess(i => exitCode = i)
                .OnFailure(exception => throw exception);
                return exitCode;
        }
        
        private static TinyIoCContainer GetContainer()
        {
            var container = new TinyIoCContainer();
            var thisAssembly = typeof(CmdLinery).GetAssembly();
            container.AutoRegister(new[] { thisAssembly });
            return container;
        }

        #endregion

        #region Privat methods

        private static List<Type> GetTargetTypesFromAssembly(Assembly assembly)
        {
            var targetTypes = new List<Type>();
            foreach (var type in assembly.GetTypes())
            {
                var attributes = type.GetCustomAttributes(typeof(CommandsAttribute), true);
                if (attributes.Count() == 1)
                    targetTypes.Add(type);
            }
            return targetTypes;
        }

        #endregion
    }
}