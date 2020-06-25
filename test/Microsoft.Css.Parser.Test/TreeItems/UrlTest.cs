using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.Tokens;
using Microsoft.WebTools.Languages.Css.TreeItems.Functions;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems
{
    [TestClass]
    public class UrlTest : CssUnitTestBase
    {
        [TestMethod]
        public void Url_ParseTest()
        {
            TokenStream tokens;
            string text = "url(www.microsoft.com)";
            ITextProvider tp = new StringTextProvider(text);
            tokens = Helpers.MakeTokenStream(tp);
            UrlItem u = new UrlItem();
            Assert.IsTrue(u.Parse(new ItemFactory(tp, null), tp, tokens));

            text = "url('www.microsoft.com')";
            tp = new StringTextProvider(text);
            tokens = Helpers.MakeTokenStream(tp);
            u = new UrlItem();
            Assert.IsTrue(u.Parse(new ItemFactory(tp, null), tp, tokens));

            text = "url()";
            tp = new StringTextProvider(text);
            tokens = Helpers.MakeTokenStream(tp);
            u = new UrlItem();
            Assert.IsTrue(u.Parse(new ItemFactory(tp, null), tp, tokens));
        }
    }
}
