namespace Union.Framework.Attributes
{
    public sealed class SimpleWebComponentAttribute : FindsByAttribute, IComponentAttribute
    {
        private string _componentName;

        public string ComponentName
        {
            get =>
                string.IsNullOrEmpty(_componentName) && !string.IsNullOrEmpty(LinkText)
                    ? LinkText
                    : _componentName;
            set => _componentName = value;
        }

        #region IComponentArgs Members

        public object[] Args
        {
            get
            {
                return new object[] { Finder };
            }
        }

        #endregion
    }
}