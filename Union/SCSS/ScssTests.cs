using NUnit.Framework;

namespace Union.SCSS
{
    [TestFixture]
    public class ScssTests
    {
        [TestCase("div", "div", "//div/descendant::div", "div div")]
        public void Run(string scssSelector1, string scssSelector2, string resultXpath, string resultCss)
        {
            var scss1 = ScssBuilder.Create(scssSelector1);
            var scss2 = ScssBuilder.Create(scssSelector2);
            var resultScss = scss1.Concat(scss2);
            Assert.AreEqual(resultXpath, resultScss.Xpath);
            Assert.AreEqual(resultCss, resultScss.Css);
        }
    }
}