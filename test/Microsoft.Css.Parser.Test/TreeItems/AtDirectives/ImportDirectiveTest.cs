using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems.AtDirectives;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems.AtDirectives
{
    [TestClass]
    public class ImportDirectiveTest : CssUnitTestBase
    {
        [TestMethod]
        public void ImportDirective_ParseTest()
        {
            ITextProvider tp = new StringTextProvider("@import 'foo';");
            TokenStream tokens = Helpers.MakeTokenStream(tp);
            CharsetDirective d = new CharsetDirective();
            Assert.IsTrue(d.Parse(new ItemFactory(tp, null), tp, tokens));
            Assert.IsNotNull(d.CharacterSet);
            Assert.IsTrue(tp.CompareTo(d.CharacterSet.Start, "'foo'", ignoreCase: false));
            Assert.IsNotNull(d.Keyword);
            Assert.IsNotNull(d.Semicolon);

            tp = new StringTextProvider("@import ;");
            tokens = Helpers.MakeTokenStream(tp);
            d = new CharsetDirective();
            Assert.IsTrue(d.Parse(new ItemFactory(tp, null), tp, tokens));
            Assert.IsNotNull(d.Keyword);
            Assert.IsNull(d.CharacterSet);
            Assert.IsNotNull(d.Semicolon);

            tp = new StringTextProvider("@import");
            tokens = Helpers.MakeTokenStream(tp);
            d = new CharsetDirective();
            Assert.IsTrue(d.Parse(new ItemFactory(tp, null), tp, tokens));
            Assert.IsNotNull(d.Keyword);
            Assert.IsNull(d.CharacterSet);
            Assert.IsNull(d.Semicolon);
        }
    }
}
