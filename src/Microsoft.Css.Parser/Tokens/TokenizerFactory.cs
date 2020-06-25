// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.WebTools.Languages.Css.Tokens
{
    public interface ICssTokenizerFactory
    {
        ICssTokenizer CreateTokenizer();
    }

    internal sealed class DefaultTokenizerFactory : ICssTokenizerFactory
    {
        public ICssTokenizer CreateTokenizer()
        {
            return new CssTokenizer();
        }
    }
}
