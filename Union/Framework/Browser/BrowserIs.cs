﻿
using System.Linq;using OpenQA.Selenium;
using Union.SCSS;

namespace Union.Framework.Browser
{
    public class BrowserIs : DriverFacade
    {
        public BrowserIs(Browser browser)
            : base(browser)
        {
        }

        public bool Visible(string scssSelector)
        {
            return Visible(ScssBuilder.CreateBy(scssSelector));
        }

        public bool Visible(By by)
        {
            return RepeatAfterStale(
                () =>
                {
                    var elements = Browser.Find.Elements(by);
                    return elements.Count != 0 && elements.Any(e => e.Displayed);
                });
        }

        public bool Visible(ISearchContext context, By by)
        {
            return RepeatAfterStale(
                () =>
                {
                    var element = Browser.Find.ElementFastS(context, by);
                    return element != null && element.Displayed;
                });
        }

        public bool HasClass(string scssSelector, string className)
        {
            return HasClass(ScssBuilder.CreateBy(scssSelector), className);
        }

        public bool HasClass(By by, string className)
        {
            var element = Browser.Find.ElementFastS(by);
            if (element == null)
            {
                return false;
            }

            return HasClass(element, className);
        }

        public bool HasClass(IWebElement element, string className)
        {
            return element.GetAttribute("class").Split(' ').Select(c => c.Trim()).Contains(className);
        }

        public bool Exists(string scssSelector)
        {
            return Exists(ScssBuilder.CreateBy(scssSelector));
        }

        public bool Exists(By by)
        {
            return Browser.Find.ElementFastS(by, false) != null;
        }

        public bool AjaxActive()
        {
            return !Browser.Js.Excecute<bool>(@"
                        var isJqueryComplete = typeof(jQuery) != 'function' || jQuery.active == 0;
                        var isPrototypeComplete = typeof(Ajax) != 'function' || Ajax.activeRequestCount == 0;
                        var isDojoComplete = typeof(dojo) != 'function' || dojo.io.XMLHTTPTransport.inFlight.length == 0;
                        return isJqueryComplete && isPrototypeComplete && isDojoComplete;");
        }

        public bool Checked(string scssSelector)
        {
            return Checked(ScssBuilder.CreateBy(scssSelector));
        }

        public bool Checked(By by)
        {
            return RepeatAfterStale(() => Checked(Browser.Find.Element(by, false)));
        }

        public bool Checked(IWebElement element)
        {
            return element.GetAttribute("checked") == "true";
        }
    }
}