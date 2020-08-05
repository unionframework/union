using System.Collections.Generic;
using System.Linq;

namespace Union.Framework.Service.Match
{
    public class BaseUrlRegexBuilder
    {
        private readonly string _domainPattern;

        private string _absolutePathPattern = "";

        private string _subDomainPattern = "((?<optionalsubdomain>[^\\.]+)\\.)?";

        public BaseUrlRegexBuilder(string domain)
            : this(new List<string> { domain })
        {
        }

        public BaseUrlRegexBuilder(List<string> domains)
        {
            _domainPattern = GenerateDomainsPattern(domains);
        }

        private string GenerateDomainsPattern(List<string> domains)
        {
            var s = domains.Aggregate("", (current, domain) => current + domain + "|");
            s = s.Substring(0, s.Length - 1);
            s = s.Replace(".", "\\.");
            return string.Format("(?<domain>({0}))", s);
        }

        public void SetSubDomain(string value)
        {
            _subDomainPattern = string.Format("(?<subdomain>{0})\\.", value);
        }

        public void SetAbsolutePathPattern(string pattern)
        {
            _absolutePathPattern = string.Format("(?<abspath>\\/{0})", pattern);
        }

        public string Build()
        {
            return "(http(|s)://|)(www.|)" + _subDomainPattern + _domainPattern + _absolutePathPattern
                   + ".*";
        }
    }
}