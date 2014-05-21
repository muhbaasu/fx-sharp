using FxSharp.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests.Extensions
{
    /// <summary>
    /// Internal test enum.
    /// </summary>
    internal enum DaysInt
    {
        Sat = 0,
        Sun = 1,
        Mon = 2
    }

    [TestClass]
    public class IntegralTypeExtensionsTests
    {

        /// <summary>
        /// Test if the parsing of an integer value returns Just when the 
        /// given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumInt()
        {
            var value = (int) DaysInt.Mon;
            value.ParseEnum<DaysInt>().Match_(
                just: day => Assert.AreEqual(day, DaysInt.Mon),
                nothing: () => Assert.Fail("Option should be nothing!"));

        }

        /// <summary>
        /// Test if the parsing of an integer value returns Nothing when the 
        /// given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumIntFailsWhenNotInRange()
        {
            var day = 3;
            day.ParseEnum<DaysInt>()
                .Select_(_ => Assert.Fail("Option should be nothing!"));
        }
    }
}