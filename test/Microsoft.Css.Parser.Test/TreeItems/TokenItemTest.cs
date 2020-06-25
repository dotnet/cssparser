// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems
{
    [TestClass]
    public class TokenItemTest : CssUnitTestBase
    {
        [TestMethod]
        public void TokenItem_ParseTest()
        {
            ITextProvider tp = new StringTextProvider("foo .a { } *");
            TokenList tokens = Helpers.MakeTokens(tp);
            TokenStream iter = new TokenStream(tokens);

            TokenItem ti = new TokenItem(null, null);
            Assert.IsTrue(ti.Parse(new ItemFactory(tp, null), tp, iter));
            Assert.AreEqual(CssTokenType.Identifier, ti.TokenType);
            Assert.AreEqual(0, ti.Token.Start);
            Assert.AreEqual(3, ti.Token.AfterEnd);
            Assert.AreEqual("foo", tp.GetText(ti.Start, ti.Length));

            ti = new TokenItem(null, null);
            Assert.IsTrue(ti.Parse(new ItemFactory(tp, null), tp, iter));
            Assert.AreEqual(CssTokenType.Dot, ti.TokenType);
            Assert.AreEqual(4, ti.Token.Start);
            Assert.AreEqual(1, ti.Token.Length);
        }
    }
}
