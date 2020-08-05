using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Union.SCSS;

namespace Union.Framework.Browser
{
    public class BrowserWait : DriverFacade
    {
        public BrowserWait(Browser browser)
            : base(browser)
        {
        }

        public void Until(Func<bool> condition, int seconds = 3)
        {
            var wait = new WebDriverWait(
                new SystemClock(),
                Driver,
                TimeSpan.FromSeconds(seconds),
                TimeSpan.FromMilliseconds(100));
            wait.Until(driver => condition.Invoke());
        }

        public void WhileElementVisible(string scssSelector, int timeout = BrowserTimeouts.AJAX) => WhileElementVisible(Scss.GetBy(scssSelector), timeout);

        public void WhileElementVisible(By by, int timeout = BrowserTimeouts.AJAX)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            wait.Until(driver => !Browser.Is.Visible(by));
        }

        public void WhilePageInProgress()
        {
            if (Browser.State.Page == null)
            {
                return;
            }

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(BrowserTimeouts.PAGE_LOAD));
            wait.Until(driver => Browser.State.Page.ProgressBars.All(p => !p.IsVisible()));
        }

        public void ForPageProgress()
        {
            if (Browser.State.Page == null)
            {
                return;
            }

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(BrowserTimeouts.JS));
            wait.Until(driver => Browser.State.Page.ProgressBars.All(p => p.IsVisible()));
        }

        public void WhileAjax(int timeout = BrowserTimeouts.AJAX, bool ajaxInevitable = false)
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeout));
            var waited = false;
            wait.Until(
                driver =>
                {
                    var ajaxActive = Browser.Is.AjaxActive();
                    if (ajaxActive)
                    {
                        waited = true;
                        return false;
                    }

                    return true;
                });
            if (!waited && ajaxInevitable)
            {
                Browser.Wait.ForAjax(3000);
                wait.Until(driver => !Browser.Is.AjaxActive());
            }
        }

        public void ForAjax(int miliseconds = 1000)
        {
            const int POLLING_INTERVAL = 200;
            var count = (int) Math.Ceiling(miliseconds / (decimal) POLLING_INTERVAL);
            for (var i = 0; i < count; i++)
            {
                if (Browser.Is.AjaxActive())
                {
                    return;
                }

                Thread.Sleep(POLLING_INTERVAL);
            }
        }
    }
}