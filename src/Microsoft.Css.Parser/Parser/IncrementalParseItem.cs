// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.Parser
{
    /// <summary>
    /// Items that support incremental parsing will implement this
    /// </summary>
    internal interface IIncrementalParseItem
    {
        ParseItem CreateNextChild(ParseItem previousChild, ItemFactory itemFactory, ITextProvider text, TokenStream tokens);

        void UpdateCachedChildren();
        bool UpdateParseErrors();
    }
}
