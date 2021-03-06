// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Diagnostics;
using Microsoft.Css.Parser.Classify;
using Microsoft.Css.Parser.Parser;
using Microsoft.Css.Parser.Text;
using Microsoft.Css.Parser.Tokens;
using Microsoft.Css.Parser.TreeItems.Functions;

namespace Microsoft.Css.Parser.TreeItems.Selectors
{
    public class PseudoElementFunctionSelector : ComplexItem // ::slot(arg)
    {
        // http://www.w3.org/TR/2009/WD-css3-selectors-20090310/#pseudo-elements:
        // :: notation is introduced by the current document in order to establish a discrimination
        // between pseudo-classes and pseudo-elements. For compatibility with existing style sheets,
        // user agents must also accept the previous one-colon notation for pseudo-elements
        // introduced in CSS levels 1 and 2 (namely, :first-line, :first-letter, :before and :after).
        // This compatibility is not allowed for the new pseudo-elements introduced in this specification.

        internal TokenItem DoubleColon { get; private set; }
        public Function Function { get; protected set; }

        public PseudoElementFunctionSelector()
        {
            Context = CssClassifierContextCache.FromTypeEnum(CssClassifierContextType.PseudoElement);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            Debug.Assert(tokens.CurrentToken.TokenType == CssTokenType.DoubleColon);
            if (tokens.CurrentToken.TokenType == CssTokenType.DoubleColon)
            {
                DoubleColon = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.PseudoElement);
                ParseName(itemFactory, text, tokens);
            }

            return Children.Count > 0;
        }

        protected virtual void ParseName(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            if (tokens.CurrentToken.TokenType == CssTokenType.Function &&
                tokens.CurrentToken.Start == DoubleColon.AfterEnd)
            {
                Function = itemFactory.CreateSpecific<Function>(this);
                Function.Parse(itemFactory, text, tokens);
                Function.Context = CssClassifierContextCache.FromTypeEnum(CssClassifierContextType.PseudoElement);
                Children.Add(Function);
            }
            else
            {
                Children.AddParseError(ParseErrorType.PseudoElementNameMissing);
            }
        }
    }
}
