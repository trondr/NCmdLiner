using System;
using System.Reflection;

namespace NCmdLiner
{
    internal static class ExceptionExtensions
    {
        public static void PrepForRemotingAndThrow(this Exception exception)
        {

            #if NET35
            var prepForRemoting = typeof(Exception).GetMethodEx("PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance);
            if (prepForRemoting != null)
            {
                prepForRemoting.Invoke(exception, new object[0]);                 
                throw exception;
            }
            #else
            var obj = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(exception);
            obj.Throw();
            #endif
            
//#if NETSTANDARD1_6
//            return type.GetTypeInfo().IsClass;
//#else
//            return type.IsClass;
//#endif
        }

    }
}