using System;
using System.Reflection;

namespace NCmdLiner
{
    internal static class AssemblyExtender
    {
        public static Attribute GetCustomAttributeEx(this Assembly assembly, Type attributeType)
        {
#if NETSTANDARD1_6 || NETCOREAPP1_0 || NETCOREAPP1_1 || NETCOREAPP2_0
            return assembly.GetCustomAttribute(attributeType);
#else
            return Attribute.GetCustomAttribute(assembly, attributeType, false);
#endif
        }
    }
}