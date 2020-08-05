using System;
using OpenQA.Selenium;
using Union.Framework.Service;
using Union.Logging;

namespace Union.Framework.Browser
{
    public abstract class DriverFacade
    {
        protected DriverFacade(Browser browser)
        {
            Browser = browser;
        }

        protected Browser Browser { get; }

        protected Web Web => Browser.Web;

        protected ITestLogger Log => Browser.Log;

        protected IWebDriver Driver => Browser.Driver;

        public T RepeatAfterStale<T>(Func<T> func)
        {
            const int TRY_COUNT = 3;
            var result = default(T);
            for (var i = 0; i < TRY_COUNT; i++)
            {
                try
                {
                    result = func.Invoke();
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                    Log.Exception(e);
                    if (i == TRY_COUNT - 1)
                    {
                        throw;
                    }
                }
            }

            return result;
        }

        public void RepeatAfterStale(Action action) =>
            RepeatAfterStale(
                () =>
                {
                    action.Invoke();
                    return true;
                });
    }
}