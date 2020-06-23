using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems.PropertyValues;

namespace Microsoft.WebTools.Languages.Css.TreeItems.Functions
{
    internal class FunctionArgument : ComplexItem
    {
        internal ParseItemList ArgumentItems { get; private set; }
        public TokenItem Comma { get; protected set; }

        public FunctionArgument()
        {
            ArgumentItems = new ParseItemList();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            ParseArgument(itemFactory, text, tokens);

            if (tokens.CurrentToken.TokenType == CssTokenType.Comma)
            {
                Comma = Children.AddCurrentAndAdvance(tokens, null);

                if (ArgumentItems.Count == 0)
                {
                    Comma.AddParseError(ParseErrorType.FunctionArgumentMissing, ParseErrorLocation.BeforeItem);
                }
            }

            return Children.Count > 0;
        }

        protected virtual void ParseArgument(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            while (!tokens.CurrentToken.IsFunctionArgumentTerminator())
            {
                ParseItem pi = PropertyValueHelpers.ParsePropertyValue(this, itemFactory, text, tokens);

                if (pi != null)
                {
                    ArgumentItems.Add(pi);
                    Children.Add(pi);
                }
                else
                {
                    // An unknown item is not an error
                    pi = Children.AddUnknownAndAdvance(itemFactory, text, tokens);
                    ArgumentItems.Add(pi);
                }
            }
        }
    }
}
