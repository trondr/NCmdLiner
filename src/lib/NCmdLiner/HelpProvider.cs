// File: HelpProvider.cs
// Project Name: NCmdLiner
// Project Home: https://github.com/trondr/NCmdLiner/blob/master/README.md
// License: New BSD License (BSD) https://github.com/trondr/NCmdLiner/blob/master/License.md
// Credits: See the Credit folder in this project
// Copyright © <github.com/trondr> 2013 
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using NCmdLiner.Credit;
using NCmdLiner.License;

namespace NCmdLiner
{
    public class HelpProvider : IHelpProvider
    {
        private readonly Func<IMessenger> _messengerFactory;
        private int _commandColumnWidth;
        // ReSharper disable once UnusedMember.Local
        private HelpProvider() { }
        public HelpProvider(Func<IMessenger> messengerFactory)
        {
            _messengerFactory = messengerFactory;
        }

        private const int MaxWidth = 79;

        /// <summary>   Query if help is requested./ </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandName">  Name of the command. </param>
        ///
        /// <returns>   true if help is requested, false if not. </returns>
        public bool IsHelpRequested(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                return true;
            }
            switch (commandName.ToLower())
            {
                case "help":
                case "/help":
                case "-help":
                case "--help":
                case "/h":
                case "-h":
                case "/?":
                case "-?":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>   Query if license is requested. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandName">  Name of the command. </param>
        ///
        /// <returns>   true if license is requested, false if not. </returns>
        public bool IsLicenseRequested(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "license":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>   Query if credits are requested. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandName">  Name of the command. </param>
        ///
        /// <returns>   true if credits are requested, false if not. </returns>
        public bool IsCreditsRequested(string commandName)
        {
            switch (commandName.ToLower())
            {
                case "credits":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>   Shows help. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandRules">         The command rules. </param>
        /// <param name="helpForCommandRule">   The help for command rule. </param>
        /// <param name="applicationInfo">      Information describing the application. </param>
        public void ShowHelp(List<CommandRule> commandRules, CommandRule helpForCommandRule,
                             IApplicationInfo applicationInfo)
        {
            var messenger = _messengerFactory.Invoke();
            messenger.WriteLine("{0} {1} - {2}", applicationInfo.Name, applicationInfo.Version,
                              applicationInfo.Description);
            messenger.WriteLine("{0}", applicationInfo.Copyright);
            if (!string.IsNullOrEmpty(applicationInfo.Authors))
            {
                messenger.WriteLine(applicationInfo.Authors.Contains(",") ? "Authors: {0}" : "Author: {0}", applicationInfo.Authors);
            }
            messenger.WriteLine("Usage: {0} <command> [parameters]", applicationInfo.ExeFileName);
            messenger.WriteLine(string.Empty);
            _commandColumnWidth = CalculateCommandColumnWidth(commandRules);
            if (helpForCommandRule != null)
            {
                ShowCommandRuleHelp(helpForCommandRule, true, applicationInfo);
            }
            else
            {
                messenger.WriteLine("Commands:");
                messenger.WriteLine("---------");

                ShowCommandRuleHelp(
                    new CommandRule { Command = new Command { Description = "Display this help text", Name = "Help" } },
                    false, applicationInfo);
                ShowCommandRuleHelp(
                    new CommandRule { Command = new Command { Description = "Display license", Name = "License" } }, false,
                    applicationInfo);
                ShowCommandRuleHelp(
                    new CommandRule { Command = new Command { Description = "Display credits", Name = "Credits" } }, false,
                    applicationInfo);

                foreach (var commandRule in commandRules)
                {
                    ShowCommandRuleHelp(commandRule, false, applicationInfo);
                }
                messenger.WriteLine(string.Empty);
                messenger.WriteLine("Commands and parameters:");
                messenger.WriteLine("------------------------");
                foreach (var commandRule in commandRules)
                {
                    ShowCommandRuleHelp(commandRule, true, applicationInfo);
                }
                //_messenger.WriteLine();
            }
            messenger.Show();
        }

        /// <summary>   Shows credits. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="applicationInfo">  Information describing the application. </param>
        public void ShowCredits(IApplicationInfo applicationInfo)
        {
            var messenger = _messengerFactory.Invoke();
            messenger.WriteLine("{0} {1} - {2}", applicationInfo.Name, applicationInfo.Version,
                              applicationInfo.Description);
            messenger.WriteLine("{0}", applicationInfo.Copyright);
            messenger.WriteLine("-------------------------------------------------------------------------------");
            messenger.WriteLine(BuildCreditsText());
            messenger.WriteLine("-------------------------------------------------------------------------------");
            messenger.Show();
        }

        /// <summary>   Shows license. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="applicationInfo">  Information describing the application. </param>
        public void ShowLicense(IApplicationInfo applicationInfo)
        {
            var messenger = _messengerFactory.Invoke();
            messenger.WriteLine("{0} {1} - {2}", applicationInfo.Name, applicationInfo.Version,
                              applicationInfo.Description);
            messenger.WriteLine("{0}", applicationInfo.Copyright);

            messenger.WriteLine("-------------------------------------------------------------------------------");
            messenger.WriteLine(BuildCreditsText());
            messenger.WriteLine("-------------------------------------------------------------------------------");
            messenger.WriteLine(BuildLicenseText());
            messenger.WriteLine("-------------------------------------------------------------------------------");
            messenger.Show();
        }

        #region Private methods

        /// <summary>   Builds credits text. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <returns>   . </returns>
        private static string BuildCreditsText()
        {
            var creditsText = new StringBuilder();
            creditsText.Append("Credits:" + Environment.NewLine);
            creditsText.Append(
                "-------------------------------------------------------------------------------" +
                Environment.NewLine);
            ICreditProvider creditProvider = new CreditProvider();
            var creditInfos = creditProvider.GetCredits(Assembly.GetEntryAssembly());
            var referencedAssemblies = typeof(HelpProvider).GetAssembly().GetReferencedAssemblies();
            foreach (var referencedAssembly in referencedAssemblies)
            {
                var referencedCreditInfos = creditProvider.GetCredits(Assembly.Load(referencedAssembly));
                creditInfos.AddRange(referencedCreditInfos);
            }
            foreach (var creditInfo in creditInfos)
            {
                if (!string.IsNullOrEmpty(creditInfo.CreditText))
                {
                    creditsText.Append($"  (*) For use of {creditInfo.ProductName} ({creditInfo.ProductHome}) : {creditInfo.CreditText}" + Environment.NewLine);
                }
            }
            return creditsText.ToString();
        }

        /// <summary>   Builds license text. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <returns>   . </returns>
        private string BuildLicenseText()
        {
            var licenseText = new StringBuilder();
            var messenger = _messengerFactory.Invoke();
            messenger.WriteLine("License summary:");
            licenseText.Append(
                "-------------------------------------------------------------------------------" +
                Environment.NewLine);
            ILicenseProvider licenseProvider = new LicenseProvider();
            var uniqueLicenseInfos = new OrderedDictionary();

            var licenseInfos = licenseProvider.GetLicenses(Assembly.GetEntryAssembly());
            foreach (var licenseInfo in licenseInfos)
            {
                if (!uniqueLicenseInfos.Contains(licenseInfo.ProductName))
                {
                    uniqueLicenseInfos.Add(licenseInfo.ProductName, licenseInfo);
                }
            }

            var referencedAssemblies = typeof(HelpProvider).GetAssembly().GetReferencedAssemblies();
            foreach (var referencedAssembly in referencedAssemblies)
            {
                IList<ILicenseInfo> referencedLicenseInfos =
                    licenseProvider.GetLicenses(Assembly.Load(referencedAssembly));
                foreach (var licenseInfo in referencedLicenseInfos)
                {
                    if (!uniqueLicenseInfos.Contains(licenseInfo.ProductName))
                    {
                        uniqueLicenseInfos.Add(licenseInfo.ProductName, licenseInfo);
                    }
                }
            }
            foreach (ILicenseInfo licenseInfo in uniqueLicenseInfos.Values)
            {
                licenseText.Append($"  (*) {licenseInfo.ProductName}, {licenseInfo.ProductHome}, {licenseInfo.License}" + Environment.NewLine);
            }
            licenseText.Append(
                "-------------------------------------------------------------------------------" +
                Environment.NewLine);
            licenseText.Append("License details:" + Environment.NewLine);
            licenseText.Append(
                "-------------------------------------------------------------------------------" +
                Environment.NewLine);
            var count = 0;
            foreach (ILicenseInfo licenseInfo in uniqueLicenseInfos.Values)
            {
                count++;
                licenseText.Append($"Product: {licenseInfo.ProductName}" + Environment.NewLine);
                licenseText.Append($"Home: {licenseInfo.ProductHome}" + Environment.NewLine);
                licenseText.Append($"License: {licenseInfo.License}" + Environment.NewLine);
                licenseText.Append($"{licenseInfo.LicenseText}" + Environment.NewLine);
                if (count < uniqueLicenseInfos.Values.Count)
                {
                    licenseText.Append(
                        "-------------------------------------------------------------------------------" +
                        Environment.NewLine);
                }
            }
            return licenseText.ToString();
        }

        /// <summary>   Calculates the command column width. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandRules"> The command rules. </param>
        ///
        /// <returns>   The calculated command column width. </returns>
        private int CalculateCommandColumnWidth(List<CommandRule> commandRules)
        {
            var maxCommandNameLength = 0;
            foreach (var commandRule in commandRules)
            {
                if (commandRule.Command.Name.Length > maxCommandNameLength)
                {
                    maxCommandNameLength = commandRule.Command.Name.Length;
                }
                foreach (var requiredCommandParameter in commandRule.Command.RequiredParameters)
                {
                    if (requiredCommandParameter.Name.Length > maxCommandNameLength)
                    {
                        maxCommandNameLength = requiredCommandParameter.Name.Length;
                    }
                }
                foreach (var optionalCommandParameter in commandRule.Command.OptionalParameters)
                {
                    if (optionalCommandParameter.Name.Length > maxCommandNameLength)
                    {
                        maxCommandNameLength = optionalCommandParameter.Name.Length;
                    }
                }
            }
            return maxCommandNameLength + 3;
        }

        /// <summary>   Shows the command rule help. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandRule">          The command rule. </param>
        /// <param name="includeParameters">    true to include, false to exclude the parameters. </param>
        /// <param name="applicationInfo">      Information describing the application. </param>
        private void ShowCommandRuleHelp(CommandRule commandRule, bool includeParameters, IApplicationInfo applicationInfo)
        {
            IValueConverter valueConverter = new ValueConverter();
            var helpString = new StringBuilder();
            var exampleString = new StringBuilder();
            var alternativeExampleString = new StringBuilder();
            helpString.Append(FormatCommand(commandRule.Command.Name));
            if(!includeParameters)
                helpString.Append(FormatCommandDescription(commandRule.Command.Summary, _commandColumnWidth, MaxWidth - _commandColumnWidth));
            if (includeParameters)
            {
                helpString.Append(FormatCommandDescription(commandRule.Command.Description, _commandColumnWidth, MaxWidth - _commandColumnWidth));
                if (Type.GetType("Mono.Runtime") != null)
                    exampleString.Append("mono ");
                exampleString.Append(applicationInfo.ExeFileName + " ");
                exampleString.Append(commandRule.Command.Name + " ");
                alternativeExampleString.Append(exampleString);
                foreach (var requiredCommandParameter in commandRule.Command.RequiredParameters)
                {
                    helpString.Append(FormatCommandParameter("/" + requiredCommandParameter.Name));
                    var commandParameterDescription = GetRequiredCommandParameterDescription(requiredCommandParameter.Description, requiredCommandParameter.AlternativeName);
                    helpString.Append(commandParameterDescription);
                    exampleString.Append($"/{requiredCommandParameter.Name}=\"{valueConverter.ObjectValue2String(requiredCommandParameter.ExampleValue)}\" ");
                    alternativeExampleString.Append(!string.IsNullOrEmpty(requiredCommandParameter.AlternativeName)
                        ? $"/{requiredCommandParameter.AlternativeName}=\"{valueConverter.ObjectValue2String(requiredCommandParameter.ExampleValue)}\" "
                        : $"/{requiredCommandParameter.Name}=\"{valueConverter.ObjectValue2String(requiredCommandParameter.ExampleValue)}\" ");
                }
                foreach (var optionalCommandParameter in commandRule.Command.OptionalParameters)
                {
                    helpString.Append(FormatCommandParameter("/" + optionalCommandParameter.Name));
                    var optionalCommandParameterDescription = GetOptionalCommandParameterDescription(optionalCommandParameter.Description, optionalCommandParameter.AlternativeName, valueConverter.ObjectValue2String(optionalCommandParameter.DefaultValue));
                    helpString.Append(optionalCommandParameterDescription);
                    exampleString.Append(
                        $"/{optionalCommandParameter.Name}=\"{valueConverter.ObjectValue2String(optionalCommandParameter.ExampleValue)}\" ");
                    alternativeExampleString.Append(
                        !string.IsNullOrEmpty(optionalCommandParameter.AlternativeName)
                            ? $"/{optionalCommandParameter.AlternativeName}=\"{valueConverter.ObjectValue2String(optionalCommandParameter.ExampleValue)}\" "
                            : $"/{optionalCommandParameter.Name}=\"{valueConverter.ObjectValue2String(optionalCommandParameter.ExampleValue)}\" ");
                }
                helpString.Append(Environment.NewLine);
                helpString.Append("".PadLeft(3) + "Example: " + exampleString + Environment.NewLine);
                if (exampleString.ToString() != alternativeExampleString.ToString())
                {
                    helpString.Append("".PadLeft(3) + "Example (alternative): " + alternativeExampleString + Environment.NewLine);
                }
                
                helpString.Append(Environment.NewLine);
                helpString.Append(Environment.NewLine);
            }
            var messenger = _messengerFactory.Invoke();
            messenger.Write(helpString.ToString());
        }

        private string GetOptionalCommandParameterDescription(string description, string alternativeName, string defaultValue)
        {
            var optionalCommandParameterDescription = FormatCommandDescription(!string.IsNullOrEmpty(alternativeName) ? $"[Optional] {description}  Alternative parameter name: /{alternativeName}. Default value: {defaultValue} "
                    : $"[Optional] {description}. Default value: {defaultValue} ", _commandColumnWidth, MaxWidth - _commandColumnWidth);
            return optionalCommandParameterDescription;
        }

        private string GetRequiredCommandParameterDescription(string description, string alternativeName)
        {
            var requiredCommandParameterDescription = 
                FormatCommandDescription(!string.IsNullOrEmpty(alternativeName) ? 
                        $"[Required] {description}  Alternative parameter name: /{alternativeName}" : 
                        $"[Required] {description}", _commandColumnWidth, MaxWidth - _commandColumnWidth);
            return requiredCommandParameterDescription;
        }

        /// <summary>   Format command parameter. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandParameterName"> Name of the command parameter. </param>
        ///
        /// <returns>   The formatted command parameter. </returns>
        private string FormatCommandParameter(string commandParameterName)
        {
            if (commandParameterName.Length < _commandColumnWidth)
            {
                return ("".PadLeft(3) + commandParameterName).PadRight(_commandColumnWidth);
            }
            return ("".PadLeft(3) + commandParameterName).PadRight(commandParameterName.Length + 3);
        }

        /// <summary>   Format command description. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="description">  The description. </param>
        /// <param name="indent">       The indent. </param>
        /// <param name="width">        The width. </param>
        ///
        /// <returns>   The formatted command description. </returns>
        private string FormatCommandDescription(string description, int indent, int width)
        {
            var textFormatter = new TextFormatter();
            if (description.Length <= width)
            {
                return JustifyText(description) + Environment.NewLine;
            }
            var lines = textFormatter.BreakIntoLines(description, width);
            var sb = new StringBuilder();
            sb.Append(JustifyText(lines[0]) + Environment.NewLine);
            for (var i = 1; i < lines.Count; i++)
            {
                sb.Append("".PadLeft(indent) + JustifyText(lines[i]) + Environment.NewLine);
            }
            return sb.ToString();
        }

        /// <summary>   Format command. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandName">  Name of the command. </param>
        ///
        /// <returns>   The formatted command. </returns>
        private string FormatCommand(string commandName)
        {
            if (commandName.Length < _commandColumnWidth)
            {
                return commandName.PadRight(_commandColumnWidth);
            }
            return commandName.PadRight(commandName.Length + 3);
        }

        ///  <summary>   Justify text. </summary>
        /// 
        ///  <remarks>   trond, 2013-05-01. </remarks>
        /// <param name="text">     The text. </param>
        /// <returns>   . </returns>
        private string JustifyText(string text)
        {
            return text;          
        }
        #endregion
    }
}