﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCmdLiner.Credit;
using NCmdLiner.License;
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

    [TestFixture(Category = "UnitTests")]
    public class LicenseProviderTests
    {
        [Test]
        public void CreditProviderGetCreditsTest()
        {
            var target = new LicenseProvider();
            var actual = target.GetLicenses(typeof(LicenseProvider).GetAssembly());
            var expectedCount = 4;
            Assert.AreEqual(expectedCount,actual.Count, "Number of embedded license xml is not " + expectedCount);
        }
    }

}
