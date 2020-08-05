using Union.Framework.Service.Interfaces;
using Union.Framework.Service.Match;

namespace Union.Framework.Service
{
    public interface IServiceFactory
    {
        IRouter CreateRouter();

        IService CreateService();

        BaseUrlPattern CreateBaseUrlPattern();

        BaseUrlInfo GetDefaultBaseUrlInfo();
    }
}