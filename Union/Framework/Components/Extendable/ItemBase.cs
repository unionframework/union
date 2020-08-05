using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components.Extendable
{
    public abstract class ItemBase : ContainerBase, IItem
    {
        protected readonly IContainer Container;

        protected ItemBase(IContainer container, string id)
            : base(container.ParentPage)
        {
            Container = container;
            ID = id;
        }

        protected override string RootScss
        {
            get
            {
                return ItemScss;
            }
        }

        protected string ContainerInnerScss(string relativeXpath, params object[] args)
        {
            return Container.InnerScss(relativeXpath, args);
        }

        #region IItem Members

        public string ID { get; }

        public abstract string ItemScss { get; }

        #endregion
    }
}