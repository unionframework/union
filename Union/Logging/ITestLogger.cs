using System;
using OpenQA.Selenium;

namespace Union.Logging
{
    public interface ITestLogger
    {
        void Action(string msg, params object[] args);

        void Info(string msg, params object[] args);

        void FatalError(string s, Exception e);

        void Selector(By by);

        void Exception(Exception exception);
    }
}