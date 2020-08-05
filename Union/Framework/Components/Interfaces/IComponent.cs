namespace Union.Framework.Components.Interfaces
{
    public interface IComponent : IPageObject
    {
        IPage ParentPage { get; }

        string ComponentName { get; set; }

        bool IsVisible();
    }
}