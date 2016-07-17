using System;
using System.Reflection;

namespace NCmdLiner
{
    internal static class AssemblyExtender
    {
        public static Attribute GetCustomAttributeEx(this Assembly assembly, Type attributeType)
        {
#if NETSTANDARD1_6
            return assembly.GetCustomAttribute(attributeType);
#else
            return Attribute.GetCustomAttribute(assembly, typeof(AssemblyInformationalVersionAttribute), false);
#endif
        }
    }
}