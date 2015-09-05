using System;
using System.Collections.Generic;
using System.Reflection;

namespace NCmdLiner
{
    public interface ICommandRuleProvider
    {
        CommandRule GetCommandRule(MethodInfo methodInfo);
        CommandRule GetCommandRule(MethodInfo methodInfo, object targetObject);
        List<CommandRule> GetCommandRules(Type[] targetTypes);
        List<CommandRule> GetCommandRules(Type targetType);
        List<CommandRule> GetCommandRules(object[] targetObjects);
    }
}