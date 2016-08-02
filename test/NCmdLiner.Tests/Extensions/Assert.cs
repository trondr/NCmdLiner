using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


#if XUNIT
using Xunit;
using Test = Xunit.FactAttribute;
using XUnitAssert = Xunit.Assert;
#else
using NUnit.Framework;
using NUnitAssert = NUnit.Framework.Assert;
#endif

namespace NCmdLiner.Tests.Extensions
{
    public class Assert
    {
        public static void AreEqual(object expected, object actual, string message = null)
        {
#if XUNIT
            XUnitAssert.Equal(expected, actual);
#else
            NUnitAssert.AreEqual(expected, actual, message);
#endif
        }

        public static void Throws<T>(Action action) where T : Exception 
        {
#if XUNIT
            XUnitAssert.Throws<T>(action);
#else
            NUnitAssert.Throws<T>(action.Invoke);
#endif
        }

        public static void IsTrue(bool condition, string message = null)
        {
#if XUNIT
            XUnitAssert.True(condition);
#else
            NUnitAssert.IsTrue(condition, message);
#endif
        }

        public static void IsFalse(bool condition, string message = null)
        {
#if XUNIT
            XUnitAssert.False(condition);
#else
            NUnitAssert.IsFalse(condition, message);
#endif
        }

        public static void IsNull(object obj, string message = null)
        {
#if XUNIT
            XUnitAssert.Null(obj);
#else
            NUnitAssert.IsNull(obj, message);
#endif
        }


        public static void IsNotNull(object obj, string message = null)
        {
#if XUNIT
            XUnitAssert.NotNull(obj);
#else
            NUnitAssert.IsNotNull(obj, message);
#endif
        }

        public static void Contains(string expectedSubstring, string actualString, string message = null)
        {
#if XUNIT
            XUnitAssert.Contains(expectedSubstring,actualString);
#else
            var comparisonType = (StringComparison) StringComparison.CurrentCulture;
            var isNotContaining = (actualString == null) || (actualString.IndexOf(expectedSubstring, comparisonType) < 0);
            NUnitAssert.IsFalse(isNotContaining, message);
#endif
        }
    }
}
