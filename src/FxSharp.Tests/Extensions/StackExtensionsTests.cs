using System.Collections.Generic;
using FxSharp.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests.Extensions
{
    [TestClass]
    public class StackExtensionsTests
    {
        private readonly Stack<int> _empty = new Stack<int>();
        private readonly Stack<int> _stack = new Stack<int>();

        public StackExtensionsTests()
        {
            _stack.Push(1);
            _stack.Push(2);
            _stack.Push(3);
        }


        [TestMethod]
        public void PopShouldReturnNothingWhenStackIsEmpty()
        {
            Assert.IsTrue(_empty.PopOrNothing().IsNothing());
        }

        [TestMethod]
        public void PopShouldReturnJustWhenStackNotEmpty()
        {
            Assert.IsTrue(_stack.PopOrNothing().IsJust());
        }

        [TestMethod]
        public void PopShouldReturnCorrectValue()
        {
            _stack.PopOrNothing().Match_(
                just: top => Assert.AreEqual(3, top),
                nothing: () => Assert.Fail("Should not be nothing"));
        }
    }
}