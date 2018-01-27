using System;
using System.Collections.Generic;
using System.Reflection;

namespace NCmdLiner
{
    public interface ICommandRuleProvider
    {
        Result<CommandRule> GetCommandRule(MethodInfo methodInfo);
        Result<CommandRule> GetCommandRule(MethodInfo methodInfo, object targetObject);
        Result<List<CommandRule>> GetCommandRules(Type[] targetTypes);
        Result<List<CommandRule>> GetCommandRules(Type targetType);
        Result<List<CommandRule>> GetCommandRules(object[] targetObjects);
    }
}