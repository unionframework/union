using OpenQA.Selenium;
using Union.Utils;

namespace Union.SCSS
{
    public class Scss
    {
        public readonly string Css;

        public readonly string Xpath;

        public Scss(string xpath, string css)
        {
            Css = css;
            Xpath = xpath;
        }

        public By By
        {
            get
            {
                return string.IsNullOrEmpty(Css) ? By.XPath(Xpath) : By.CssSelector(Css);
            }
        }

        public string Value
        {
            get
            {
                return string.IsNullOrEmpty(Css) ? Xpath : Css;
            }
        }

        public static string Concat(string scssSelector1, string scssSelector2)
        {
            return ScssBuilder.Concat(scssSelector1, scssSelector2).Value;
        }

        public Scss Concat(Scss scss2)
        {
            string resultXpath = XPathBuilder.Concat(Xpath, scss2.Xpath);
            var resultCss = string.IsNullOrEmpty(Css) || string.IsNullOrEmpty(scss2.Css)
                                ? string.Empty
                                : CssBuilder.Concat(Css, scss2.Css);
            return new Scss(resultXpath, resultCss);
        }

        public static By GetBy(string scssSelector1, string scssSelector2)
        {
            return ScssBuilder.Concat(scssSelector1, scssSelector2).By;
        }

        public static By GetBy(string scssSelector)
        {
            return ScssBuilder.CreateBy(scssSelector);
        }
    }
}