using OpenQA.Selenium;
using Union.Framework.Components.Extendable;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components
{
    public class WebLoader : ComponentBase, IProgressBar
    {
        private readonly By _loaderSelector;

        public WebLoader(IPage parent, By loaderSelector)
            : base(parent)
        {
            _loaderSelector = loaderSelector;
        }

        public override bool IsVisible()
        {
            return Is.Visible(_loaderSelector);
        }

        public void WaitWhileVisible()
        {
            Wait.WhileElementVisible(_loaderSelector);
        }
    }
}