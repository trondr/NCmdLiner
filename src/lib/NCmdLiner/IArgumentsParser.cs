using System.Collections.Generic;
using LanguageExt.Common;

namespace NCmdLiner
{
    public interface IArgumentsParser
    {
        Result<Dictionary<string, CommandLineParameter>> GetCommandLineParameters(string[] args);
    }
}