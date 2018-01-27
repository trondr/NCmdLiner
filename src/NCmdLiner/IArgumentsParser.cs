using System.Collections.Generic;
using NCmdLiner.Exceptions;

namespace NCmdLiner
{
    public interface IArgumentsParser
    {
        Result<Dictionary<string, CommandLineParameter>> GetCommandLineParameters(string[] args);
    }
}