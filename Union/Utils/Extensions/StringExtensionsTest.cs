using NUnit.Framework;

namespace Union.Utils.Extensions
{
    [TestFixture]
    public class StringExtensionsTest
    {
        [TestCase("-5", "-5")]
        [TestCase("text -5 text", "-5")]
        [TestCase("text 1.2 text", "1.2")]
        [TestCase("text 1,2 text", "1,2")]
        public void FindNumber(string text, string expected)
        {
            Assert.AreEqual(expected, text.FindNumber());
        }

        [TestCase("-5", -5)]
        public void AsDecimal(string text, decimal expected)
        {
            Assert.AreEqual(expected, text.AsDecimal());
        }

        [TestCase("1", "1")]
        [TestCase("text 1 text", "1")]
        [TestCase("text 1.2 text", "1")]
        [TestCase("-1", "-1")]
        [TestCase("text -1 text", "-1")]
        [TestCase("text -1.2 text", "-1")]
        public void FindInt(string text, string expected)
        {
            Assert.AreEqual(expected, text.FindInt());
        }
    }
}