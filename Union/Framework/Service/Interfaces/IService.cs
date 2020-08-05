/**
 * Created by VolkovA on 27.02.14.
 */

using Union.Framework.Components.Interfaces;
using Union.Framework.Service.Match;

namespace Union.Framework.Service.Interfaces
{
    public interface IService
    {
        IRouter Router { get; }

        BaseUrlPattern BaseUrlPattern { get; }

        BaseUrlInfo DefaultBaseUrlInfo { get; }

        IPage GetPage(RequestData requestData, BaseUrlInfo baseUrlInfo);

        RequestData GetRequestData(IPage page);
    }
}