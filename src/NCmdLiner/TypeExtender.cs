using System;
using System.Collections.Generic;
using System.Reflection;

namespace NCmdLiner
{
    internal static class TypeExtender
    {
        public static bool IsClass(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsClass;
#else
            return type.IsClass;
#endif
        } 

        public static bool IsGenericTypeDefinition(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsGenericTypeDefinition;
#else
            return type.IsGenericTypeDefinition;
#endif
        } 

        public static bool IsInterface(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsInterface;
#else
            return type.IsInterface;
#endif
        }  

        public static bool IsAbstract(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsAbstract;
#else
            return type.IsAbstract;
#endif
        }  

        public static bool IsPrimitive(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsPrimitive;
#else
            return type.IsPrimitive;
#endif
        } 

        public static bool IsGenericType(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsGenericType;
#else
            return type.IsGenericType;
#endif
        } 

        public static bool IsValueType(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsValueType;
#else
            return type.IsValueType;
#endif
        } 

        public static bool IsEnum(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().IsEnum;
#else
            return type.IsEnum;
#endif
        } 
        
        public static Assembly GetAssembly(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().Assembly;
#else
            return type.Assembly;
#endif
        }    
        
        public static Type[] FindInterfacesEx(this Type type, TypeFilter filter,object filterCriteria)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().FindInterfaces(filter,filterCriteria);
#else
            return type.FindInterfaces(filter,filterCriteria);
#endif
        }

         public static Type BaseType(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().BaseType;
#else
            return type.BaseType;
#endif
        }
        
        public static IEnumerable<Attribute> GetCustomAttributes(this Type type)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().GetCustomAttributes();
#else
            return type.GetCustomAttributes();
#endif
        }


        public static IEnumerable<Attribute> GetCustomAttributes(this Type type, Type attributeType, bool inherit)
        {
#if NETSTANDARD1_6
            return type.GetTypeInfo().GetCustomAttributes(attributeType, inherit);
#else
            return (IEnumerable<Attribute>)type.GetCustomAttributes(attributeType, inherit);
#endif
        }
    }    
}
