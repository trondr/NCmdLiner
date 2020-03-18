using System;
using LanguageExt.Common;

namespace NCmdLiner
{
    public static class ResultExtensions
    {
        public static Exception ToException<T>(this Result<T> result)
        {
            return result.Match(v => throw new InvalidOperationException("Success not expected."),
                exception => exception);
        }

        public static T ToValue<T>(this Result<T> result)
        {
            return result.Match(v => v,exception => throw new InvalidOperationException("Failure not expected.", exception));
        }
    }
}