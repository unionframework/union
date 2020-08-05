using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Union.Framework.Driver
{
    public class FirefoxDriverManager : IDriverManager
    {
        private FirefoxDriver _driver;

        #region DriverManager Members

        public IWebDriver GetDriver() => _driver;

        public void InitDriver() => _driver = new FirefoxDriver();

        public void DestroyDriver() => _driver.Quit();

        #endregion
    }
}