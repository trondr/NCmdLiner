namespace NCmdLiner
{
    internal interface IMethodParameterBuilder
    {
        object[] BuildMethodParameters(CommandRule commandRule);
    }
}
