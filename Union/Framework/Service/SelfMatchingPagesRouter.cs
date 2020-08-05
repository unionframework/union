using System;
using System.Collections.Generic;
using Union.Framework.Components.Interfaces;
using Union.Framework.Page.Extendable;
using Union.Framework.Service.Extendable;

namespace Union.Framework.Service
{
    public class SelfMatchingPagesRouter : RouterBase
    {
        private readonly Dictionary<Type, ISelfMatchingPage> _pages;


        public SelfMatchingPagesRouter()
        {
            _pages = new Dictionary<Type, ISelfMatchingPage>();
        }

        public override RequestData GetRequest(IPage page, BaseUrlInfo defaultBaseUrlInfo)
        {
            var selfMatchingPage = page as SelfMatchingPageBase;
            if (selfMatchingPage == null)
            {
                return null;
            }
            return selfMatchingPage.GetRequest(defaultBaseUrlInfo);
        }

        public override IPage GetPage(RequestData requestData, BaseUrlInfo baseUrlInfo)
        {
            foreach (var dummyPage in _pages.Values)
            {
                var match = dummyPage.Match(requestData, baseUrlInfo);
                if (match.Success)
                {
                    var instance = (SelfMatchingPageBase)Activator.CreateInstance(dummyPage.GetType());
                    instance.BaseUrlInfo = baseUrlInfo;
                    instance.Data = match.Data;
                    instance.Params = match.Params;
                    instance.Cookies = match.Cookies;
                    return instance;
                }
            }
            return null;
        }

        public override bool HasPage(IPage page)
        {
            return _pages.ContainsKey(page.GetType());
        }

        //        public void RegisterDerivedPages<T>() where T : SelfMatchingPageBase {
        //            Type superType = typeof (T);
        //            Assembly assembly = superType.GetTypeInfo().Assembly;
        //            IEnumerable<Type> derivedTypes =
        //                assembly.DefinedTypes.AsEnumerable().Where(t => !t.GetTypeInfo().IsAbstract && superType.IsAssignableFrom(t));
        //            foreach (Type derivedType in derivedTypes)
        //                RegisterPage(derivedType);
        //        }

        public void RegisterPage<T>()
        {
            RegisterPage(typeof(T));
        }

        private void RegisterPage(Type pageType)
        {
            var pageInstance = (ISelfMatchingPage)Activator.CreateInstance(pageType);
            _pages.Add(pageType, pageInstance);
        }

    }
}