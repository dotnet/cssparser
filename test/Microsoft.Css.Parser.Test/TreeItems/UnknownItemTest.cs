using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Text;
using Microsoft.WebTools.Languages.Css.TreeItems;
using Microsoft.WebTools.Languages.Css.TreeItems.Functions;

namespace Microsoft.WebTools.Languages.Css.Test.TreeItems
{
    [TestClass]
    public class UnknownItemTest : CssUnitTestBase
    {
        [TestMethod]
        public void ParseUnknownTest()
        {
            string text = "}";
            ITextProvider tp = new StringTextProvider(text);
            {
                ParseItem pi = UnknownItem.ParseUnknown(
                    null, new ItemFactory(tp, null), tp,
                    Helpers.MakeTokenStream(tp),
                    ParseErrorType.OpenCurlyBraceMissingForRule);

                Assert.IsNotNull(pi);
                Assert.IsTrue(pi.HasParseErrors);
                Assert.AreEqual(ParseErrorType.OpenCurlyBraceMissingForRule, pi.ParseErrors[0].ErrorType);
                Assert.AreEqual(ParseErrorLocation.WholeItem, pi.ParseErrors[0].Location);
            }

            // Try a URL
            {
                text = "url('foo.jpg')";
                tp = new StringTextProvider(text);

                UrlItem pi = UnknownItem.ParseUnknown(
                    null, new ItemFactory(tp, null), tp,
                    Helpers.MakeTokenStream(tp)) as UrlItem;

                Assert.IsNotNull(pi);
            }
        }
    }
}
