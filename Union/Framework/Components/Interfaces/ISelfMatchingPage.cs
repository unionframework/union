using Union.Framework.Page;
using Union.Framework.Page.Match;
using Union.Framework.Service;

namespace Union.Framework.Components.Interfaces
{
    public interface ISelfMatchingPage
    {
        UriMatchResult Match(RequestData requestData, BaseUrlInfo baseUrlInfo);
    }
}