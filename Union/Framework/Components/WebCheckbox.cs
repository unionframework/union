using NUnit.Framework;
using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components
{
    public class WebCheckbox : SimpleWebComponent, IWebCheckbox
    {
        public WebCheckbox(IPage parent, By by)
            : base(parent, by)
        {
        }

        public WebCheckbox(IPage parent, string rootScss)
            : base(parent, rootScss)
        {
        }

        public void Select()
        {
            if (!Is.Checked(By))
            {
                Log.Action("Устанавливаем чекбокс {0}", ComponentName);
                Action.Click(By);
            }
        }

        /// <summary>
        ///     Снять галку из чекбокса
        /// </summary>
        public void Deselect()
        {
            if (Checked())
            {
                Log.Action("Снимаем чекбокс {0}", ComponentName);
                Action.Click(By);
            }
        }

        public bool Checked()
        {
            return Is.Checked(By);
        }

        public void AssertIsDisabled()
        {
            Assert.AreEqual("disabled", Get.Attr(By, "disabled"), "Чекбокс активен");
        }
    }
}