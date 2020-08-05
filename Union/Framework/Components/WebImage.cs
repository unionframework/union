using OpenQA.Selenium;
using Union.Framework.Components.Interfaces;

namespace Union.Framework.Components
{
    public class WebImage : SimpleWebComponent
    {
        public WebImage(IPage parent, By @by)
            : base(parent, @by)
        {
        }

        public string GetFileName()
        {
            return Get.ImgFileName(By);
        }
    }
}