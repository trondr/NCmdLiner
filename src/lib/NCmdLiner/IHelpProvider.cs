using System.Collections.Generic;

namespace NCmdLiner
{
    public interface IHelpProvider
    {
        bool IsCreditsRequested(string commandName);
        bool IsHelpRequested(string commandName);
        bool IsLicenseRequested(string commandName);
        void ShowCredits(IApplicationInfo applicationInfo);
        void ShowHelp(List<CommandRule> commandRules, CommandRule helpForCommandRule, IApplicationInfo applicationInfo);
        void ShowLicense(IApplicationInfo applicationInfo);
    }
}