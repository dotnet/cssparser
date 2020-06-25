// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems.Functions;

namespace Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives
{
    internal sealed class NamespaceDirective : AtLineDirective
    {
        // CSS3 : http://www.w3.org/TR/2008/CR-css3-namespace-20080523/
        //
        // @namespace svg "http://www.w3.org/2000/svg";
        // @namespace "http://www.w3.org/1999/xhtml";
        //
        // namespace: NAMESPACE_SYM S* [namespace_prefix S*]? [STRING|URI] S* ';' S*
        // namespace_prefix : IDENT

        internal TokenItem Namespace { get; private set; } // may be missing if default namespace
        internal TokenItem String { get; private set; }
        internal UrlItem Url { get; private set; }

        public NamespaceDirective()
        {
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            ParseAtAndKeyword(itemFactory, text, tokens);

            if (tokens.CurrentToken.TokenType == CssTokenType.Identifier)
            {
                Namespace = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.ItemNamespace);
            }

            if (tokens.CurrentToken.IsString())
            {
                String = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.String);
            }
            else if (tokens.CurrentToken.TokenType == CssTokenType.Url)
            {
                Url = new UrlItem();

                if (Url.Parse(itemFactory, text, tokens))
                {
                    Url.Context = CssClassifierContextCache.FromTypeEnum(CssClassifierContextType.UrlString);
                    Children.Add(Url);
                }
                else
                {
                    Url = null;
                }
            }

            if (String == null && Url == null)
            {
                Children.AddParseError(ParseErrorType.UrlNamespaceMissing);
            }

            CheckSemicolon(tokens);

            return Children.Count > 0;
        }
    }
}
