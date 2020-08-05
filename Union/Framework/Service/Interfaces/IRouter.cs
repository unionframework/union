using Union.Framework.Components.Interfaces;

namespace Union.Framework.Service.Interfaces
{
    public interface IRouter
    {
        RequestData GetRequest(IPage page, BaseUrlInfo defaultBaseUrlInfo);

        IPage GetPage(RequestData requestData, BaseUrlInfo baseUrlInfo);

        bool HasPage(IPage page);
    }
}