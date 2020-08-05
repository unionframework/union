using OpenQA.Selenium;

namespace Union.Framework.Driver
{
    public interface IDriverManager
    {
        void InitDriver();

        IWebDriver GetDriver();

        void DestroyDriver();
    }
}