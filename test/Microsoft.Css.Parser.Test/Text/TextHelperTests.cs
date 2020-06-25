using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Text;

namespace Microsoft.WebTools.Languages.Css.Test.Text
{

    [TestClass]
    public class TextHelperTests
    {
        [TestMethod]
        public void Test_TextHelper_GetNewLineCount()
        {
            // 1 = \r
            // 2 = \r\n
            // 3 = \r
            // 4 = \r\n
            // 5 = \n
            // 6 = \r\n
            string text = "\r\r\n\r\r\n\n\r\n";

            int newLineCount = TextHelper.GetNewLineCount(text);

            Assert.AreEqual<int>(6, newLineCount);
        }
    }
}
