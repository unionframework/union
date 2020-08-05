using System;
using OpenQA.Selenium;
using Union.Framework.Driver;
using Union.Framework.Service;
using Union.Logging;

namespace Union.Framework.Browser
{
    public class Browser
    {
        private readonly IDriverManager _driverManager;

        public readonly BrowserAction Action;

        public readonly BrowserAlert Alert;

        public readonly BrowserFind Find;

        public readonly BrowserGet Get;

        public readonly BrowserGo Go;

        public readonly BrowserIs Is;

        public readonly BrowserJs Js;

        public readonly BrowserState State;

        public readonly BrowserWait Wait;

        public BrowserCookies Cookies;

        public BrowserOptions Options = new BrowserOptions();

        public BrowserWindow Window;

        public Browser(Web web, ITestLogger log, IDriverManager driverManager)
        {
            Web = web;
            Log = log;
            _driverManager = driverManager;
            _driverManager.InitDriver();
            Driver = _driverManager.GetDriver();
            Find = new BrowserFind(this);
            Get = new BrowserGet(this);
            Is = new BrowserIs(this);
            Alert = new BrowserAlert(this);
            State = new BrowserState(this);
            Action = new BrowserAction(this);
            Window = new BrowserWindow(this);
            Go = new BrowserGo(this);
            Wait = new BrowserWait(this);
            Js = new BrowserJs(this);
            Cookies = new BrowserCookies(this);
        }

        public ITestLogger Log { get; private set; }

        public Web Web { get; private set; }

        public IWebDriver Driver { get; }

        // Уничтожить драйвер(закрывает все открытые окна браузер)
        public void Destroy()
        {
            _driverManager.DestroyDriver();
        }

        // Пересоздать драйвер
        public void Recreate()
        {
        }

        public void DisableTimeout()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public void EnableTimeout()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(BrowserTimeouts.FIND);
        }

        public void WithOptions(Action action, bool findSingle = BrowserOptions.FINDSINGLE_DEFAULT)
        {
            var memento = (BrowserOptions)Options.Clone();
            Options.FindSingle = findSingle;
            action.Invoke();
            Options = memento;
        }
    }
}