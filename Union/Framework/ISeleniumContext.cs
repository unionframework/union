using Union.Framework.Service;
using Union.Logging;

namespace Union.Framework
{
    public interface ISeleniumContext
    {
        Web Web { get; }

        ITestLogger Log { get; }

        Browser.Browser Browser { get; }
    }
}