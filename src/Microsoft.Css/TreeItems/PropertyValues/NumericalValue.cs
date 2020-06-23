using System.Diagnostics;
using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Shared.Text;

namespace Microsoft.WebTools.Languages.Css.TreeItems.PropertyValues
{
    /// <summary>
    /// Numerical property value
    /// </summary>
    public class NumericalValue : ComplexItem
    {
        public TokenItem Number { get; private set; }

        internal override bool TreatAsWord => true;

        public NumericalValue()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        internal static ParseItem ParseNumber(ComplexItem parent, ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            ParseItem pv;

            if (tokens.Peek(1).TokenType == CssTokenType.Units)
            {
                pv = itemFactory.Create<UnitValue>(parent);
            }
            else
            {
                pv = itemFactory.Create<NumericalValue>(parent);
            }

            if (pv != null && !pv.Parse(itemFactory, text, tokens))
            {
                pv = null;
            }

            return pv;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            Debug.Assert(tokens.CurrentToken.TokenType == CssTokenType.Number);

            if (tokens.CurrentToken.TokenType != CssTokenType.Number)
            {
                return false;
            }

            Number = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.Number);

            return Children.Count > 0;
        }
    }
}
