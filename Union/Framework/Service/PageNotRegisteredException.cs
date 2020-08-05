using System;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Service
{
    public class PageNotRegisteredException : Exception
    {
        public PageNotRegisteredException(IPage page)
            : base($"There are not services with registered page of type {page.GetType().Name}")
        {
        }
    }
}