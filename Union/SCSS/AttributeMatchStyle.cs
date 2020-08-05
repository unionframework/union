using Union.Utils.Extensions;

namespace Union.SCSS
{
    internal enum AttributeMatchStyle
    {
        [StringValue("=")]
        Equal,

        [StringValue("~")]
        Contains
    }
}