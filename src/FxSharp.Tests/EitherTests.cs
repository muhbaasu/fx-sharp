using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests
{
    [TestClass]
    public class EitherTests
    {
        private readonly Either<Exception, int> _left = Either.Left<Exception, int>(new Exception());
        private readonly Either<Exception, int> _right = Either.Right<Exception, int>(100);


        [TestMethod]
        public void ToStringTest()
        {
            var left = Either.Left<int, object>(5);
            var right = Either.Right<object, int>(10);
            var invalid = new Either<object, object>();

            Assert.AreEqual("Left 5", left.ToString());
            Assert.AreEqual("Right 10", right.ToString());
            Assert.AreEqual("Invalid Either", invalid.ToString());
        }

        [TestMethod]
        public void GetOrElseShouldReturnRightWhenSuccessful()
        {
            Assert.AreEqual(100, _right.GetOrElse(1));
        }

        [TestMethod]
        public void GetOrElseShouldReturnLeftWhenFailed()
        {
            Assert.AreEqual(1, _left.GetOrElse(1));
        }

        [TestMethod]
        public void Select_ShouldApplyFnWhenRight()
        {
            var mappedFn = false;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _right.Select_(right => mappedFn = true);
            Assert.IsTrue(mappedFn);
        }

        [TestMethod]
        public void Select_ShouldNotApplyFnWhenLeft()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _left.Select_(right => Assert.Fail("Should not be right."));
        }

        [TestMethod]
        public void SelectShouldApplyFnWhenRight()
        {
            var mappedFn = false;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _right.Select(right => mappedFn = true);
            Assert.IsTrue(mappedFn);
        }

        [TestMethod]
        public void SelectShouldNotApplyFnWhenLeft()
        {
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _left.Select(right =>
            {
                Assert.Fail("Should not be right.");
                return true;
            });
        }

        [TestMethod]
        public void Match_ShouldApplyRightFnWhenRight()
        {
            var mappedFn = false;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _right.Match_(
                left: left => Assert.Fail("Should not be left."),
                right: right => mappedFn = true);
            Assert.IsTrue(mappedFn);
        }

        [TestMethod]
        public void Match_ShouldApplyLeftFnWhenLeft()
        {
            var mappedFn = false;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _left.Match_(
                left: left => mappedFn = true,
                right: right => Assert.Fail("Should not be right."));
            Assert.IsTrue(mappedFn);
        }

        [TestMethod]
        public void MatchShouldApplyRightFnWhenRight()
        {
            var mappedFn = false;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _right.Match(
                left: left =>
                {
                    Assert.Fail("Should not be left.");
                    return false;
                },
                right: right => mappedFn = true);
            Assert.IsTrue(mappedFn);
        }

        [TestMethod]
        public void MatchShouldApplyLeftFnWhenLeft()
        {
            var mappedFn = false;
            // ReSharper disable once ReturnValueOfPureMethodIsNotUsed
            _left.Match(
                left: left => mappedFn = true,
                right: right =>
                {
                    Assert.Fail("Should not be right.");
                    return false;
                });
            Assert.IsTrue(mappedFn);
        }

        [TestMethod]
        public void ToMaybeShouldReturnJustWhenRight()
        {
            Assert.IsTrue(_right.ToMaybe().IsJust());
        }

        [TestMethod]
        public void ToMaybeShouldReturnNothingWhenLeft()
        {
            Assert.IsTrue(_left.ToMaybe().IsNothing());
        }

        [TestMethod]
        public void ToMaybeShouldContainCorrectValue()
        {
            Assert.AreEqual(100, _right.ToMaybe().GetOrElse(-1));
        }
    }
}