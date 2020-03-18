using System.Collections.Generic;

namespace NCmdLiner.Tests.UnitTests.Custom
{
    public class CustomTestHelpProvider : IHelpProvider
    {
        public bool IsCreditsRequested(string commandName)
        {
            throw new CustomTestHelpProviderException();
        }

        public bool IsHelpRequested(string commandName)
        {
            throw new CustomTestHelpProviderException();
        }

        public bool IsLicenseRequested(string commandName)
        {
            throw new CustomTestHelpProviderException();
        }

        public void ShowCredits(IApplicationInfo applicationInfo)
        {
            throw new CustomTestHelpProviderException();
        }

        public void ShowHelp(List<CommandRule> commandRules, CommandRule helpForCommandRule, IApplicationInfo applicationInfo)
        {
            throw new CustomTestHelpProviderException();
        }

        public void ShowLicense(IApplicationInfo applicationInfo)
        {
            throw new CustomTestHelpProviderException();
        }
    }
}