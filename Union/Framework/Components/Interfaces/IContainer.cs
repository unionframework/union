namespace Union.Framework.Components.Interfaces
{
    public interface IContainer : IComponent
    {
        string InnerScss(string relativeScss, params object[] args);
    }
}