using System;
using Union.Framework.Browser;
using Union.Framework.Enums;
using Union.Framework.Service;
using Union.Logging;

namespace Union.Framework
{
    public class SeleniumContext<T> : ISeleniumContext
        where T : ISeleniumContext
    {
        private readonly BrowsersCache _browsersCache;

        protected SeleniumContext()
        {
            Log = new ConsoleLogger();
            Web = new Web();
            _browsersCache = new BrowsersCache(Web, Log);
        }

        public static T Inst => SingletonCreator<T>.CreatorInstance;

        protected virtual void InitWeb()
        {
        }

        public virtual void Init()
        {
            InitWeb();
        }

        public void Destroy()
        {
            Inst.Browser.Destroy();
        }

        #region Nested type: SingletonCreator

        private sealed class SingletonCreator<S>
            where S : ISeleniumContext
        {
            //            (S).GetConstructor(
            //                BindingFlags.Instance | BindingFlags.NonPublic,
            //                null,
            //                new Type[0],
            //                new ParameterModifier[0]).Invoke(null);

            public static S CreatorInstance { get; } = (S) Activator.CreateInstance(typeof(S));
        }

        #endregion

        #region ISeleniumContext Members

        public Web Web { get; }

        public ITestLogger Log { get; }

        public Browser.Browser Browser => _browsersCache.GetBrowser(BrowserType.CHROME);

        #endregion
    }
}