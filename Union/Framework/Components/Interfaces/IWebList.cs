using System.Collections.Generic;

namespace Union.Framework.Components.Interfaces
{
    public interface IWebList<T> : IWebList
        where T : IItem
    {
        string ItemIdScss { get; }

        List<string> GetIds();

        List<T> GetItems();
    }

    public interface IWebList
    {
        List<T> GetItems<T>();
    }
}