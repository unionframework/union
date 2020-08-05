using System;
using System.Collections.Generic;
using System.Linq;
using Union.Framework.Components.Interfaces;
using Union.Framework.Service.Interfaces;
using Union.Framework.Service.Match;

namespace Union.Framework.Service
{
    public class Web
    {
        private readonly List<IService> _services;

        public Web()
        {
            _services = new List<IService>();
        }

        public ServiceMatchResult MatchService(RequestData request)
        {
            ServiceMatchResult baseDomainMatch = null;
            foreach (var service in _services)
            {
                var baseUrlPattern = service.BaseUrlPattern;
                var result = baseUrlPattern.Match(request.Url.OriginalString);
                if (result.Level == BaseUrlMatchLevel.FullDomain)
                {
                    return new ServiceMatchResult(service, result.GetBaseUrlInfo());
                }
                if (result.Level == BaseUrlMatchLevel.BaseDomain)
                {
                    if (baseDomainMatch != null)
                    {
                        throw new Exception(string.Format("Two BaseDomain matches for url {0}", request.Url));
                    }
                    baseDomainMatch = new ServiceMatchResult(service, result.GetBaseUrlInfo());
                }
            }
            return baseDomainMatch;
        }

        public RequestData GetRequestData(IPage page)
        {
            var service = _services.FirstOrDefault(s => s.Router.HasPage(page));
            if (service == null)
            {
                throw new PageNotRegisteredException(page);
            }
            return service.Router.GetRequest(page, service.DefaultBaseUrlInfo);
        }

        public void RegisterService(IServiceFactory serviceFactory)
        {
            var service = serviceFactory.CreateService();
            _services.Add(service);
        }

    }
}