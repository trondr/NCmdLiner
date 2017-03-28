namespace NCmdLiner
{
    internal interface ICommandRuleValidator
    {
        void Validate(string[] args, CommandRule commandRule);
    }
}