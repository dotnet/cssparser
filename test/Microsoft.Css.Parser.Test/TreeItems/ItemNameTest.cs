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
    public class ItemNameTest : CssUnitTestBase
    {
        [TestMethod]
        public void ItemName_ParseTest()
        {
            string text1 = "a|b";
            ITextProvider tp = new StringTextProvider(text1);
            TokenStream tokens = Helpers.MakeTokenStream(tp);
            ItemName n = new ItemName();
            Assert.IsTrue(n.Parse(new ItemFactory(tp, null), tp, tokens));
            Assert.IsNotNull(n.Name);
            Assert.IsNotNull(n.Namespace);
            Assert.IsNotNull(n.Separator);

            text1 = "|b";
            tp = new StringTextProvider(text1);
            tokens = Helpers.MakeTokenStream(tp);
            n = new ItemName();
            Assert.IsTrue(n.Parse(new ItemFactory(tp, null), tp, tokens));
            Assert.IsNotNull(n.Name);
            Assert.IsNull(n.Namespace);
            Assert.IsNotNull(n.Separator);

            text1 = "*|b";
            tp = new StringTextProvider(text1);
            tokens = Helpers.MakeTokenStream(tp);
            n = new ItemName();
            Assert.IsTrue(n.Parse(new ItemFactory(tp, null), tp, tokens));
            Assert.IsNotNull(n.Name);
            Assert.IsNotNull(n.Namespace);
            Assert.IsNotNull(n.Separator);
        }

        [TestMethod]
        public void ItemName_ParseValid()
        {
            string[] tests = new string[]
            {
                "|",
                "*",
                "**",
                "*|",
                "*|*",
                "foo",
                "foo|",
                "foo|bar",
                "foo|bar",
                "foo|*",
                "*foo",
            };

            foreach (string test in tests)
            {
                ITextProvider tp = new StringTextProvider(test);
                TokenStream tokens = Helpers.MakeTokenStream(tp);
                ItemName s = new ItemName();
                Assert.IsTrue(s.Parse(new ItemFactory(tp, null), tp, tokens));
            }
        }

        [TestMethod]
        public void ItemName_ParseInvalid()
        {
            string[] tests = new string[]
            {
                "#foo",
                "!foo",
                "||", // double pipe is a new combinator in CSS4, so don't parse it as two consecutive |
            };

            foreach (string test in tests)
            {
                ITextProvider tp = new StringTextProvider(test);
                TokenStream tokens = Helpers.MakeTokenStream(tp);
                ItemName s = new ItemName();
                Assert.IsFalse(s.Parse(new ItemFactory(tp, null), tp, tokens));
            }
        }
    }
}
