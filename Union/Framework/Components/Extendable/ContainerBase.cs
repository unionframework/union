using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;
using Union.SCSS;

namespace Union.Framework.Components.Extendable
{
    public abstract class ContainerBase : ComponentBase, IContainer
    {
        private string _rootScss;

        protected ContainerBase(IPage parent)
            : this(parent, null)
        {
        }

        protected ContainerBase(IPage parent, string rootScss)
            : base(parent)
        {
            _rootScss = rootScss;
        }

        protected virtual string RootScss
        {
            get
            {
                return _rootScss ?? (_rootScss = "html");
            }
        }

        protected By RootSelector
        {
            get
            {
                return ScssBuilder.CreateBy(RootScss);
            }
        }

        public override bool IsVisible()
        {
            return Is.Visible(RootSelector);
        }

        /// <summary>
        ///     �������� Scss ��� ���������� ��������
        /// </summary>
        public string InnerScss(string relativeScss, params object[] args)
        {
            relativeScss = string.Format(relativeScss, args);
            return ScssBuilder.Concat(RootScss, relativeScss).Value;
        }

        /// <summary>
        ///     �������� �������� ��� ���������� ��������
        /// </summary>
        public By InnerSelector(string relativeScss, params object[] args)
        {
            relativeScss = string.Format(relativeScss, args);
            return ScssBuilder.Concat(RootScss, relativeScss).By;
        }

        /// <summary>
        ///     �������� �������� ��� ���������� ��������
        /// </summary>
        public By InnerSelector(Scss innerScss)
        {
            var rootScss = ScssBuilder.Create(RootScss);
            return rootScss.Concat(innerScss).By;
        }
    }
}