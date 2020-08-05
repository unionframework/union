namespace Union.Framework.Browser
{
    public class BrowserOptions
    {
        public const bool FINDSINGLE_DEFAULT = true;

        public const bool WAIT_WHILE_AJAX_BEFORE_CLICK = true;

        public bool FindSingle = FINDSINGLE_DEFAULT;

        public bool WaitWhileAjaxBeforeClick = WAIT_WHILE_AJAX_BEFORE_CLICK;

        public object Clone()
        {
            var options = new BrowserOptions
            {
                FindSingle = FindSingle,
                WaitWhileAjaxBeforeClick = WaitWhileAjaxBeforeClick
            };
            return options;
        }
    }
}