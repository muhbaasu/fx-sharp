using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FxSharp.Tests
{
    [TestClass]
    public class TryTests
    {

        [TestMethod]
        public void SuccessShouldReturnSuccessfullTry()
        {
            var success = Try.Success<bool, Exception>(true);
            Assert.IsTrue(success.isSuccess());
        }

        [TestMethod]
        public void FailureShouldReturnFailingTry()
        {
            var failure = Try.Failure<bool, Exception>(new ArgumentOutOfRangeException());
            Assert.IsTrue(failure.isFailure());
        }
    }
}
