using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.TreeItems;
using Microsoft.WebTools.Languages.Css.TreeItems.Comments;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems
{
    [TestClass]
    public class InlineStyleTest
    {
        [TestMethod]
        public void InlineStyle_ParseTest()
        {
            string text = "color:red; /* c1 */ background-color: /* c2 */ ugly";
            CssParser parser = new CssParser();

            InlineStyle inlineStyle = parser.ParseInlineStyle(text, insertComments: true);
            Assert.IsNotNull(inlineStyle);

            Assert.AreEqual(0, inlineStyle.Start);
            Assert.AreEqual(text.Length, inlineStyle.Length);

            Assert.IsNull(inlineStyle.OpenCurlyBrace);
            Assert.IsNull(inlineStyle.CloseCurlyBrace);

            Assert.AreEqual(2, inlineStyle.Declarations.Count);
            Assert.AreEqual(0, inlineStyle.Declarations[0].Start);
            Assert.AreEqual(10, inlineStyle.Declarations[0].Length);
            Assert.AreEqual(20, inlineStyle.Declarations[1].Start);
            Assert.AreEqual(31, inlineStyle.Declarations[1].Length);

            Assert.AreEqual(3, inlineStyle.Children.Count);
            Assert.IsInstanceOfType(inlineStyle.Children[0], typeof(Declaration));
            Assert.IsInstanceOfType(inlineStyle.Children[1], typeof(CComment));
            Assert.IsInstanceOfType(inlineStyle.Children[2], typeof(Declaration));
        }
    }
}
