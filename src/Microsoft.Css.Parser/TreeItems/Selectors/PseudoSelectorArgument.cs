// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems.Functions;

namespace Microsoft.WebTools.Languages.Css.TreeItems.Selectors
{
    /// <summary>
    /// An simple selector argument for a pseudo function.
    /// </summary>
    internal sealed class PseudoSelectorArgument : FunctionArgument
    {
        internal SimpleSelector Selector { get; private set; }

        public PseudoSelectorArgument()
        {
        }

        protected override void ParseArgument(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            SimpleSelector simpleSelector = itemFactory.CreateSpecific<SimpleSelector>(this);

            if (simpleSelector.ParseInFunction(itemFactory, text, tokens))
            {
                Selector = simpleSelector;
                ArgumentItems.Add(Selector);
                Children.Add(Selector);
            }

            if (tokens.CurrentToken.TokenType != CssTokenType.Comma &&
                tokens.CurrentToken.TokenType != CssTokenType.CloseFunctionBrace)
            {
                Children.AddParseError(ParseErrorType.FunctionArgumentCommaMissing);
            }
        }
    }
}
