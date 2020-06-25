// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives
{
    /// <summary>
    /// Single line expected, in the form: @item1 item2 ... ;
    /// </summary>
    internal abstract class AtLineDirective : AtDirective
    {
        internal TokenItem Semicolon { get; private set; }

        protected AtLineDirective()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected bool CheckSemicolon(TokenStream tokens)
        {
            if (tokens.CurrentToken.TokenType == CssTokenType.Semicolon)
            {
                Semicolon = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.Default);
            }
            else
            {
                AddParseError(ParseErrorType.AtDirectiveSemicolonMissing, ParseErrorLocation.AfterItem);
            }

            return Semicolon != null;
        }
    }
}
