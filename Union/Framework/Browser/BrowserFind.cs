using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Union.Framework.Browser.Exceptions;
using Union.SCSS;

namespace Union.Framework.Browser
{
    public class BrowserFind : DriverFacade
    {
        public BrowserFind(Browser browser)
            : base(browser)
        {
        }

        public IWebElement Element(string scssSelector)
        {
            return Element(ScssBuilder.CreateBy(scssSelector));
        }

        public IWebElement Element(By by, bool displayed = true)
        {
            return Element(Driver, by, displayed);
        }

        public IWebElement Element(ISearchContext context, By by, bool displayed = true)
        {
            var start = DateTime.Now;
            var elements = context.FindElements(by).ToList();
            if (elements.Count == 0)
            {
                Log.Selector(by);
                throw new NoSuchElementException($"Search time: {(DateTime.Now - start).TotalMilliseconds}");
            }

            if (displayed)
            {
                elements = elements.Where(e => e.Displayed).ToList();
                if (elements.Count == 0)
                {
                    Log.Selector(by);
                    throw new NoVisibleElementsException();
                }
            }

            if (elements.Count > 1)
            {
                throw new Exception($"Found more then 1 element by selector '{by}'");
            }

            return Browser.Options.FindSingle ? elements.SingleOrDefault() : elements.First();
        }

        public IWebElement ElementFastS(string scssSelector, bool displayed = true)
        {
            return ElementFastS(ScssBuilder.CreateBy(scssSelector), displayed);
        }

        public IWebElement ElementFastS(By by, bool displayed = true)
        {
            return ElementFastS(Driver, by, displayed);
        }

        public IWebElement ElementFastS(ISearchContext context, By by, bool displayed = true)
        {
            try
            {
                Browser.DisableTimeout();
                return Element(context, by, displayed);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
            catch (NoVisibleElementsException)
            {
                return null;
            }
            finally
            {
                Browser.EnableTimeout();
            }
        }

        public IWebElement ElementFast(string scssSelector)
        {
            return ElementFast(ScssBuilder.CreateBy(scssSelector));
        }

        public IWebElement ElementFast(By by)
        {
            try
            {
                Browser.DisableTimeout();
                Log.Selector(by);
                return Driver.FindElement(by);
            }
            finally
            {
                Browser.EnableTimeout();
            }
        }

        public List<IWebElement> Elements(string scssSelector)
        {
            return Elements(ScssBuilder.CreateBy(scssSelector));
        }

        public List<IWebElement> Elements(By by)
        {
            try
            {
                Browser.DisableTimeout();
                return new List<IWebElement>(Driver.FindElements(by));
            }
            catch (NoSuchElementException)
            {
                return new List<IWebElement>();
            }
            finally
            {
                Browser.EnableTimeout();
            }
        }

        public List<IWebElement> VisibleElements(string scssSelector)
        {
            return VisibleElements(ScssBuilder.CreateBy(scssSelector));
        }

        public List<IWebElement> VisibleElements(By by)
        {
            return RepeatAfterStale(() => Elements(by).Where(e => e.Displayed).ToList());
        }
    }
}