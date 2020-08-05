using NUnit.Framework;
using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components
{
    public class WebText : SimpleWebComponent
    {
        public WebText(IPage parent, By by)
            : base(parent, by)
        {
        }

        public WebText(IPage parent, string rootScss)
            : base(parent, rootScss)
        {
        }

        public string Text
        {
            get
            {
                return Get.Text(By);
            }
        }

        public void AssertIsVisible()
        {
            Assert.IsTrue(IsVisible(), "{0} �� ������������", ComponentName);
        }

        public void AssertIsHidden()
        {
            Assert.IsFalse(IsVisible(), "{0} ������������", ComponentName);
        }

        public void AssertEqual(string expected)
        {
            Assert.AreEqual(
                expected,
                Text,
                "����� ���������� '{0}' �� ������������� ����������",
                ComponentName);
        }

        public void Click(int sleepTimeout = 0)
        {
            Log.Action("���� �� ������������ '{0}'", ComponentName);
            Action.Click(By, sleepTimeout);
        }

        public void ClickAndWaitWhileAjaxRequests()
        {
            Log.Action("���� �� ������������ '{0}'", ComponentName);
            Action.ClickAndWaitWhileAjax(By);
        }

        public void ClickAndWaitForRedirect()
        {
            Log.Action("���� �� ������������ '{0}'", ComponentName);
            Action.ClickAndWaitForRedirect(By);
        }

        public T Value<T>()
        {
            return Get.Value<T>(By);
        }

        public void DragByHorizontal(int pixels)
        {
            Log.Action("Drag '{0}' at {1} pixels", ComponentName, pixels);
            Action.DragByHorizontal(By, pixels);
        }
    }
}