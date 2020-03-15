using System.Collections.Generic;
using LanguageExt.Common;

namespace NCmdLiner
{
    internal interface ICmdLineryProvider
    {
        Result<int> Run(List<CommandRule> commandRules, string[] args);
    }
}