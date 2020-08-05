using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using Union.Framework.Enums;
using Union.SCSS;
using Union.Utils.Extensions;

namespace Union.Framework.Browser
{
    public class BrowserGet : DriverFacade
    {
        public BrowserGet(Browser browser)
            : base(browser)
        {
        }

        public string PageSource => Browser.Driver.PageSource;

        public string TextS(string scssSelector)
        {
            return TextS(ScssBuilder.CreateBy(scssSelector));
        }

        public string TextS(By by)
        {
            return RepeatAfterStale(
                () =>
                {
                    var element = Browser.Find.ElementFastS(by);
                    if (element == null)
                    {
                        return null;
                    }

                    return element.Text;
                });
        }

        public string Text(string scssSelector, bool displayed = false)
        {
            return Text(ScssBuilder.CreateBy(scssSelector));
        }

        public string Text(By by, bool displayed = false)
        {
            return Text(Driver, by, displayed);
        }

        public string Text(ISearchContext context, By by, bool displayed = false)
        {
            if (displayed && !Browser.Is.Visible(context, by))
            {
                return null;
            }

            return RepeatAfterStale(() => Browser.Find.Element(context, @by, displayed).Text);
        }

        public List<string> Texts(string scssSelector, bool displayed = false)
        {
            return Texts(ScssBuilder.CreateBy(scssSelector), displayed);
        }

        public List<string> Texts(By by, bool displayed = false)
        {
            return RepeatAfterStale(
                () =>
                {
                    var elements = Browser.Find.Elements(by);
                    if (displayed)
                    {
                        elements = elements.Where(e => e.Displayed).ToList();
                    }

                    return elements.Select(e => e.Text).ToList();
                });
        }

        public string ImgFileName(string scssSelector)
        {
            return ImgFileName(ScssBuilder.CreateBy(scssSelector));
        }

        public string ImgFileName(By by)
        {
            return ImgSrc(by).Split('/').Last();
        }

        public string ImgSrc(string scssSelector)
        {
            return ImgSrc(ScssBuilder.CreateBy(scssSelector));
        }

        public string ImgSrc(By by)
        {
            return Attr(by, "src");
        }

        public List<string> ImgSrcs(string scssSelector)
        {
            return ImgSrcs(ScssBuilder.CreateBy(scssSelector));
        }

        public List<string> ImgSrcs(By by)
        {
            return Attrs(by, "src");
        }

        public string Attr(string scssSelector, string name, bool displayed = true)
        {
            return Attr(ScssBuilder.CreateBy(scssSelector), name, displayed);
        }

        public string Attr(By by, string name, bool displayed = true)
        {
            return RepeatAfterStale(() => Attr(Browser.Find.Element(@by, displayed), name));
        }

        public T Attr<T>(string scssSelector, string name, bool displayed = true)
        {
            return Attr<T>(ScssBuilder.CreateBy(scssSelector), name, displayed);
        }

        public T Attr<T>(By by, string name, bool displayed = true)
        {
            return Cast<T>(Attr(by, name, displayed));
        }

        public string Attr(IWebElement element, string name)
        {
            return element.GetAttribute(name);
        }

        public List<string> Attrs(string scssSelector, string name)
        {
            return Attrs(ScssBuilder.CreateBy(scssSelector), name);
        }

        public List<string> Attrs(By by, string name)
        {
            return RepeatAfterStale(() => Browser.Find.Elements(@by).Select(e => Attr(e, name)).ToList());
        }

        public string Href(string scssSelector)
        {
            return Href(ScssBuilder.CreateBy(scssSelector));
        }

        public string Href(By by)
        {
            return Attr(by, "href");
        }

        public List<string> Hrefs(string scssSelector)
        {
            return Hrefs(ScssBuilder.CreateBy(scssSelector));
        }

        public List<string> Hrefs(By by)
        {
            return Attrs(by, "href");
        }

        public List<T> Attrs<T>(string scssSelector, string name)
        {
            return Attrs<T>(ScssBuilder.CreateBy(scssSelector), name);
        }

        public List<T> Attrs<T>(By by, string name)
        {
            return
                RepeatAfterStale(
                    () => Browser.Find.Elements(by).Select(e => Attr(e, name)).Select(Cast<T>).ToList());
        }

        private T Cast<T>(string value)
        {
            var type = typeof(T);
            if (type == typeof(short) || type == typeof(int) || type == typeof(long))
            {
                return (T) Convert.ChangeType(value.FindInt(), typeof(T));
            }

            if (type == typeof(ushort) || type == typeof(uint) || type == typeof(ulong))
            {
                return (T) Convert.ChangeType(value.FindUInt(), typeof(T));
            }

            if (type == typeof(decimal) || type == typeof(float))
            {
                return (T) Convert.ChangeType(value.FindNumber(), typeof(T));
            }

            return (T) Convert.ChangeType(value, typeof(T));
        }

        public string InputValue(string scssSelector, bool displayed = true)
        {
            return InputValue(ScssBuilder.CreateBy(scssSelector), displayed);
        }

        public string InputValue(By by, bool displayed = true)
        {
            return Attr(by, "value", displayed);
        }

        public string InputValue(IWebElement element, bool displayed = true)
        {
            return Attr(element, "value");
        }

        public int Int(string scssSelector, bool displayed = false)
        {
            return Int(ScssBuilder.CreateBy(scssSelector), displayed);
        }

        public int Int(By by, bool displayed = false)
        {
            return Int(Driver, by, displayed);
        }

        public int Int(ISearchContext context, By by, bool displayed = false)
        {
            return Text(context, by, displayed).AsInt();
        }

        public List<int> Ints(string scssSelector)
        {
            return Ints(ScssBuilder.CreateBy(scssSelector));
        }

        public List<int> Ints(By by)
        {
            return Texts(by).Select(s => s.AsInt()).ToList();
        }

        public T CssValue<T>(string scssSelector, ECssProperty property)
        {
            return CssValue<T>(ScssBuilder.CreateBy(scssSelector), property);
        }

        public T CssValue<T>(By by, ECssProperty property)
        {
            return RepeatAfterStale(() => CssValue<T>(Browser.Find.Element(by), property));
        }

        public T CssValue<T>(IWebElement element, ECssProperty property)
        {
            var value = element.GetCssValue(property.StringValue());
            return Cast<T>(value);
        }

        public int Count(string scssSelector)
        {
            return Count(ScssBuilder.CreateBy(scssSelector));
        }

        public int Count(By by)
        {
            return RepeatAfterStale(() => Browser.Find.Elements(by).Count);
        }

        public T Value<T>(string scssSelector)
        {
            return Value<T>(ScssBuilder.CreateBy(scssSelector));
        }

        public T Value<T>(By by)
        {
            return RepeatAfterStale(() => Value<T>(Browser.Find.Element(by)));
        }

        public T Value<T>(IWebElement element)
        {
            return Cast<T>(element.Text);
        }

        public string TextsAsString(string scssSelector, string delimiter = " ")
        {
            return TextsAsString(ScssBuilder.CreateBy(scssSelector), delimiter);
        }

        public string TextsAsString(By by, string delimiter = " ")
        {
            return Texts(by).AsString(delimiter);
        }

    }
}