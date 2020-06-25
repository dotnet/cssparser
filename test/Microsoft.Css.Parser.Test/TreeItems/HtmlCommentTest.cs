using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems;
using Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives;
using Microsoft.WebTools.Languages.Css.TreeItems.Comments;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems
{
    [TestClass]
    public class HtmlCommentTest : CssUnitTestBase
    {
        [TestMethod]
        public void HtmlComment_ParseTest()
        {
            string text1 = "<!-- foo#bar -->";
            ITextProvider tp = new StringTextProvider(text1);
            TokenStream tokens = Helpers.MakeTokenStream(tp);

            // Parse "<!--"
            {
                HtmlComment hc = new HtmlComment();
                Assert.IsTrue(hc.Parse(new ItemFactory(tp, null), tp, tokens));
                Assert.AreEqual(1, hc.Children.Count);
                Assert.AreEqual(CssTokenType.OpenHtmlComment, ((TokenItem)hc.Children[0]).TokenType);
            }

            // Parse "foo#bar"
            {
                Assert.AreEqual(CssTokenType.Identifier, tokens.Advance(1).TokenType);
                Assert.AreEqual(CssTokenType.HashName, tokens.Advance(1).TokenType);
            }

            // Parse "-->"
            {
                HtmlComment hc = new HtmlComment();
                Assert.IsTrue(hc.Parse(new ItemFactory(tp, null), tp, tokens));
                Assert.AreEqual(1, hc.Children.Count);
                Assert.AreEqual(CssTokenType.CloseHtmlComment, ((TokenItem)hc.Children[0]).TokenType);
            }

            string text2 = "<!-- @namespace foo \"www.foo.com\" -->";
            CssParser parser = new CssParser();
            StyleSheet sheet = parser.Parse(text2, true);

            Assert.IsTrue(sheet.ComplexItemFromRange(14, 0) is NamespaceDirective);
            Assert.IsTrue(sheet.ComplexItemFromRange(2, 0) is HtmlComment);
            Assert.IsTrue(sheet.ComplexItemFromRange(35, 0) is HtmlComment);
        }
    }
}
