using FxSharp.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests.Extensions
{
    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysInt
    {
        Sat = -32769,
        Sun = -32770,
        Mon = -32771
    }

    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysUInt : uint
    {
        Sat = 32769,
        Sun = 32770,
        Mon = 32771
    }

    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysLong : long
    {
        Sat = -2147483649,
        Sun = -2147483650,
        Mon = -2147483651
    }

    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysULong : ulong
    {
        Sat = 2147483649,
        Sun = 2147483650,
        Mon = 2147483651
    }

    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysShort : short
    {
        Sat = -256,
        Sun = -257,
        Mon = -258
    }

    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysUShort : ushort
    {
        Sat = 256,
        Sun = 257,
        Mon = 258
    }

    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysByte : byte
    {
        Sat = 1,
        Sun = 2,
        Mon = 3
    }

    /// <summary>
    ///     Internal test enum.
    /// </summary>
    internal enum DaysSByte : sbyte
    {
        Sat = -1,
        Sun = -2,
        Mon = -3
    }


    [TestClass]
    public class IntegralTypeExtensionsTests
    {
        /// <summary>
        ///     Test if the parsing of an integer value returns Just when the
        ///     given value is in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumInt()
        {
            const int value = (int) DaysInt.Mon;

            value.ParseEnum<DaysInt>().Match_(
                just: day => Assert.AreEqual(day, DaysInt.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of an integer value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumIntFailsWhenNotInRange()
        {
            const int day = -32772;

            day.ParseEnum<DaysInt>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of an integer value returns Just when the
        ///     given value is in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumUInt()
        {
            const uint value = (uint) DaysUInt.Mon;
            value.ParseEnum<DaysUInt>().Match_(
                just: day => Assert.AreEqual(day, DaysUInt.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of an integer value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumUIntFailsWhenNotInRange()
        {
            const uint day = 32772;
            day.ParseEnum<DaysUInt>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a long value returns Just when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumLong()
        {
            const long value = (long) DaysLong.Mon;
            value.ParseEnum<DaysLong>().Match_(
                just: day => Assert.AreEqual(day, DaysLong.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a long value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumLongFailsWhenNotInRange()
        {
            const long day = -2147483652;
            day.ParseEnum<DaysLong>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of an ulong value returns Just when the
        ///     given value is in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumULong()
        {
            const ulong value = (ulong) DaysULong.Mon;
            value.ParseEnum<DaysULong>().Match_(
                just: day => Assert.AreEqual(day, DaysULong.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of an ulong value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumULongFailsWhenNotInRange()
        {
            const ulong day = 2147483652;
            day.ParseEnum<DaysULong>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a short value returns Just when the
        ///     given value is in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumShort()
        {
            const short value = (short) DaysShort.Mon;
            value.ParseEnum<DaysShort>().Match_(
                just: day => Assert.AreEqual(day, DaysShort.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a short value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumShortFailsWhenNotInRange()
        {
            const short day = -259;
            day.ParseEnum<DaysShort>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a short value returns Just when the
        ///     given value is in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumUShort()
        {
            const ushort value = (ushort) DaysUShort.Mon;
            value.ParseEnum<DaysUShort>().Match_(
                just: day => Assert.AreEqual(day, DaysUShort.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a short value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumUShortFailsWhenNotInRange()
        {
            const ushort day = 259;
            day.ParseEnum<DaysUShort>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a byte value returns Just when the
        ///     given value is in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumByte()
        {
            const byte value = (byte) DaysByte.Mon;
            value.ParseEnum<DaysByte>().Match_(
                just: day => Assert.AreEqual(day, DaysByte.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a byte value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumByteFailsWhenNotInRange()
        {
            const byte day = 4;
            day.ParseEnum<DaysByte>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a sbyte value returns Just when the
        ///     given value is in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumSByte()
        {
            const sbyte value = (sbyte) DaysSByte.Mon;
            value.ParseEnum<DaysSByte>().Match_(
                just: day => Assert.AreEqual(day, DaysSByte.Mon),
                nothing: () => Assert.Fail("Maybe should be nothing!"));
        }

        /// <summary>
        ///     Test if the parsing of a short value returns Nothing when the
        ///     given value is not in range of the given enum type.
        /// </summary>
        [TestMethod]
        public void ParseEnumSByteFailsWhenNotInRange()
        {
            const sbyte day = -4;
            day.ParseEnum<DaysSByte>()
                .Select_(_ => Assert.Fail("Maybe should be nothing!"));
        }
    }
}