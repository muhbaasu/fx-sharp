using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests
{
    public static class FxAssert
    {
        public static void Throws<T, TRes>(Func<TRes> fn) where T : Exception
        {
            try
            {
                fn();
                Assert.Fail();
            }
            catch (T)
            {
            }
        }

        public static void Throws<T>(Action fn) where T : Exception
        {
            try
            {
                fn();
                Assert.Fail();
            }
            catch (T)
            {
            }
        }
    }
}