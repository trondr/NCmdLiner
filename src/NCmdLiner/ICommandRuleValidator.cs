namespace NCmdLiner
{
    public interface ICommandRuleValidator
    {
        void Validate(string[] args, CommandRule commandRule);
    }
}