using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.Utilities;

namespace Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives
{
    internal sealed class KeyFramesRuleSet : ComplexItem
    {
        internal SortedRangeList<KeyFramesSelector> Selectors { get; private set; }
        internal RuleBlock RuleBlock { get; private set; }

        public KeyFramesRuleSet()
        {
            Selectors = new SortedRangeList<KeyFramesSelector>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            bool allowSelector = true;
            bool addedUnknownToken = false;

            while (tokens.CurrentToken.TokenType != CssTokenType.OpenCurlyBrace &&
                tokens.CurrentToken.TokenType != CssTokenType.CloseCurlyBrace &&
                !tokens.CurrentToken.IsScopeBlocker())
            {
                KeyFramesSelector kfs = allowSelector ? new KeyFramesSelector() : null;

                if (kfs != null && kfs.Parse(itemFactory, text, tokens))
                {
                    Selectors.Add(kfs);
                    Children.Add(kfs);

                    allowSelector = (kfs.Comma != null);
                    addedUnknownToken = false;
                }
                else
                {
                    // Skip unknown stuff (only the first unknown token in a row will be an error)

                    if (addedUnknownToken)
                    {
                        Children.AddUnknownAndAdvance(itemFactory, text, tokens);
                    }
                    else
                    {
                        Children.AddUnknownAndAdvance(itemFactory, text, tokens,
                            allowSelector ? ParseErrorType.KeyFramesSelectorExpected : ParseErrorType.UnexpectedToken);

                        allowSelector = true;
                        addedUnknownToken = true;
                    }
                }
            }

            KeyFramesSelector lastSelector = (Selectors.Count > 0) ? Selectors[Selectors.Count - 1] : null;

            if (tokens.CurrentToken.TokenType == CssTokenType.OpenCurlyBrace)
            {
                if (Children.Count == 0)
                {
                    Children.AddParseError(ParseErrorType.SelectorBeforeRuleBlockMissing);
                }

                if (lastSelector != null && lastSelector.Comma != null && !addedUnknownToken)
                {
                    Children.AddParseError(ParseErrorType.SelectorAfterCommaMissing);
                }

                RuleBlock rb = itemFactory.CreateSpecific<RuleBlock>(this);

                if (rb.Parse(itemFactory, text, tokens))
                {
                    RuleBlock = rb;
                    Children.Add(rb);
                }
            }
            else if (lastSelector != null)
            {
                AddParseError(ParseErrorType.OpenCurlyBraceMissingForRule, ParseErrorLocation.AfterItem);
            }

            return Children.Count > 0;
        }
    }
}
