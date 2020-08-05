namespace Union.Framework.Service
{
    public class BaseUrlInfo
    {
        public BaseUrlInfo(string subDomain, string domain, string absolutePath)
        {
            SubDomain = subDomain;
            Domain = domain;
            AbsolutePath = absolutePath;
        }

        public string SubDomain { get; }

        public string Domain { get; }

        public string AbsolutePath { get; }

        public BaseUrlInfo ApplyActual(BaseUrlInfo baseUrlInfo)
        {
            var subDomain = baseUrlInfo == null || string.IsNullOrEmpty(baseUrlInfo.SubDomain)
                                ? SubDomain
                                : baseUrlInfo.SubDomain;
            var domain = baseUrlInfo == null || string.IsNullOrEmpty(baseUrlInfo.Domain)
                             ? Domain
                             : baseUrlInfo.Domain;
            var absolutePath = baseUrlInfo == null || string.IsNullOrEmpty(baseUrlInfo.AbsolutePath)
                                   ? AbsolutePath
                                   : baseUrlInfo.AbsolutePath;
            return new BaseUrlInfo(subDomain, domain, absolutePath);
        }

        public string GetBaseUrl()
        {
            var s = Domain + AbsolutePath;
            if (!string.IsNullOrEmpty(SubDomain))
            {
                s = SubDomain + "." + s;
            }
            return s;
        }
    }
}