using System.Collections.Generic;

namespace NCmdLiner
{
    public interface ICmdLineryProvider
    {
        int Run(List<CommandRule> commandRules, string[] args);
    }
}
