using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests
{
    [TestClass]
    public class MaybeTests
    {
        internal enum TestEnum
        {
            DefaultValue,
            AnotherValue
        }

        [TestMethod]
        public void ToMaybeShouldReturnNothingForNull()
        {
            object o = null;

            // ReSharper disable once ExpressionIsAlwaysNull
            var result = o.ToMaybe().Match(just: _ => true, nothing: () => false);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ToMaybeShouldReturnJustForNotNull()
        {
            var o = new object();
            var result = o.ToMaybe().Match(just: _ => true, nothing: () => false);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToMaybeShouldReturnJustForValueTypes()
        {
            const int five = 5;
            var result = five.ToMaybe().Match(just: _ => true, nothing: () => false);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToMaybeShouldReturnJustForEnumValues()
        {
            const TestEnum defaultValue = TestEnum.DefaultValue;
            const TestEnum anotherValue = TestEnum.AnotherValue;

            Assert.IsTrue(defaultValue.ToMaybe().Match(just: _ => true, nothing: () => false));
            Assert.IsTrue(anotherValue.ToMaybe().Match(just: _ => true, nothing: () => false));
        }

        [TestMethod]
        public void ToEnumerableShouldHaveOneElementForJust()
        {
            var justFive = 5.ToMaybe();
            Assert.AreEqual(1, justFive.ToEnumerable().Count());
        }

        [TestMethod]
        public void ToEnumerableShouldHaveZeroElementsForNothing()
        {
            var nothing = Maybe.Nothing<object>();
            Assert.AreEqual(0, nothing.ToEnumerable().Count());
        }

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

        [TestMethod]
        public void SelectManyShouldPropatateAValueFromNestedMaybes()
        {
            var justFive = 5.ToMaybe();
            var squareFn = new Func<int, Maybe<int>>(i => Maybe.Just(i*i));
            var result = justFive.SelectMany(squareFn).GetOrElse(-1);

            Assert.AreEqual(25, result);
        }

        [TestMethod]
        public void SelectManyShouldPropagateFailureFromNestedMaybes()
        {
            var justFive = 5.ToMaybe();
            var failFn = new Func<int, Maybe<int>>(_ => Maybe.Nothing<int>());
            var result = justFive.SelectMany(failFn).GetOrElse(-1);

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void SelectManyShouldJustReturnFailure()
        {
            var nothing = Maybe.Nothing<object>();
            var called = false;
            var maybeIdentity = new Func<object, Maybe<object>>(o =>
            {
                called = true;
                return o.ToMaybe();
            });

            nothing.SelectMany(maybeIdentity);

            Assert.IsFalse(called);
        }

        [TestMethod]
        public void SelectManyShouldWorkWithSyntax()
        {
            var maybeFive = 5.ToMaybe();
            var maybeFourtyTwo = 42.ToMaybe();
            var result = from five in maybeFive
                         from fourtyTwo in maybeFourtyTwo
                         select five + fourtyTwo;

            Assert.AreEqual(47, result.GetOrElse(-1));
        }

        [TestMethod]
        public void GetOrElseTest()
        {
            var justFive = 5.ToMaybe();
            var nothing = Maybe.Nothing<int>();

            Assert.AreEqual(5, justFive.GetOrElse(-1));
            Assert.AreEqual(-1, nothing.GetOrElse(-1));
        }

        [TestMethod]
        public void ToStringTest()
        {
            var justFive = 5.ToMaybe();
            var nothing = Maybe.Nothing<int>();

            Assert.AreEqual("Just 5", justFive.ToString());
            Assert.AreEqual("Nothing", nothing.ToString());
        }
    }
}