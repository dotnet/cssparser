using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.Tokens;

namespace Microsoft.WebTools.Languages.Css.Test.Tokens
{
    [TestClass]
    public class TokenizerFactoryTest : CssUnitTestBase
    {
        private class TestTokenizer : CssTokenizer
        {
            private readonly TestTokenizerFactory _factory;

            public TestTokenizer(TestTokenizerFactory factory)
            {
                _factory = factory;
            }

            protected override bool HandleToken()
            {
                _factory.CallCount++;
                return base.HandleToken();
            }
        }

        private class TestTokenizerFactory : ICssTokenizerFactory
        {
            public int CallCount { get; set; }

            public ICssTokenizer CreateTokenizer()
            {
                return new TestTokenizer(this);
            }
        }

        [TestMethod]
        public void TokenizerFactory_BasicTest()
        {
            TestTokenizerFactory tokenizerFactory = new TestTokenizerFactory();
            CssParser parser = new CssParser(tokenizerFactory, null);
            _ = parser.Parse(@".foo#bar { color: blue; color: invisible }", insertComments: true);

            Assert.AreEqual(12, tokenizerFactory.CallCount);
        }
    }
}
