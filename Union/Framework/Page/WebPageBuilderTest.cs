using System;
using NUnit.Framework;
using Union.Framework.Attributes;
using Union.Framework.Components.Extendable;
using Union.Framework.Components.Interfaces;
using Union.Framework.Page.Extendable;

namespace Union.Framework.Page
{
    [TestFixture]
    public class WebPageBuilderTest
    {
        private class Container : ContainerBase
        {
            [WebComponent("root:div[text()='mytext']")]
            public Component Component1;

            [WebComponent("//div[text()='mytext']")]
            public Component Component2;

            public Container(IPage parent, string rootScss)
                : base(parent, rootScss)
            {
            }
        }

        private class Component : ComponentBase
        {
            public readonly string Xpath;

            public Component(IPage page, string xpath)
                : base(page)
            {
                Xpath = xpath;
            }

            public override bool IsVisible()
            {
                throw new NotImplementedException();
            }
        }

        private class Page : PageBase
        {
        }

        [Test]
        public void DoNotAddRootWithouPrefix()
        {
            var page = new Page();
            var container = new Container(page, "//*[@id='rootelementid']");
            WebPageBuilder.InitComponents(page, container);
            Assert.AreEqual(
                "//div[text()" + "='mytext']",
                container.Component2.Xpath,
                "ќтносительный xpath не преобразовалс€ в абсолютный");
        }

        [Test]
        public void ReplacePrefixWithRootSelector()
        {
            var page = new Page();
            var container = new Container(page, "//*[@id='rootelementid']");
            WebPageBuilder.InitComponents(page, container);
            Assert.AreEqual(
                "//*[@id='rootelementid']/descendant::div[text()='mytext']",
                container.Component1.Xpath,
                "ќтносительный xpath не преобразовалс€ в абсолютный");
        }
    }
}