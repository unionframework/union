using Union.Framework.Components.Interfaces;
using Union.Framework.Service.Interfaces;
using Union.Framework.Service.Match;

namespace Union.Framework.Service.Extendable
{
    public abstract class ServiceBase : IService
    {
        public ServiceBase(BaseUrlInfo defaultBaseUrlInfo, BaseUrlPattern baseUrlPattern, IRouter router)
        {
            DefaultBaseUrlInfo = defaultBaseUrlInfo;
            BaseUrlPattern = baseUrlPattern;
            Router = router;
        }

        public IPage GetPage(RequestData requestData, BaseUrlInfo baseUrlInfo)
        {
            return Router.GetPage(requestData, baseUrlInfo);
        }

        public RequestData GetRequestData(IPage page)
        {
            return Router.GetRequest(page, DefaultBaseUrlInfo);
        }

        #region Service Members

        public IRouter Router { get; }

        public BaseUrlPattern BaseUrlPattern { get; }

        public BaseUrlInfo DefaultBaseUrlInfo { get; }

        #endregion
    }
}