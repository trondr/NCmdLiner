using System.Collections.Generic;

namespace NCmdLiner
{
    internal interface ICmdLineryProvider
    {
        Result<int> Run(List<CommandRule> commandRules, string[] args);
    }
}