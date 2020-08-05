using NUnit.Framework;
using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components
{
    public class WebInput : SimpleWebComponent
    {
        public WebInput(IPage parent, By @by)
            : base(parent, @by)
        {
        }

        public WebInput(IPage parent, string rootScss)
            : base(parent, rootScss)
        {
        }

        public virtual string Text
        {
            get
            {
                return Get.InputValue(By);
            }
        }

        /// <summary>
        ///     Ввести значение в поле ввода
        /// </summary>
        /// <param name="value">значение</param>
        public virtual void TypeIn(object value)
        {
            Log.Action("Вводим '{0}' в поле '{1}'", value, ComponentName);
            Action.TypeIn(By, value);
        }

        /// <summary>
        ///     Проверить что поле ввода не содержит текста
        /// </summary>
        public void AssertIsEmpty()
        {
            Assert.IsTrue(IsEmpty(), "Поле '{0}' не пустое", ComponentName);
        }

        public bool IsEmpty()
        {
            return string.IsNullOrWhiteSpace(Text);
        }

        /// <summary>
        ///     Проверить что поле ввода содержит текст
        /// </summary>
        public void AssertIsNotEmpty()
        {
            Assert.IsFalse(string.IsNullOrWhiteSpace(Text), string.Format("Поле '{0}' пустое", ComponentName));
        }


        /// <summary>
        ///     Удалить последний символ в поле ввода
        /// </summary>
        public void RemoveLast(bool changeFocus = false)
        {
            Action.PressKey(By, Keys.End);
            Action.PressKey(By, Keys.Backspace);
            if (changeFocus)
            {
                Action.ChangeFocus();
            }
        }

        /// <summary>
        ///     Очистить текстовое поле
        /// </summary>
        public virtual void Clear()
        {
            Action.Clear(By);
        }

        public void TypeInAndSubmit(string query)
        {
            TypeIn(query);
            Action.PressEnter(By);
            Wait.WhileAjax();
        }

        public void AssertEqual(string expected)
        {
            Assert.AreEqual(expected, Text, "Некоректное значение в поле '{0}'", ComponentName);
        }
    }
}