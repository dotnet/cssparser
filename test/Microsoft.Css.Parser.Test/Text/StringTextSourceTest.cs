using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Text;

namespace Microsoft.WebTools.Languages.Css.Test.Text
{
    [TestClass]
    public class StringTextProviderTest : CssUnitTestBase
    {
        [TestMethod]
        public void StringTextProvider_Simple()
        {
            string text = "abcd";
            ITextProvider ts = new StringTextProvider(text);

            Assert.AreEqual(text[0], ts[0]);
            Assert.AreEqual(text[3], ts[3]);
            Assert.AreEqual(4, ts.Length);
            Assert.AreEqual("bc", ts.GetText(1, 2));
            Assert.IsTrue(ts.CompareTo(1, "bcd", ignoreCase: false));
            Assert.IsFalse(ts.CompareTo(1, "abcd", ignoreCase: true));

            Assert.IsTrue(ts.CompareTo(1, new StringTextProvider("BCd"), 0, 3, ignoreCase: true));
            Assert.IsFalse(ts.CompareTo(1, new StringTextProvider("aBCDe"), 1, 3, ignoreCase: false));
            Assert.IsTrue(ts.CompareTo(1, new StringTextProvider("aBCDe"), 1, 3, ignoreCase: true));
        }
    }
}
