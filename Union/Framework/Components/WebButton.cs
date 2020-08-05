using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components
{
    public class WebButton : SimpleWebComponent, IClickable
    {
        public WebButton(IPage parent, By by)
            : base(parent, by)
        {
        }

        #region IClickable Members

        public void Click(int sleepTimeout = 0)
        {
            Log.Action("Клик по кнопке '{0}'", ComponentName);
            Action.Click(By, sleepTimeout);
        }

        #endregion

        public void ClickAndWaitWhileAjax(int sleepTimeout = 0, bool ajaxInevitable = false)
        {
            Log.Action("Клик по кнопке '{0}'", ComponentName);
            Action.ClickAndWaitWhileAjax(By, sleepTimeout, ajaxInevitable);
        }

        public void ClickAndWaitForRedirect()
        {
            Log.Action("Клик по кнопке '{0}'", ComponentName);
            Action.ClickAndWaitForRedirect(By);
        }

        public void ClickAndWaitWhileProgress()
        {
            Log.Action("Клик по кнопке '{0}'", ComponentName);
            Action.ClickAndWaitWhileProgress(By, 1000);
        }
    }
}