using System.Collections.Generic;
using System.Threading.Tasks;
using LanguageExt.Common;

namespace NCmdLiner
{
    internal interface ICmdLineryProvider
    {
        Task<Result<int>> Run(List<CommandRule> commandRules, string[] args);
    }
}