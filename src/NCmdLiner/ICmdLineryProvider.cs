using System.Collections.Generic;

namespace NCmdLiner
{
    internal interface ICmdLineryProvider
    {
        int Run(List<CommandRule> commandRules, string[] args);
    }
}
