using LanguageExt.Common;

namespace NCmdLiner
{
    internal interface IMethodParameterBuilder
    {
        Result<object[]> BuildMethodParameters(CommandRule commandRule);
    }
}
