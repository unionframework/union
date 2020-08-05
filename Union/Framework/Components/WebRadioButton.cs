using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components
{
    public class WebRadioButton : SimpleWebComponent
    {
        public WebRadioButton(IPage parent, By @by)
            : base(parent, @by)
        {
        }

        public virtual void SelectAndWaitWhileAjaxRequests(int sleepTimeout = 0, bool ajaxInevitable = false)
        {
            Log.Action("Select radio button '{0}'", ComponentName);
            Action.ClickAndWaitWhileAjax(By, sleepTimeout, ajaxInevitable);
        }

        public virtual void Select(int sleepTimeout = 0)
        {
            Log.Action("Select radio button '{0}'", ComponentName);
            Action.Click(By, sleepTimeout);
        }
    }
}