using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems.Selectors
{
    /// <summary>
    /// CSS class selector, like A.foo
    /// </summary>
    public class ClassSelector : ComplexItem // E.foo
    {
        internal TokenItem Dot { get; private set; }
        public ParseItem ClassName { get; protected set; }

        public ClassSelector()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            if (tokens.CurrentToken.TokenType == CssTokenType.Dot)
            {
                Dot = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.ClassSelector);
                ParseName(itemFactory, text, tokens);
            }

            return Children.Count > 0;
        }

        protected virtual void ParseName(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            if ((tokens.CurrentToken.TokenType == CssTokenType.Identifier ||
                tokens.CurrentToken.TokenType == CssTokenType.Asterisk) &&
                tokens.CurrentToken.Start == Dot.AfterEnd)
            {
                ClassName = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.ClassSelector);
            }
            else
            {
                Dot.AddParseError(ParseErrorType.ClassNameMissing, ParseErrorLocation.AfterItem);
            }
        }

        /// <summary>
        /// True if selector is well-formed, i.e. it has dot, name and base class is valid.
        /// </summary>
        public override bool IsValid
        {
            get { return Dot != null && ClassName != null && base.IsValid; }
        }
    }
}
