﻿using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Union.SCSS;

namespace Union.Framework.Browser
{
    public class BrowserAction : DriverFacade
    {
        private readonly BrowserFind _find;

        public BrowserAction(Browser browser)
            : base(browser)
        {
            _find = browser.Find;
        }

        public void Select(string scssSelector, string value) => Select(ScssBuilder.CreateBy(scssSelector), value);

        public void Select(By by, string value) =>
            RepeatAfterStale(
                () =>
                {
                    var select = _find.Element(@by);
                    var dropDown = new SelectElement(@select);
                    dropDown.SelectByValue(value);
                });

        public void TypeIn(string scssSelector, object value, bool clear = true) => TypeIn(ScssBuilder.CreateBy(scssSelector), value, clear);

        public void TypeIn(By by, object value, bool clear = true) =>
            RepeatAfterStale(
                () =>
                {
                    var element = _find.Element(@by);
                    if (clear)
                    {
                        Clear(element);
                    }

                    foreach (var c in value.ToString())
                    {
                        element.SendKeys(c.ToString());
                    }
                });

        public void Click(string scssSelector, int sleepTimeout = 0) => Click(ScssBuilder.CreateBy(scssSelector), sleepTimeout);

        public void Click(By by, int sleepTimeout = 0) => Click(Driver, @by, sleepTimeout);

        public void Click(ISearchContext context, By by, int sleepTimeout = 0) =>
            RepeatAfterStale(
                () =>
                {
                    try
                    {
                        if (Browser.Options.WaitWhileAjaxBeforeClick)
                        {
                            Browser.Wait.WhileAjax();
                        }

                        Click(_find.Element(context, @by), sleepTimeout);
                    }
                    catch (InvalidOperationException e)
                    {
                        Log.Selector(@by);
                        Log.Exception(e);
                    }
                });

        public void Click(IWebElement element, int sleepTimeout = 0)
        {
            //Browser.Js.ScrollIntoView(element); // Fix for "element not visible" exception
            element.Click();
            if (sleepTimeout != 0)
            {
                Thread.Sleep(sleepTimeout);
            }
        }

        public void ClickAndWaitForAlert(string scssSelector, int timeout = BrowserTimeouts.AJAX) => ClickAndWaitForAlert(ScssBuilder.CreateBy(scssSelector), timeout);

        public void ClickAndWaitForAlert(By by, int timeout = BrowserTimeouts.AJAX)
        {
            Click(by);
            Browser.Wait.Until(
                () =>
                {
                    Browser.State.ActualizeHtmlAlert();
                    return Browser.State.HtmlAlert != null;
                },
                timeout);
            Browser.State.Actualize();
        }

        public void ClickAndWaitForRedirect(string scssSelector, bool waitForAjax = false, bool ajaxInevitable = false) =>
            ClickAndWaitForRedirect(ScssBuilder.CreateBy(scssSelector), waitForAjax, ajaxInevitable);

        public void ClickAndWaitForRedirect(By by, bool waitForAjax = false, bool ajaxInevitable = false) =>
            RepeatAfterStale(
                () => ClickAndWaitForRedirect(Browser.Find.Element(@by), waitForAjax, ajaxInevitable));

        public void ClickAndWaitForRedirect(IWebElement element, bool waitForAjax = false, bool ajaxInevitable = false)
        {
            const int POLLING_INTERVAL = 200;
            var oldUrl = Browser.Window.Url;
            Click(element, 1000);
            for (var i = 0; i < 10; i++)
            {
                Browser.State.Actualize();
                if (Browser.State.SystemAlert != null || oldUrl != Browser.Window.Url)
                {
                    if (waitForAjax || ajaxInevitable)
                    {
                        Browser.Wait.WhileAjax(ajaxInevitable: ajaxInevitable);
                    }

                    return;
                }

                Thread.Sleep(POLLING_INTERVAL);
            }

            Log.Info("No redirect after click");
        }

        /// <summary>
        ///     Клик и ожидание пока отрыбатывают Ajax Запросы
        /// </summary>
        public void ClickAndWaitWhileAjax(string scssSelector, int sleepTimeout = 0, bool ajaxInevitable = false) =>
            ClickAndWaitWhileAjax(ScssBuilder.CreateBy(scssSelector), sleepTimeout, ajaxInevitable);

        public void ClickAndWaitWhileAjax(By by, int sleepTimeout = 0, bool ajaxInevitable = false) => ClickAndWaitWhileAjax(Driver, @by, sleepTimeout, ajaxInevitable);

        public void ClickAndWaitWhileAjax(ISearchContext context, By by, int sleepTimeout = 0, bool ajaxInevitable = false) =>
            RepeatAfterStale(() => ClickAndWaitWhileAjax(Browser.Find.Element(context, @by), sleepTimeout, ajaxInevitable));

        public void ClickAndWaitWhileAjax(IWebElement element, int sleepTimeout = 0, bool ajaxInevitable = false)
        {
            Browser.Wait.WhileAjax();
            Click(element, sleepTimeout);
            Browser.Wait.WhileAjax(ajaxInevitable: ajaxInevitable);
            Browser.State.Actualize();
        }

        public void PressKey(string scssSelector, string key) => PressKey(ScssBuilder.CreateBy(scssSelector), key);

        public void PressKey(By by, string key) => RepeatAfterStale(() => PressKey(Browser.Find.Element(@by), key));

        public void PressKey(IWebElement element, string key) => element.SendKeys(key);

        public void Clear(string scssSelector) => Clear(ScssBuilder.CreateBy(scssSelector));

        public void Clear(By by) => RepeatAfterStale(() => Clear(Browser.Find.Element(@by)));

        public void Clear(IWebElement element)
        {
            element.Clear();
        }

        public void ChangeFocus() => PressKey(Driver.SwitchTo().ActiveElement(), Keys.Tab);

        public void ClickAndWaitWhileProgress(
            string scssSelector,
            int sleepTimeout = 0,
            bool progressInevitable = false) =>
            ClickAndWaitWhileProgress(ScssBuilder.CreateBy(scssSelector), sleepTimeout, progressInevitable);

        public void ClickAndWaitWhileProgress(By by, int sleepTimeout = 0, bool progressInevitable = false)
        {
            Click(by, sleepTimeout);
            if (progressInevitable)
            {
                Browser.Wait.ForPageProgress();
            }

            Browser.Wait.WhilePageInProgress();
            Browser.State.Actualize();
        }

        public void PressEnter(string scssSelector) => PressEnter(ScssBuilder.CreateBy(scssSelector));

        public void PressEnter(By by) => PressKey(@by, Keys.Enter);

        public void ClickByAll(string scssSelector) => ClickByAll(ScssBuilder.CreateBy(scssSelector));

        public void ClickByAll(By by)
        {
            var elements = Browser.Find.Elements(by);
            foreach (var element in elements)
            {
                Browser.Action.Click(element);
            }
        }

        public void ScrollToBottom()
        {
            var start = DateTime.Now;
            do
            {
                Browser.Js.ScrollToBottom();
                Browser.Wait.WhileAjax(ajaxInevitable: true);
            } while (!Browser.Js.IsPageBottom() && (DateTime.Now - start).TotalSeconds < 300);
        }

        public void MouseOver(string scssSelector, int sleepTimeout = 0) => MouseOver(ScssBuilder.CreateBy(scssSelector), sleepTimeout);

        public void MouseOver(By by, int sleepTimeout = 0)
        {
            var action = new Actions(Driver);
            RepeatAfterStale(
                () =>
                {
                    var element = Browser.Find.Element(by);
                    action.MoveToElement(element).Build().Perform();
                    if (sleepTimeout != 0)
                    {
                        Thread.Sleep(sleepTimeout);
                    }
                });
        }

        public void SetFocus(IWebElement element)
        {
            if (element.TagName == "input")
            {
                element.SendKeys("");
            }
            else
            {
                new Actions(Driver).MoveToElement(element).Build().Perform();
            }
        }

        public void DragByHorizontal(string scssSelector, int pixels) => DragByHorizontal(ScssBuilder.CreateBy(scssSelector), pixels);

        public void DragByHorizontal(By by, int pixels)
        {
            var builder = new Actions(Driver);
            builder.DragAndDropToOffset(Browser.Find.Element(by), pixels, 0).Build().Perform();
        }
    }
}