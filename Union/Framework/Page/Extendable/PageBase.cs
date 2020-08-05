using System.Collections.Generic;
using System.Collections.Specialized;
using OpenQA.Selenium;
using Union.Framework.Browser;
using Union.Framework.Components.Interfaces;
using Union.Framework.Service;
using Union.Logging;

namespace Union.Framework.Page.Extendable
{
    public abstract class PageBase : IPage
    {
        public Browser.Browser Browser { get; private set; }

        public ITestLogger Log { get; private set; }

        public BrowserAction Action => Browser.Action;

        public BrowserAlert Alert => Browser.Alert;

        public BrowserFind Find => Browser.Find;

        public BrowserGet Get => Browser.Get;

        public BrowserGo Go => Browser.Go;

        public BrowserIs Is => Browser.Is;

        public BrowserState State => Browser.State;

        public BrowserWait Wait => Browser.Wait;

        public BrowserJs Js => Browser.Js;

        public BrowserWindow Window => Browser.Window;

        BrowserCookies IPageObject.Cookies => Browser.Cookies;

        #region IPage Members

        public void Activate(Browser.Browser browser, ITestLogger log)
        {
            Browser = browser;
            Log = log;
            Alerts = new List<IHtmlAlert>();
            ProgressBars = new List<IProgressBar>();
            WebPageBuilder.InitPage(this);
        }

        public List<IProgressBar> ProgressBars { get; private set; }

        public List<IHtmlAlert> Alerts { get; private set; }

        public BaseUrlInfo BaseUrlInfo { get; set; }

        public List<Cookie> Cookies { get; set; }

        public StringDictionary Params { get; set; }

        public Dictionary<string, string> Data { get; set; }

        public void RegisterComponent(IComponent component)
        {
            if (component is IHtmlAlert)
            {
                Alerts.Add(component as IHtmlAlert);
            }
            else if (component is IProgressBar)
            {
                ProgressBars.Add(component as IProgressBar);
            }
        }

        public T RegisterComponent<T>(string componentName, params object[] args) where T : IComponent
        {
            var component = CreateComponent<T>(args);
            RegisterComponent(component);
            component.ComponentName = componentName;
            return component;
        }

        public T CreateComponent<T>(params object[] args) where T : IComponent
        {
            return (T)WebPageBuilder.CreateComponent<T>(this, args);
        }

        #endregion
    }
}