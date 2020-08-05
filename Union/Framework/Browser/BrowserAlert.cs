using OpenQA.Selenium;

namespace Union.Framework.Browser
{
    public class BrowserAlert : DriverFacade
    {
        public BrowserAlert(Browser browser)
            : base(browser)
        {
        }

        public IAlert GetSystemAlert()
        {
            try
            {
                return Driver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException)
            {
                return null;
            }
        }
    }
}