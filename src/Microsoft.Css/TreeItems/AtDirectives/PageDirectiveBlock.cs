using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.Utilities;
using Microsoft.WebTools.Languages.Shared.Text;

namespace Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives
{
    /// <summary>
    /// Adds @margin directive support to normal rule blocks
    /// </summary>
    internal sealed class PageDirectiveBlock : RuleBlock
    {
        internal SortedRangeList<MarginDirective> Margins { get; private set; }

        internal PageDirectiveBlock()
        {
            Margins = new SortedRangeList<MarginDirective>();
        }

        protected override ParseItem CreateDirective(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            MarginDirective marginDirective = new MarginDirective();

            if (marginDirective.Parse(itemFactory, text, tokens))
            {
                return marginDirective;
            }
            else
            {
                return UnknownItem.ParseUnknown(this, itemFactory, text, tokens, ParseErrorType.UnexpectedParseError);
            }
        }

        public override void UpdateCachedChildren()
        {
            Margins.Clear();

            foreach (ParseItem child in Children)
            {
                if (child is MarginDirective)
                {
                    Margins.Add((MarginDirective)child);
                }
            }

            base.UpdateCachedChildren();
        }
    }
}
