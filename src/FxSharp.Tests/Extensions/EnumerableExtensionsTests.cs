﻿using System;
using System.Collections.Generic;
using System.Linq;
using FxSharp.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests.Extensions
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        private readonly IEnumerable<int> _empty = Enumerable.Empty<int>();
        private readonly IEnumerable<int> _list = new List<int> {1, 2, 3};

        [TestMethod]
        public void ToEnumerableShouldWrapObjectInACollection()
        {
            var o = new object();
            var coll = o.ToEnumerable().ToList();

            Assert.AreEqual(1, coll.Count());
            Assert.AreSame(o, coll.First());
        }

        [TestMethod]
        public void FirstOrNothingShouldReturnNothingWhenEmpty()
        {
            Assert.IsTrue(_empty.FirstOrNothing().IsNothing());
        }

        [TestMethod]
        public void FirstOrNothingShouldReturnJustWhenNotEmpty()
        {
            Assert.IsTrue(_list.FirstOrNothing().IsJust());
        }

        [TestMethod]
        public void FirstOrNothingShouldReturnCorrectValueWhenNotEmpty()
        {
            _list.FirstOrNothing().Match_(
                just: first => Assert.AreEqual(1, first),
                nothing: () => Assert.Fail("Should not be nothing"));
        }

        [TestMethod]
        public void FirstOrNothingShouldReturnNothingWhenPredicateUnsatisfied()
        {
            Assert.IsTrue(_list.FirstOrNothing(x => x == 0).IsNothing());
        }

        [TestMethod]
        public void FirstOrNothingShouldReturnJustWhenPredicateSatisfied()
        {
            Assert.IsTrue(_list.FirstOrNothing(x => x == 1).IsJust());
        }

        [TestMethod]
        public void FirstOrNothingShouldReturnCorrectValueWhenPredicateSatisfied()
        {
            _list.FirstOrNothing(x => x == 1).Match_(
                just: first => Assert.AreEqual(1, first),
                nothing: () => Assert.Fail("Should not be nothing"));
        }

        [TestMethod]
        public void FirstOrNothingShouldThrowWhenPredicateNull()
        {
            FxAssert.Throws<ArgumentNullException, Maybe<int>>(() => _empty.FirstOrNothing(null));
        }

        [TestMethod]
        public void FirstOrNothingShouldThrowWhenSourceNull()
        {
            IEnumerable<int> nullEnumerable = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            FxAssert.Throws<ArgumentNullException, Maybe<int>>(
                () => nullEnumerable.FirstOrNothing(null));
            // ReSharper disable once ExpressionIsAlwaysNull
            FxAssert.Throws<ArgumentNullException, Maybe<int>>(nullEnumerable.FirstOrNothing);
        }
    }
}