using System.Collections.Generic;
using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives;
using Microsoft.WebTools.Languages.Css.Utilities;
using Microsoft.WebTools.Languages.Shared.Text;

namespace Microsoft.WebTools.Languages.Css.TreeItems
{
    /// <summary>
    /// CSS rule block
    /// </summary>
    public class RuleBlock : BlockItem, IIncrementalParseItem
    {
        internal static SortedRangeList<AtDirective> _emptyDirectives = new SortedRangeList<AtDirective>();
        internal static SortedRangeList<Declaration> _emptyDeclarations = new SortedRangeList<Declaration>();

        /// <summary>
        /// List of declarations in the block
        /// </summary>
        public IReadOnlyList<Declaration> Declarations
        {
            get
            {
                return _declarations;
            }
        }

        private SortedRangeList<Declaration> _declarations;

        /// <summary>
        /// List of directives in the block
        /// </summary>
        internal IReadOnlyList<AtDirective> Directives
        {
            get
            {
                return _directives;
            }
        }

        private SortedRangeList<AtDirective> _directives;

        public RuleBlock()
        {
            _declarations = _emptyDeclarations;
            _directives = _emptyDirectives;
        }

        /// <summary>
        /// Rule blocks are always valid, but individual declarations inside of it may be invalid
        /// </summary>
        public override bool IsValid
        {
            get { return true; }
        }

        /// <summary>
        /// Returns false if this block contains property declarations that aren't standard CSS.
        /// </summary>
        internal virtual bool HasStandardPropertyNames
        {
            get { return true; }
        }

        /// <summary>
        /// List of custom properties in the block.
        /// </summary>
        internal IEnumerable<Declaration> CustomProperties
        {
            get
            {
                int declarationCount = Declarations.Count;
                for (int i = 0; i < declarationCount; i++)
                {
                    Declaration decl = Declarations[i];
                    if (decl.IsCustomProperty)
                    {
                        yield return decl;
                    }
                }
            }
        }

        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            return IncrementalParseHelper.FullParseIncrementalItem(this, itemFactory, text, tokens);
        }

        public virtual ParseItem CreateNextChild(ParseItem previousChild, ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            if (!IsInlineStyle)
            {
                // Look for curly braces

                if (previousChild is TokenItem && ((TokenItem)previousChild).TokenType == CssTokenType.CloseCurlyBrace)
                {
                    // No more children after the close curly brace
                    return null;
                }

                if (previousChild == null && tokens.CurrentToken.TokenType != CssTokenType.OpenCurlyBrace)
                {
                    // First child must be a curly brace
                    return null;
                }

                if (tokens.CurrentToken.TokenType == CssTokenType.OpenCurlyBrace)
                {
                    return (previousChild == null) ? new TokenItem(tokens.AdvanceToken(), CssClassifierContextType.CurlyBrace) : null;
                }

                if (tokens.CurrentToken.TokenType == CssTokenType.CloseCurlyBrace)
                {
                    return new TokenItem(tokens.AdvanceToken(), CssClassifierContextType.CurlyBrace);
                }
            }

            if (tokens.CurrentToken.IsScopeBlocker())
            {
                return null;
            }

            ParseItem newChild;

            switch (tokens.CurrentToken.TokenType)
            {
                case CssTokenType.At:
                    newChild = CreateDirective(itemFactory, text, tokens);
                    break;

                default:
                    newChild = CreateDefaultChild(itemFactory, text, tokens);
                    break;
            }

            return newChild;
        }

        protected virtual ParseItem CreateDefaultChild(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            ParseItem newChild = itemFactory.Create<Declaration>(this);

            if (!newChild.Parse(itemFactory, text, tokens))
            {
                newChild = UnknownItem.ParseUnknown(this, itemFactory, text, tokens, ParseErrorType.DeclarationExpected);
            }

            return newChild;
        }

        protected virtual ParseItem CreateDirective(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            ParseItem item = AtDirective.ParseDirective(this, itemFactory, text, tokens);

            if (item is AtDirective directive &&
                directive.Keyword != null &&
                !directive.Keyword.HasParseErrors &&
                !IsExpectedDirective(directive))
            {
                // @directives aren't expected in rules

                directive.Keyword.AddParseError(ParseErrorType.UnexpectedAtDirective, ParseErrorLocation.WholeItem);
            }

            return item;
        }

        protected virtual bool IsExpectedDirective(AtDirective directive)
        {
            // no directives are expected to be inside of rule blocks
            return false;
        }

        protected virtual bool IsInlineStyle
        {
            get { return false; }
        }

        /// <summary>
        /// Always called after parsing is done (full or incremental)
        /// </summary>
        public override void UpdateCachedChildren()
        {
            _declarations.Clear();
            _directives.Clear();

            int childCount = Children.Count;
            for (int i = 0; i < childCount; i++)
            {
                ParseItem child = Children[i];
                if (child is Declaration)
                {
                    if (_declarations == _emptyDeclarations)
                    {
                        _declarations = new SortedRangeList<Declaration>();
                    }

                    _declarations.Add((Declaration)child);
                }
                else if (child is AtDirective)
                {
                    if (_directives == _emptyDirectives)
                    {
                        _directives = new SortedRangeList<AtDirective>();
                    }

                    _directives.Add((AtDirective)child);
                }
            }

            base.UpdateCachedChildren();

            // tell the stylesheet to update in case we have new CustomProperties
            StyleSheet?.UpdateCachedChildren();
        }
    }
}
