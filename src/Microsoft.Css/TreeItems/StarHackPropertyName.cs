using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems
{
    /// <summary>
    /// Normally a property name is just a single indentifier token.
    /// But some people put an asterisk in front of an identifier to invalidate them.  This technique
    /// is called the "Star Hack" and used to target IE versions below 7.
    /// This class will hold onto the asterisk as well as the identifier.
    /// </summary>
    internal sealed class StarHackPropertyName : ComplexItem
    {
        internal StarHackPropertyName()
        {
        }

        /// <summary>
        /// This invalidates the declaration
        /// </summary>
        public override bool IsValid
        {
            get { return false; }
        }

        /// <summary>
        /// Parse: property, -property, *property, or _property
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            Context = CssClassifierContextCache.FromTypeEnum(CssClassifierContextType.PropertyName);

            if (tokens.CurrentToken.TokenType == CssTokenType.Asterisk &&
                tokens.Peek(1).TokenType == CssTokenType.Identifier)
            {
                Context = CssClassifierContextCache.FromTypeEnum(CssClassifierContextType.CustomPropertyName);
                Children.AddCurrentAndAdvance(tokens, Context);
            }

            if (tokens.CurrentToken.TokenType == CssTokenType.Identifier)
            {
                Children.AddCurrentAndAdvance(tokens, Context);
            }

            return Children.Count > 0;
        }
    }
}
