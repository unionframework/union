using Union.Framework.Components.Interfaces;
using Union.Framework.Service.Interfaces;

namespace Union.Framework.Service.Extendable
{
    public abstract class RouterBase : IRouter
    {
        public abstract RequestData GetRequest(IPage page, BaseUrlInfo defaultBaseUrlInfo);

        public abstract IPage GetPage(RequestData requestData, BaseUrlInfo baseUrlInfo);

        public abstract bool HasPage(IPage page);
    }
}