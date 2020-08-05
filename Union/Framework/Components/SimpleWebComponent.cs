using OpenQA.Selenium;
using Union.Framework.Components.Extendable;
using Union.Framework.Components.Interfaces;
using Union.SCSS;

namespace Union.Framework.Components
{
    public abstract class SimpleWebComponent : ContainerBase
    {
        public readonly By By;

        protected SimpleWebComponent(IPage parent, By by)
            : base(parent)
        {
            By = by;
        }

        protected SimpleWebComponent(IPage parent, string rootScss)
            : base(parent, rootScss)
        {
            By = ScssBuilder.CreateBy(rootScss);
        }

        public override bool IsVisible()
        {
            return Is.Visible(By);
        }
    }
}