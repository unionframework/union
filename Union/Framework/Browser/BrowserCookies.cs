namespace Union.Framework.Browser
{
    public class BrowserCookies : DriverFacade
    {
        public BrowserCookies(Browser browser)
            : base(browser)
        {
        }

        public void Clear()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }
    }
}