using LanguageExt.Common;

namespace NCmdLiner
{
    public interface ICommandRuleValidator
    {
        Result<int> Validate(string[] args, CommandRule commandRule);
    }
}