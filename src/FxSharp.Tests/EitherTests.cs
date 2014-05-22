using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FxSharp.Tests
{
    [TestClass]
    public class EitherTests
    {
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
    }
}