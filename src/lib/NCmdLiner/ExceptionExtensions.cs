using System;

namespace NCmdLiner
{
    internal static class ExceptionExtensions
    {
        public static void PrepForRemotingAndThrow(this Exception exception)
        {
            var obj = System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(exception);
            obj.Throw();
        }
    }
}