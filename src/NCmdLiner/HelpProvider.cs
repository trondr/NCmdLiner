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
                if (applicationInfo.Authors.Contains(","))
                {
                    messenger.WriteLine("Authors: {0}", applicationInfo.Authors);
                }
                else
                {
                    messenger.WriteLine("Author: {0}", applicationInfo.Authors);
                }
            }
            messenger.WriteLine("Usage: {0} <command> [parameters]", applicationInfo.ExeFileName);
            messenger.WriteLine(string.Empty);
            _commandColumnWidth = CalculateCommandColumnWitdth(commandRules);
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

                foreach (CommandRule commandRule in commandRules)
                {
                    ShowCommandRuleHelp(commandRule, false, applicationInfo);
                }
                messenger.WriteLine(string.Empty);
                messenger.WriteLine("Commands and parameters:");
                messenger.WriteLine("------------------------");
                foreach (CommandRule commandRule in commandRules)
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
            StringBuilder creditsText = new StringBuilder();
            creditsText.Append(string.Format("Credits:") + Environment.NewLine);
            creditsText.Append(
                string.Format("-------------------------------------------------------------------------------") +
                Environment.NewLine);
            ICreditProvider creditProvider = new CreditProvider();
            List<ICreditInfo> creditInfos = creditProvider.GetCredits(Assembly.GetEntryAssembly());
            AssemblyName[] referencedAssemblies = Assembly.GetCallingAssembly().GetReferencedAssemblies();
            foreach (AssemblyName referencedAssembly in referencedAssemblies)
            {
                List<ICreditInfo> referencedCreditInfos = creditProvider.GetCredits(Assembly.Load(referencedAssembly));
                creditInfos.AddRange(referencedCreditInfos);
            }
            foreach (var creditInfo in creditInfos)
            {
                if (!string.IsNullOrEmpty(creditInfo.CreditText))
                {
                    creditsText.Append(
                        string.Format("  (*) For use of {0} ({1}) : {2}", creditInfo.ProductName, creditInfo.ProductHome,
                                      creditInfo.CreditText) + Environment.NewLine);
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
            StringBuilder licenseText = new StringBuilder();
            var messenger = _messengerFactory.Invoke();
            messenger.WriteLine("License summary:");
            licenseText.Append(
                string.Format("-------------------------------------------------------------------------------") +
                Environment.NewLine);
            ILicenseProvider licenseProvider = new LicenseProvider();
            OrderedDictionary uniqueLicenseInfos = new OrderedDictionary();

            List<ILicenseInfo> licenseInfos = licenseProvider.GetLicenses(Assembly.GetEntryAssembly());
            foreach (ILicenseInfo licenseInfo in licenseInfos)
            {
                if (!uniqueLicenseInfos.Contains(licenseInfo.ProductName))
                {
                    uniqueLicenseInfos.Add(licenseInfo.ProductName, licenseInfo);
                }
            }

            AssemblyName[] referencedAssemblies = Assembly.GetCallingAssembly().GetReferencedAssemblies();
            foreach (AssemblyName referencedAssembly in referencedAssemblies)
            {
                IList<ILicenseInfo> referencedLicenseInfos =
                    licenseProvider.GetLicenses(Assembly.Load(referencedAssembly));
                foreach (ILicenseInfo licenseInfo in referencedLicenseInfos)
                {
                    if (!uniqueLicenseInfos.Contains(licenseInfo.ProductName))
                    {
                        uniqueLicenseInfos.Add(licenseInfo.ProductName, licenseInfo);
                    }
                }
            }
            foreach (ILicenseInfo licenseInfo in uniqueLicenseInfos.Values)
            {
                licenseText.Append(
                    string.Format("  (*) {0}, {1}, {2}", licenseInfo.ProductName, licenseInfo.ProductHome,
                                  licenseInfo.License) + Environment.NewLine);
            }
            licenseText.Append(
                string.Format("-------------------------------------------------------------------------------") +
                Environment.NewLine);
            licenseText.Append(string.Format("License details:") + Environment.NewLine);
            licenseText.Append(
                string.Format("-------------------------------------------------------------------------------") +
                Environment.NewLine);
            int count = 0;
            foreach (ILicenseInfo licenseInfo in uniqueLicenseInfos.Values)
            {
                count++;
                licenseText.Append(string.Format("Product: {0}", licenseInfo.ProductName) + Environment.NewLine);
                licenseText.Append(string.Format("Home: {0}", licenseInfo.ProductHome) + Environment.NewLine);
                licenseText.Append(string.Format("Lincence: {0}", licenseInfo.License) + Environment.NewLine);
                licenseText.Append(string.Format("{0}", licenseInfo.LicenseText) + Environment.NewLine);
                if (count < uniqueLicenseInfos.Values.Count)
                {
                    licenseText.Append(
                        string.Format("-------------------------------------------------------------------------------") +
                        Environment.NewLine);
                }
            }
            return licenseText.ToString();
        }

        /// <summary>   Calculates the command column witdth. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="commandRules"> The command rules. </param>
        ///
        /// <returns>   The calculated command column witdth. </returns>
        private int CalculateCommandColumnWitdth(List<CommandRule> commandRules)
        {
            int maxCommandNameLength = 0;
            foreach (CommandRule commandRule in commandRules)
            {
                if (commandRule.Command.Name.Length > maxCommandNameLength)
                {
                    maxCommandNameLength = commandRule.Command.Name.Length;
                }
                foreach (RequiredCommandParameter requiredCommandParameter in commandRule.Command.RequiredParameters)
                {
                    if (requiredCommandParameter.Name.Length > maxCommandNameLength)
                    {
                        maxCommandNameLength = requiredCommandParameter.Name.Length;
                    }
                }
                foreach (OptionalCommandParameter optionalCommandParameter in commandRule.Command.OptionalParameters)
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
            StringBuilder helpString = new StringBuilder();
            StringBuilder exampleString = new StringBuilder();
            StringBuilder alternativeExampleString = new StringBuilder();
            helpString.Append(FormatCommand(commandRule.Command.Name));
            helpString.Append(FormatCommandDescription(commandRule.Command.Description, _commandColumnWidth, MaxWidth - _commandColumnWidth));
            if (includeParameters)
            {
                if (Type.GetType("Mono.Runtime") != null)
                    exampleString.Append("mono ");
                exampleString.Append(applicationInfo.ExeFileName + " ");
                exampleString.Append(commandRule.Command.Name + " ");
                alternativeExampleString.Append(exampleString);
                foreach (RequiredCommandParameter requiredCommandParameter in commandRule.Command.RequiredParameters)
                {
                    helpString.Append(FormatCommandParameter("/" + requiredCommandParameter.Name));
                    helpString.Append(FormatCommandDescription(string.Format("[Required] {0}  Alternative parameter name: /{1}", requiredCommandParameter.Description, requiredCommandParameter.AlternativeName), _commandColumnWidth, MaxWidth - _commandColumnWidth));
                    exampleString.Append(string.Format("/{0}=\"{1}\" ", requiredCommandParameter.Name, valueConverter.ObjectValue2String(requiredCommandParameter.ExampleValue)));
                    if (!string.IsNullOrEmpty(requiredCommandParameter.AlternativeName))
                    {
                        alternativeExampleString.Append(string.Format("/{0}=\"{1}\" ", requiredCommandParameter.AlternativeName, valueConverter.ObjectValue2String(requiredCommandParameter.ExampleValue)));
                    }
                    else
                    {
                        alternativeExampleString.Append(string.Format("/{0}=\"{1}\" ", requiredCommandParameter.Name, valueConverter.ObjectValue2String(requiredCommandParameter.ExampleValue)));
                    }
                }
                foreach (OptionalCommandParameter optionalCommandParameter in commandRule.Command.OptionalParameters)
                {
                    helpString.Append(FormatCommandParameter("/" + optionalCommandParameter.Name));
                    helpString.Append(
                        FormatCommandDescription(
                            string.Format("[Optional] {0}  Alternative parameter name: /{1}. Default value: {2} ",
                                          optionalCommandParameter.Description, optionalCommandParameter.AlternativeName, valueConverter.ObjectValue2String(optionalCommandParameter.DefaultValue)), _commandColumnWidth,
                            MaxWidth - _commandColumnWidth));
                    exampleString.Append(string.Format("/{0}=\"{1}\" ", optionalCommandParameter.Name, valueConverter.ObjectValue2String(optionalCommandParameter.ExampleValue)));
                    if (!string.IsNullOrEmpty(optionalCommandParameter.AlternativeName))
                    {
                        alternativeExampleString.Append(string.Format("/{0}=\"{1}\" ",
                                                                      optionalCommandParameter.AlternativeName, valueConverter.ObjectValue2String(optionalCommandParameter.ExampleValue)));
                    }
                    else
                    {
                        alternativeExampleString.Append(string.Format("/{0}=\"{1}\" ", optionalCommandParameter.Name, valueConverter.ObjectValue2String(optionalCommandParameter.ExampleValue)));
                    }
                }
                helpString.Append(Environment.NewLine);
                helpString.Append("".PadLeft(3) + "Example: " + exampleString + Environment.NewLine);
                helpString.Append("".PadLeft(3) + "Example (alternative): " + alternativeExampleString +
                                  Environment.NewLine);
                helpString.Append(Environment.NewLine);
                helpString.Append(Environment.NewLine);
            }
            var messenger = _messengerFactory.Invoke();
            messenger.Write(helpString.ToString());
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
            TextFormater textFormater = new TextFormater();
            if (description.Length <= width)
            {
                return JustifyText(description, width) + Environment.NewLine;
            }
            List<string> lines = textFormater.BreakIntoLines(description, width);
            StringBuilder sb = new StringBuilder();
            sb.Append(JustifyText(lines[0], width) + Environment.NewLine);
            for (int i = 1; i < lines.Count; i++)
            {
                sb.Append("".PadLeft(indent) + JustifyText(lines[i], width) + Environment.NewLine);
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

        /// <summary>   Justify text. </summary>
        ///
        /// <remarks>   trond, 2013-05-01. </remarks>
        ///
        /// <param name="text">     The text. </param>
        /// <param name="width">    The width. </param>
        ///
        /// <returns>   . </returns>
        private string JustifyText(string text, int width)
        {
            return text; //Justifying text did not look good, so just return the text without modification.
            //TextFormater textFormater = new TextFormater();
            //return textFormater.Justify(text, width);            
        }

        #endregion
    }
}