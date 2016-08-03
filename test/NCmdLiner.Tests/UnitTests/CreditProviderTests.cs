using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCmdLiner.Credit;
#if XUNIT
using Xunit;
using Test = Xunit.FactAttribute;
using TestFixture = NCmdLiner.Tests.Extensions.TestFixtureAttribute;
#else
using NUnit.Framework;
#endif
using Assert = NCmdLiner.Tests.Extensions.Assert;

namespace NCmdLiner.Tests.UnitTests
{

    [TestFixture]
    public class CreditProviderTests
    {
        [Test]
        public void CreditProviderGetCreditsTest()
        {
            var target = new CreditProvider();
            var actual = target.GetCredits(typeof(CreditProvider).GetAssembly());
            var expectedCount = 2;
            Assert.AreEqual(expectedCount,actual.Count, "Number of embeded credit xml is not " + expectedCount);
        }
    }

}
