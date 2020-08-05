using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;
using Union.Framework.Service;

namespace Union.Framework.Browser
{
    public class BrowserState : DriverFacade
    {
        public string CurrentWindowHandle;

        public IAlert HtmlAlert;

        public IPage Page;

        public IAlert SystemAlert;

        public BrowserState(Browser browser)
            : base(browser)
        {
        }

        public IAlert GetActiveAlert() => SystemAlert ?? HtmlAlert;

        public T HtmlAlertAs<T>() => (T) HtmlAlert;

        public bool HtmlAlertIs<T>()
        {
            if (HtmlAlert == null)
            {
                return false;
            }

            return HtmlAlert is T;
        }

        public T PageAs<T>() => (T) Page;

        public bool PageIs<T>() where T : IPage
        {
            if (Page == null)
            {
                return false;
            }

            return Page is T;
        }

        public void Actualize()
        {
            ActualizeSystemAlert();
            if (SystemAlert != null)
            {
                return;
            }

            ActualizeWindow();
            ActualizePage(
                new RequestData(
                    Driver.Url,
                    new List<Cookie>(Driver.Manage().Cookies.AllCookies.AsEnumerable())));
            ActualizeHtmlAlert();
        }

        private void ActualizeWindow()
        {
            if (Driver.WindowHandles.Last() != CurrentWindowHandle)
            {
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                CurrentWindowHandle = Driver.CurrentWindowHandle;
            }
        }

        public void ActualizeHtmlAlert()
        {
            HtmlAlert = null;
            if (Page == null)
            {
                return;
            }

            HtmlAlert = Page.Alerts.FirstOrDefault(a => a.IsVisible());
        }

        private void ActualizeSystemAlert() => SystemAlert = Browser.Alert.GetSystemAlert();

        private void ActualizePage(RequestData requestData)
        {
            Page = null;
            var result = Web.MatchService(requestData);
            if (result != null)
            {
                Page = result.GetService().GetPage(requestData, result.GetBaseUrlInfo());
            }

            Page?.Activate(Browser, Log);
        }
    }
}