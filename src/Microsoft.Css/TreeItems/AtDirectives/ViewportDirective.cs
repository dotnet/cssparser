using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Shared.Text;

namespace Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives
{
    internal sealed class ViewportDirective : AtBlockDirective
    {
        public RuleBlock RuleBlock { get; private set; }

        internal override BlockItem Block
        {
            get { return RuleBlock; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            ParseAtAndKeyword(itemFactory, text, tokens);

            RuleBlock = itemFactory.CreateSpecific<RuleBlock>(this);

            if (!ParseBlock(RuleBlock, itemFactory, text, tokens))
            {
                RuleBlock = null;
            }

            return Children.Count > 0;
        }
    }
}
