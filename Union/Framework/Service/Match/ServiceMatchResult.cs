using Union.Framework.Service.Interfaces;

namespace Union.Framework.Service.Match
{
    public class ServiceMatchResult
    {
        private readonly BaseUrlInfo _baseUrlInfo;

        private readonly IService _service;

        public ServiceMatchResult(IService service, BaseUrlInfo baseUrlInfo)
        {
            _service = service;
            _baseUrlInfo = baseUrlInfo;
        }

        public IService GetService()
        {
            return _service;
        }

        public BaseUrlInfo GetBaseUrlInfo()
        {
            return _baseUrlInfo;
        }
    }
}