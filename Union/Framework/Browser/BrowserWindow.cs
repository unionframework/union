namespace Union.Framework.Browser
{
    public class BrowserWindow : DriverFacade
    {
        public BrowserWindow(Browser browser)
            : base(browser)
        {
        }

        public string Url => Driver.Url;
    }
}