using OpenQA.Selenium;
using Union.Framework.Enums;

namespace Union.Framework.Browser
{
    public class BrowserJs : DriverFacade
    {
        public BrowserJs(Browser browser)
            : base(browser)
        {
        }

        public T Excecute<T>(string js) => (T) Excecute(js);

        public object Excecute(string js, params object[] args)
        {
            var excecutor = Driver as IJavaScriptExecutor;
            js = string.Format(js, args);
            return excecutor.ExecuteScript(js);
        }

        public string GetEventHandlers(string css, JsEventType eventType)
        {
            var js = string.Format(@"var handlers= $._data($('{0}').get(0),'events').{1};
                          var s='';
                          for(var i=0;i<handlers.length;i++)
                            s+=handlers[i].handler.toString();
                          return s;", css, eventType);
            return Excecute<string>(js);
        }

        public bool IsPageBottom() =>
            Excecute<bool>(
                "return document.body.scrollHeight==="
                + "document.body.scrollTop+document.documentElement.clientHeight");

        public void ScrollToBottom() =>
            Excecute(@"window.scrollTo(0,
                                       Math.max(document.documentElement.scrollHeight,
                                                document.body.scrollHeight,
                                                document.documentElement.clientHeight));");

        public void ScrollToTop() => Excecute(@"window.scrollTo(0,0);");
    }
}