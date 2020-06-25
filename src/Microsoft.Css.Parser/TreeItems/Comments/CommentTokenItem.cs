using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems.Comments
{
    /// <summary>
    /// Holds onto a single comment token
    /// </summary>
    internal sealed class CommentTokenItem : Comment
    {
        public TokenItem Comment { get; private set; }

        public CommentTokenItem()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            CssTokenType tokenType = tokens.CurrentToken.TokenType;
            if (tokenType == CssTokenType.CommentText ||
                tokenType == CssTokenType.SingleTokenComment ||
                tokenType == CssTokenType.SingleLineComment)
            {
                CssClassifierContextType context = tokenType == CssTokenType.CommentText
                    ? CssClassifierContextType.Default
                    : CssClassifierContextType.Comment;

                Comment = Children.AddCurrentAndAdvance(tokens, context);
            }

            return Children.Count > 0;
        }
    }
}
