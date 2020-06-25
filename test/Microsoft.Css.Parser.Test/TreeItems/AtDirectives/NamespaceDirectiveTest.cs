// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License. See License.txt in the project root for license information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems.AtDirectives
{
    [TestClass]
    public class NamespaceDirectiveTest : CssUnitTestBase
    {
        [TestMethod]
        public void NamespaceDirective_ParseTest()
        {
            TokenStream ts;
            string text = "@namespace foo \"www.foo.com\";";
            ITextProvider tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            NamespaceDirective pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace foo;";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace foo";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace;";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace { }";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace 'www.microsoft.com'";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace foo bar;";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));

            text = "@namespace foo url('www.microsoft.com');";
            tp = new StringTextProvider(text);
            ts = Helpers.MakeTokenStream(tp);
            pd = new NamespaceDirective();
            Assert.IsTrue(pd.Parse(new ItemFactory(tp, null), tp, ts));
        }
    }
}
