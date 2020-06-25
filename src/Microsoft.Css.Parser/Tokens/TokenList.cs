using System.Diagnostics.CodeAnalysis;
using Microsoft.WebTools.Languages.Css.Utilities;

namespace Microsoft.WebTools.Languages.Css.Tokens
{
    /// <summary>
    /// This is used to store a sorted list of tokens.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Public API")]
    public class TokenList : SortedRangeList<CssToken>
    {
        internal TokenList()
        {
        }

        internal TokenList(int capacity)
            : base(capacity)
        {
        }
    }
}
