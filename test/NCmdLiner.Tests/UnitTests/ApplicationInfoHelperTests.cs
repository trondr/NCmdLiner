using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public class ApplicationInfoHelperTests
    {
        [Test]
        public void ApplicationInfoHelperApplicationCopyrightTest()
        {
            var actual = ApplicationInfoHelper.ApplicationCopyright;
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual),"ApplicationCopyright is null");
        }

        [Test]
        public void ApplicationInfoHelperApplicationDescriptionTest()
        {
            var actual = ApplicationInfoHelper.ApplicationDescription;
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual),"ApplicationDescription is null");
        }

        [Test]
        public void ApplicationInfoHelperApplicationNameTest()
        {
            var actual = ApplicationInfoHelper.ApplicationName;
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual),"ApplicationName is null");
        }

        [Test]
        public void ApplicationInfoHelperApplicationVersionTest()
        {
            var actual = ApplicationInfoHelper.ApplicationVersion;
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual),"ApplicationVersion is null");
        }

        [Test]
        public void ApplicationInfoHelperExeFileNameTest()
        {
            var actual = ApplicationInfoHelper.ExeFileName;
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual),"ExeFileName is null");
        }

        [Test]
        public void ApplicationInfoHelperExeFilePathTest()
        {
            var actual = ApplicationInfoHelper.ExeFilePath;
            Assert.IsFalse(string.IsNullOrWhiteSpace(actual),"ExeFilePath is null");
        }

    }
}
