using Union.Framework.Browser;
using Union.Framework.Components.Interfaces;
using Union.Framework.Page;
using Union.Logging;

namespace Union.Tests
{
    public abstract class TestBase<P>
        where P : IPage
    {
        protected P Page { get; set; }

        protected abstract Browser Browser { get; }

        protected abstract ITestLogger Log { get; }

        protected void up()
        {
            Page = Browser.State.PageAs<P>();
        }
    }
}