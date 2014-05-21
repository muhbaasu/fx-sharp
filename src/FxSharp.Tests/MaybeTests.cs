using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests
{
    [TestClass]
    public class MaybeTests
    {
        [TestMethod]
        public void Select_DoNotMapFnOnlyWhenNothing()
        {
            Maybe.Nothing<int>()
                .Select_(_ => Assert.Fail("Function should not have been invoked."));
        }

        [TestMethod]
        public void Select_MapFnWhenJust()
        {
            var mapped = false;
            Maybe.Just("value").Select_(_ => mapped = true);
            Assert.IsTrue(mapped);
        }

        [TestMethod]
        public void SelectDoNotMapFnWhenNothing()
        {
            Maybe.Nothing<int>().Select(_ =>
            {
                Assert.Fail("Function should not have been invoked.");
                return 0;
            });
        }

        [TestMethod]
        public void SelectMapFnWhenJust()
        {
            var mapped = false;
            Maybe.Just("value").Select(_ => mapped = true);
            Assert.IsTrue(mapped);
        }

        [TestMethod]
        public void GetOrElseShouldReturnDefaultValueWhenNothing()
        {
            var maybe = Maybe.Nothing<string>();
            Assert.AreEqual("default", maybe.GetOrElse("default"));
        }

        [TestMethod]
        public void GetOrElseShouldReturnValueWhenJust()
        {
            var maybe = Maybe.Just("value");
            Assert.AreEqual("value", maybe.GetOrElse("default"));
        }

        [TestMethod]
        public void Otherwise_DoNotMapFnWhenJust()
        {
            Maybe.Just("value")
                .Otherwise_(() => Assert.Fail("Function should not have been invoked."));
        }

        [TestMethod]
        public void Otherwise_MapFnWhenNothing()
        {
            var mapped = false;
            Maybe.Nothing<string>().Otherwise_(() => mapped = true);
            Assert.IsTrue(mapped);
        }

        [TestMethod]
        public void Match_MapJustFnWhenJust()
        {
            var mapped = false;
            Maybe.Just("value").Match_(
                just: _ => mapped = true,
                nothing: () => Assert.Fail("Function should not have been invoked."));
            Assert.IsTrue(mapped);
        }

        [TestMethod]
        public void Match_MapNothingFnWhenNothing()
        {
            var mapped = false;
            Maybe.Nothing<string>().Match_(
                just: _ => Assert.Fail("Function should not have been invoked."),
                nothing: () => mapped = true);
            Assert.IsTrue(mapped);
        }


        [TestMethod]
        public void MatchMapJustFnWhenJust()
        {
            var mapped = false;
            Maybe.Just("value").Match(
                just: _ => mapped = true,
                nothing: () =>
                {
                    Assert.Fail("Function should not have been invoked.");
                    return false;
                });
            Assert.IsTrue(mapped);
        }

        [TestMethod]
        public void MatchMapNothingFnWhenNothing()
        {
            var mapped = false;
            Maybe.Nothing<string>().Match(
                just: _ =>
                {
                    Assert.Fail("Function should not have been invoked.");
                    return false;
                },
                nothing: () => mapped = true);
            Assert.IsTrue(mapped);
        }

        [TestMethod]
        public void MatchReturnCorrectFnResult()
        {
            var result = Maybe.Nothing<string>().Match(
                just: _ => false,
                nothing: () => true);
            Assert.IsTrue(result);
        }
    }
}