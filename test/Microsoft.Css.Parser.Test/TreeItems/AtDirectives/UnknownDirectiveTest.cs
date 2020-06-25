// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems.AtDirectives
{
    [TestClass]
    public class UnknownDirectiveTest : CssUnitTestBase
    {
        [TestMethod]
        public void UnknownDirective_ParseTest()
        {
            string text = "@foo \"bar\";";
            ITextProvider tp = new StringTextProvider(text);
            UnknownDirective ud = new UnknownDirective();
            Assert.IsTrue(ud.Parse(new ItemFactory(tp, null), tp, Helpers.MakeTokenStream(tp)));
            Assert.AreEqual("@", text.Substring(ud.At.Start, ud.At.Length));
            Assert.AreEqual("foo", text.Substring(ud.Keyword.Start, ud.Keyword.Length));
        }
    }
}
