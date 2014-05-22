using FxSharp.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests.Extensions
{
    [TestClass]
    public class ObjectExtensionsTests
    {
        [TestMethod]
        public void IdentityShouldJustReturnTheObject()
        {
            var o = new object();
            Assert.AreSame(o, o.Identity());
        }

        [TestMethod]
        public void ConstShouldAlwaysReturnTheObjectCalledOn()
        {
            var o = new object();
            Assert.AreSame(o, o.Constant(new object()));
        }
    }
}