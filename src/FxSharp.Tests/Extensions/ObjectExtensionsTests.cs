using System;
using System.Collections.Generic;
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

        [TestMethod]
        public void AsShoulldReturnNothingWhenCastFails()
        {
            Assert.IsTrue(0.As<IEnumerable<string>>().IsNothing());
        }

        [TestMethod]
        public void AsShoulldReturnJustWhenCastSuccessful()
        {
            Assert.IsTrue(0.As<ValueType>().IsJust());
        }
    }
}