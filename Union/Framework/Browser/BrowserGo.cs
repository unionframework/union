using System;
using Union.Framework.Components.Interfaces;
using Union.Framework.Service;

namespace Union.Framework.Browser
{
    public class BrowserGo : DriverFacade
    {
        public BrowserGo(Browser browser)
            : base(browser)
        {
        }

        public T ToPage<T>() where T : IPage
        {
            var pageInstance = (T) Activator.CreateInstance(typeof(T));
            ToPage(pageInstance);
            return Browser.State.PageAs<T>();
        }

        public void ToPage(IPage page)
        {
            var requestData = Web.GetRequestData(page);
            ToUrl(requestData);
        }

        public void ToUrl(string url)
        {
            ToUrl(new RequestData(url));
        }

        public void ToUrl(RequestData requestData)
        {
            Log.Action("Navigating to url: {0}", requestData.Url);
            Driver.Navigate().GoToUrl(requestData.Url);
            Browser.State.Actualize();
            Browser.Wait.WhileAjax();
        }
    }
}