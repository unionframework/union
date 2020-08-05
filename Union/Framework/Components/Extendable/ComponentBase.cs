using System;
using NUnit.Framework;
using Union.Framework.Browser;
using Union.Framework.Components.Interfaces;
using Union.Logging;

namespace Union.Framework.Components.Extendable
{
    public abstract class ComponentBase : IComponent
    {
        private string _componentName;

        protected ComponentBase(IPage parent)
        {
            ParentPage = parent;
        }

        public void WaitForVisible()
        {
            Wait.Until(IsVisible);
        }

        public void AssertVisible()
        {
            Assert.IsTrue(IsVisible(), "'{0}' не отображается", ComponentName);
        }

        public void AssertNotVisible()
        {
            Assert.IsFalse(IsVisible(), "'{0}' is not displayed.", ComponentName);
        }

        public virtual ComponentBase Open(Action action)
        {
            if (!IsVisible())
            {
                action.Invoke();
            }

            if (!IsVisible())
            {
                throw new Exception($"Component '{ComponentName}' did not open.");
            }

            return this;
        }

        public T Open<T>(Action action) where T : ComponentBase
        {
            return (T)Open(action);
        }

        #region IComponent Members

        public virtual string ComponentName
        {
            get
            {
                _componentName = _componentName ?? (_componentName = GetType().Name);
                return _componentName;
            }
            set
            {
                _componentName = value;
            }
        }

        public IPage ParentPage { get; }

        public abstract bool IsVisible();

        public Browser.Browser Browser
        {
            get
            {
                return ParentPage.Browser;
            }
        }

        public ITestLogger Log
        {
            get
            {
                return ParentPage.Log;
            }
        }

        public BrowserAction Action
        {
            get
            {
                return Browser.Action;
            }
        }

        public BrowserAlert Alert
        {
            get
            {
                return Browser.Alert;
            }
        }

        public BrowserFind Find
        {
            get
            {
                return Browser.Find;
            }
        }

        public BrowserGet Get
        {
            get
            {
                return Browser.Get;
            }
        }

        public BrowserGo Go
        {
            get
            {
                return Browser.Go;
            }
        }

        public BrowserIs Is
        {
            get
            {
                return Browser.Is;
            }
        }

        public BrowserState State
        {
            get
            {
                return Browser.State;
            }
        }

        public BrowserWait Wait
        {
            get
            {
                return Browser.Wait;
            }
        }

        public BrowserJs Js
        {
            get
            {
                return Browser.Js;
            }
        }

        public BrowserWindow Window
        {
            get
            {
                return Browser.Window;
            }
        }

        BrowserCookies IPageObject.Cookies
        {
            get
            {
                return Browser.Cookies;
            }
        }

        #endregion
    }
}