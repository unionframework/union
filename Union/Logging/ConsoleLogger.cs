using System;
using OpenQA.Selenium;

namespace Union.Logging
{
    public class ConsoleLogger : ITestLogger
    {

        #region ITestLogger Members

        public void Action(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            Console.WriteLine(msg);
        }

        public void Info(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            Console.WriteLine(msg);
        }

        public void FatalError(string msg, Exception e)
        {
            Info(msg, e);
            Console.WriteLine(msg);
        }

        public void Selector(By by)
        {
            Console.WriteLine("By: {0}", by);
        }

        public void Exception(Exception exception)
        {
            Console.WriteLine("Exception: {0}", exception.Message);
        }

        #endregion
    }
}