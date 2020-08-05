using System.Collections.Generic;
using OpenQA.Selenium;

namespace Union.Framework.Components.Interfaces
{
    public interface IDropList
    {
        string ItemNameScss { get; }

        List<string> GetItems();

        By GetItemSelector(string name);
    }
}