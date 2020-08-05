using System.Collections.Generic;
using System.Collections.Specialized;
using OpenQA.Selenium;
using Union.Framework.Service;
using Union.Logging;

namespace Union.Framework.Components.Interfaces
{
    public interface IPage : IPageObject
    {
        new List<Cookie> Cookies { get; set; }

        Dictionary<string, string> Data { get; set; }

        StringDictionary Params { get; set; }

        BaseUrlInfo BaseUrlInfo { get; set; }

        List<IHtmlAlert> Alerts { get; }

        List<IProgressBar> ProgressBars { get; }

        void RegisterComponent(IComponent component);

        T RegisterComponent<T>(string componentName, params object[] args) where T : IComponent;

        T CreateComponent<T>(params object[] args) where T : IComponent;
        void Activate(Browser.Browser browser, ITestLogger log);
    }
}