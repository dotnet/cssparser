// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.WebTools.Languages.Css.Classify;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.TreeItems.PropertyValues
{
    /// <summary>
    /// Hex color property value
    /// </summary>
    public class HexColorValue : ComplexItem
    {
        internal TokenItem HashName { get; private set; }

        internal override bool TreatAsWord => true;

        public HexColorValue()
        {
        }

        internal bool TryGetNumberRange(out int start, out int length)
        {
            if (HashName == null)
            {
                start = 0;
                length = 0;
                return false;
            }

            start = HashName.Start + 1;
            length = HashName.Length - 1;
            return true;
        }

        public override bool Parse(ItemFactory itemFactory, ITextProvider text, TokenStream tokens)
        {
            HashName = Children.AddCurrentAndAdvance(tokens, CssClassifierContextType.HexColor);

            return Children.Count > 0;
        }
    }
}
