using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests
{
    [TestClass]
    public class PreludeTests
    {
        [TestMethod]
        public void IdReturnThePlainArgument()
        {
            const string str = "";
            const int five = 5;
            var now = DateTime.Now;

            Assert.AreSame(str, Prelude.Id(str));
            Assert.AreEqual(five, Prelude.Id(five));
            Assert.AreEqual(now, Prelude.Id(now));
        }

        [TestMethod]
        public void ConstShouldAlwaysReturnTheFirstArgument()
        {
            const string str1 = "";
            const string str2 = "abc";
            const int five = 5;
            const int ten = 10;
            var now = DateTime.Now;
            var today = DateTime.Today;

            Assert.AreSame(str1, Prelude.Const(str1, str2));
            Assert.AreEqual(five, Prelude.Const(five, ten));
            Assert.AreEqual(now, Prelude.Const(now, today));
        }
    }
}