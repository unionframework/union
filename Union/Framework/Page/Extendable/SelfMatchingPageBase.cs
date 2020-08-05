using Union.Framework.Components.Interfaces;
using Union.Framework.Page.Match;
using Union.Framework.Service;

namespace Union.Framework.Page.Extendable
{
    public abstract class SelfMatchingPageBase : PageBase, ISelfMatchingPage
    {
        public abstract string AbsolutePath { get; }

        #region ISelfMatchingPage Members

        public virtual UriMatchResult Match(RequestData requestData, BaseUrlInfo baseUrlInfo)
        {
            return new UriMatcher(AbsolutePath, Data, Params).Match(
                requestData.Url,
                baseUrlInfo.AbsolutePath);
        }

        #endregion

        public virtual RequestData GetRequest(BaseUrlInfo defaultBaseUrlInfo)
        {
            var url =
                new UriAssembler(BaseUrlInfo, AbsolutePath, Data, Params).Assemble(
                    defaultBaseUrlInfo);
            return new RequestData(url);
        }
    }
}