// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace Microsoft.WebTools.Languages.Css.Parser
{
    public interface ICssParserFactory
    {
        ICssParser CreateParser();
    }

    public class DefaultParserFactory : ICssParserFactory
    {
        public ICssParser CreateParser()
        {
            return new CssParser();
        }
    }
}
