using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Union.Framework.Driver
{
    public class ChromeDriverFactory : IDriverManager
    {
        private IWebDriver _driver;

        #region DriverManager Members

        public void InitDriver()
        {
            var buildCheckoutDir = Environment.GetEnvironmentVariable("BuildCheckoutDir");
            _driver = string.IsNullOrEmpty(buildCheckoutDir)
                               ? new ChromeDriver()
                               : new ChromeDriver(Path.Combine(buildCheckoutDir, "selenium.core\\"));
        }

        public IWebDriver GetDriver() => _driver;

        public void DestroyDriver() => _driver.Quit();

        #endregion
    }
}