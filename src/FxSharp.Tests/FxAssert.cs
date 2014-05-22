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
            }
            catch (T)
            {
                Assert.Fail();
            }
        }

        public static void Throws<T>(Action fn) where T : Exception
        {
            try
            {
                fn();
            }
            catch (T)
            {
                Assert.Fail();
            }
        }
    }
}