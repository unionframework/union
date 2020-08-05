using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components.Extendable
{
    public abstract class DropListBase : ContainerBase, IDropList
    {
        public DropListBase(IPage parent, string rootScss)
            : base(parent, rootScss)
        {
        }

        public abstract By GetItemSelector(string name);

        public abstract string ItemNameScss { get; }

        public List<string> GetItems()
        {
            return Get.Texts(ItemNameScss);
        }

        public void AssertContains(string item)
        {
            Assert.IsTrue(Contains(item));
        }

        /// <summary>
        ///     Содержит ли список указанное значение
        /// </summary>
        private bool Contains(string item)
        {
            return GetItems().Contains(item);
        }
    }
}