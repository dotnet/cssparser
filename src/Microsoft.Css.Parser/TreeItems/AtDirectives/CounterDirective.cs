// http://www.w3.org/TR/css3-content/#counter1

using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives
{
    internal sealed class CounterDirective : AtBlockDirective
    {
        internal RuleBlock RuleBlock { get; private set; }

        public CounterDirective()
        {
        }

        internal override BlockItem Block
        {
            get { return RuleBlock; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            ParseAtAndKeyword(itemFactory, text, tokens);

            // The syntax in the CSS spec isn't defined yet, just eat identifiers
            while (tokens.CurrentToken.TokenType == CssTokenType.Identifier)
            {
                Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.AtDirectiveKeyword);
            }

            RuleBlock = itemFactory.CreateSpecific<RuleBlock>(this);

            if (!ParseBlock(RuleBlock, itemFactory, text, tokens))
            {
                RuleBlock = null;
            }

            return Children.Count > 0;
        }
    }
}
