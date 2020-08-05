using System;

namespace Union.Framework.Attributes
{
    public class WebComponentAttribute : Attribute, IComponentAttribute
    {
        public WebComponentAttribute(params object[] args)
        {
            Args = args;
        }

        #region IComponentArgs Members

        public object[] Args { get; }

        public string ComponentName { get; set; }

        #endregion
    }
}