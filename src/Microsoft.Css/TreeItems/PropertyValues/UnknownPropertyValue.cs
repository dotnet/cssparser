using System.Diagnostics;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems.PropertyValues
{
    /// <summary>
    /// Unknown (generic) property value
    /// </summary>
    internal sealed class UnknownPropertyValue : ComplexItem
    {
        public UnknownPropertyValue()
        {
        }

        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            Debug.Fail("UnknownPropertyValue.Parse whould never be called as this class is a temporary object to pass over to external factory");
            return false;
        }
    }
}
