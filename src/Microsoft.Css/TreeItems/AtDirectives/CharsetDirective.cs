using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Shared.Text;

namespace Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives
{
    /// <summary>
    /// Character set directive (@charset)
    /// </summary>
    internal sealed class CharsetDirective : AtLineDirective
    {
        /// <summary>
        /// Token that represents character set string
        /// </summary>
        internal TokenItem CharacterSet { get; private set; }

        public CharsetDirective()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            // @charset "Shift_JIS";
            ParseAtAndKeyword(itemFactory, text, tokens);

            // In case semicolon is missing we don't want to continue parsing up
            // until next semicolon (although technically it is correct per W3C
            // @ directive definition. If we've seen charset spec we'll stop at the next
            // token that is not typically part of the @charset syntax
            if (tokens.CurrentToken.IsString())
            {
                CharacterSet = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.CharsetName);
            }
            else
            {
                Children.AddParseError(ParseErrorType.EncodingMissing);
            }

            CheckSemicolon(tokens);

            return Children.Count > 0;
        }
    }
}
