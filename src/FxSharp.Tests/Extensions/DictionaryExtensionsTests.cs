using System.Collections.Generic;
using FxSharp.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests.Extensions
{
    [TestClass]
    public class DictionaryExtensionsTests
    {
        private readonly Dictionary<int, int> _dict = new Dictionary<int, int>
        {
            {1, 10},
            {2, 20}
        };

        [TestMethod]
        public void GetMaybeShouldReturnJustWhenInRange()
        {
            _dict.GetMaybe(2)
                .Otherwise_(() => Assert.Fail("Function should not have been invoked."));
        }

        [TestMethod]
        public void GetMaybeShouldReturnNothingWhenNotInRange()
        {
            _dict.GetMaybe(3)
                .Select_(_ => Assert.Fail("Function should not have been invoked."));
        }
    }
}