using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Document;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.TreeItems;
using Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives;

namespace Microsoft.WebTools.Languages.Css.Test.Document
{
    [TestClass]
    public class CssDocumentTest : CssUnitTestBase
    {
        [TestMethod]
        public void CssDocument_TextTest()
        {
            CssTree tree = new CssTree(new DefaultParserFactory());
            Assert.IsNull(tree.TextProvider);

            string text = "@import 'list.css' .a {color:red}";
            tree.TextProvider = new StringTextProvider(text);
            Assert.AreEqual(text, tree.TextProvider.GetText(0, tree.TextProvider.Length));
            Assert.IsNotNull(tree.StyleSheet);
            Assert.AreEqual(2, tree.StyleSheet.Children.Count);
            Assert.IsTrue(tree.StyleSheet.Children[0] is ImportDirective);

            text = ".a {color:red}";
            tree.TextProvider = new StringTextProvider(text);
            Assert.AreEqual(text, tree.TextProvider.GetText(0, tree.TextProvider.Length));
            Assert.IsNotNull(tree.StyleSheet);
            Assert.AreEqual(1, tree.StyleSheet.Children.Count);
            Assert.IsTrue(tree.StyleSheet.Children[0] is RuleSet);
        }
    }
}
