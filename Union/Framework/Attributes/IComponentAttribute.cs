namespace Union.Framework.Attributes
{
    public interface IComponentAttribute
    {
        object[] Args { get; }

        string ComponentName { get; set; }
    }
}