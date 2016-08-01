// File: CmdLinery.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NCmdLiner.Attributes;
using NCmdLiner.Exceptions;
using TinyIoC;

namespace NCmdLiner
{
    public class CmdLinery
    {
        //private static readonly TinyIoCContainer Container;

        static CmdLinery()
        {
            //Container = new TinyIoCContainer();
            //container.AutoRegister(new[] { container.GetType().GetAssembly() });
        }

        #region Run from TargetType

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args)
        {
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            using (var container = GetContainer())
            {
                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }


        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo)
        {
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);

                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }


        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IMessenger messenger)
        {
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");
            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);

                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
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
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);

                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }


        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IHelpProvider>(helpProvider);

                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }


        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo,
            IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IHelpProvider>(helpProvider);

                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }


        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);

                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }


        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on the target type. </summary>
        /// 
        ///  <param name="targetType">   A class with one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type targetType, string[] args, IApplicationInfo applicationInfo, IMessenger messenger,
            IHelpProvider helpProvider)
        {
            if (targetType == null) throw new ArgumentNullException("targetType");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);

                return Run(new[] {targetType}, args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        #endregion

        #region Run from Assembly

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (args == null) throw new ArgumentNullException("args");
            using (var container = GetContainer())
            {
                return Run(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);

                return Run(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IMessenger messenger)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");
            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);

                return Run(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
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
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);

                return Run(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (args == null) throw new ArgumentNullException("args");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IHelpProvider>(helpProvider);

                return Run(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo,
            IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IHelpProvider>(helpProvider);

                return Run(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);

                return Run(assembly, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        /// <summary>Run command specified on the command line. The command is implemented by a static method on one of the target types in the specified assembly. </summary>
        ///
        /// <param name="assembly">An assembly with one or more classes decorated with the [Commands] attribute having one or more static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Assembly assembly, string[] args, IApplicationInfo applicationInfo, IMessenger messenger,
            IHelpProvider helpProvider)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);

                var targetTypes = GetTargetTypesFromAssembly(assembly);
                return Run(targetTypes.ToArray(), args, container.Resolve<IApplicationInfo>(),
                    container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        #endregion

        #region Run from TargetTypes

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            using (var container = GetContainer())
            {
                return Run(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);

                return Run(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IMessenger messenger)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");
            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);

                return Run(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);

                return Run(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IHelpProvider>(helpProvider);

                return Run(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo,
            IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IHelpProvider>(helpProvider);

                return Run(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");

            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);

                return Run(targetTypes, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetTypes">   An array of classes, each with one or more static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        ///  <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(Type[] targetTypes, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetTypes == null) throw new ArgumentNullException("targetTypes");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");
            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);

                var commandRuleProvider = container.Resolve<ICommandRuleProvider>();
                var cmdLineryProvider = container.Resolve<ICmdLineryProvider>();
                var commandRules = commandRuleProvider.GetCommandRules(targetTypes);
                return cmdLineryProvider.Run(commandRules, args);
            }
        }

        #endregion

        #region Run from targetObjects

        /// <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        ///
        /// <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///  <param name="args">         The command line arguments. </param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            using (var container = GetContainer())
            {
                return Run(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");

            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);

                return Run(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args, IMessenger messenger)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");

            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);

                return Run(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IMessenger messenger)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");

            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);

                return Run(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");

            using (var container = GetContainer())
            {
                container.Register<IHelpProvider>(helpProvider);

                return Run(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");

            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IHelpProvider>(helpProvider);

                return Run(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(),
                    container.Resolve<IHelpProvider>());
            }
        }

        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");

            using (var container = GetContainer())
            {
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);
                return Run(targetObjects, args, container.Resolve<IApplicationInfo>(), container.Resolve<IMessenger>(), container.Resolve<IHelpProvider>());
            }
        }
        
        ///  <summary>   Run command specified on the command line. The command is implemented by a static method on one of the target types. </summary>
        /// 
        ///  <param name="targetObjects">   An array of object instances, each with one or more static or non static methods decorated with the [Command] attribute. </param>
        ///   <param name="args">         The command line arguments. </param>
        ///  <param name="applicationInfo">A modified applicaton info object for customization of the help output.</param>
        ///  <param name="messenger">An alternative messenger for display of the help text. The default is to display the help text to the console.</param>
        /// <param name="helpProvider">An alternative help provider. The default help provider produce formated text.</param>
        /// <returns> The user defined return code. Typically 0 means success. </returns>
        public static int Run(object[] targetObjects, string[] args, IApplicationInfo applicationInfo, IMessenger messenger, IHelpProvider helpProvider)
        {
            if (targetObjects == null) throw new ArgumentNullException("targetObjects");
            if (args == null) throw new ArgumentNullException("args");
            if (applicationInfo == null) throw new ArgumentNullException("applicationInfo");
            if (messenger == null) throw new ArgumentNullException("messenger");
            if (helpProvider == null) throw new ArgumentNullException("helpProvider");

            using (var container = GetContainer())
            {
                container.Register<IApplicationInfo>(applicationInfo);
                container.Register<IMessenger>(messenger);
                container.Register<IHelpProvider>(helpProvider);

                var commandRuleProvider = container.Resolve<ICommandRuleProvider>();
                var cmdLineryProvider = container.Resolve<ICmdLineryProvider>();

                var commandRules = commandRuleProvider.GetCommandRules(targetObjects);
                return cmdLineryProvider.Run(commandRules, args);
            }
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