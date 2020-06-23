using System.Diagnostics;
using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Shared.Text;

namespace Microsoft.WebTools.Languages.Css.TreeItems.Comments
{
    /// <summary>
    /// C-type of comment (/* ... */)
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1722:IdentifiersShouldNotHaveIncorrectPrefix")]
    internal sealed class CComment : Comment
    {
        public ParseItem OpenComment { get; private set; }
        public ParseItem CommentText { get; private set; }
        public ParseItem CloseComment { get; private set; }

        public CComment()
        {
        }

        internal override bool IsUnclosed
        {
            get { return OpenComment != null && CloseComment == null; }
        }

        // Opening C-style comment /* which must be followed by an optional
        // CommentContent token and then closing comment or EOF.
        // Basically four combinations
        // /* text */,  /**/, /* text [EOF] and /* [EOF]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            Debug.Assert(tokens.CurrentToken.TokenType == CssTokenType.OpenCComment);
            OpenComment = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.Comment);

            if (tokens.CurrentToken.TokenType == CssTokenType.CommentText)
            {
                CommentText = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.Comment);
            }

            if (tokens.CurrentToken.TokenType == CssTokenType.CloseCComment)
            {
                CloseComment = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.Comment);
            }
            else
            {
                AddParseError(ParseErrorType.CloseCommentMissing, ParseErrorLocation.AfterItem);
            }

            return Children.Count > 0;
        }
    }
}
