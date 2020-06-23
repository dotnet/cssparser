using System.Diagnostics;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems.Selectors
{
    /// <summary>
    /// /foo/
    /// </summary>
    internal sealed class IdReferenceOperator : ComplexItem
    {
        internal TokenItem OpenSlash { get; private set; }
        internal TokenItem IdReference { get; private set; }
        internal TokenItem CloseSlash { get; private set; }

        public IdReferenceOperator()
        {
        }

        public override bool IsValid
        {
            get { return OpenSlash != null && IdReference != null && CloseSlash != null; }
        }

        internal static bool IsAtIdReferenceOperator(TokenStream tokens)
        {
            return tokens.Peek(0).TokenType == CssTokenType.Slash &&
                tokens.Peek(1).TokenType == CssTokenType.Identifier &&
                tokens.Peek(2).TokenType == CssTokenType.Slash &&
                tokens.Peek(0).AfterEnd == tokens.Peek(1).Start &&
                tokens.Peek(1).AfterEnd == tokens.Peek(2).Start;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            Debug.Assert(IsAtIdReferenceOperator(tokens));
            if (IsAtIdReferenceOperator(tokens))
            {
                OpenSlash = Children.AddCurrentAndAdvance(tokens);
                IdReference = Children.AddCurrentAndAdvance(tokens);
                CloseSlash = Children.AddCurrentAndAdvance(tokens);
            }

            return Children.Count > 0;
        }
    }
}
