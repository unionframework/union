using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Union.Framework.Service
{
    public class RequestData
    {
        public RequestData(string url)
            : this(url, new List<Cookie>())
        {
        }

        public RequestData(string url, List<Cookie> cookies)
        {
            Url = new Uri(url);
            Cookies = cookies;
        }

        public Uri Url { get; }

        public List<Cookie> Cookies { get; }
    }
}