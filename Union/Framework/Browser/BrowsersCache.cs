using System.Collections.Generic;
using Union.Framework.Driver;
using Union.Framework.Enums;
using Union.Framework.Service;
using Union.Logging;

namespace Union.Framework.Browser
{
    public class BrowsersCache
    {
        private readonly Dictionary<BrowserType, Browser> _browsers;

        private readonly ITestLogger _log;

        private readonly Web _web;

        public BrowsersCache(Web web, ITestLogger log)
        {
            _web = web;
            _log = log;
            _browsers = new Dictionary<BrowserType, Browser>();
        }

        public Browser GetBrowser(BrowserType browserType)
        {
            if (_browsers.ContainsKey(browserType))
            {
                return _browsers[browserType];
            }

            var browser = CreateBrowser(browserType);
            _browsers.Add(browserType, browser);
            return browser;
        }

        private Browser CreateBrowser(BrowserType browserType)
        {
            var driverManager = GetDriverFactory(browserType);
            return new Browser(_web, _log, driverManager);
        }

        private IDriverManager GetDriverFactory(BrowserType browserType)
        {
            switch (browserType)
            {
                case BrowserType.FIREFOX:
                    return new FirefoxDriverManager();
                case BrowserType.CHROME:
                    return new ChromeDriverFactory();
                default:
                    return null;
            }
        }
    }
}