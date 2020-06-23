namespace Microsoft.WebTools.Languages.Css.Tokens
{
    public interface ICssTokenizerFactory
    {
        ICssTokenizer CreateTokenizer();
    }

    internal sealed class DefaultTokenizerFactory : ICssTokenizerFactory
    {
        public ICssTokenizer CreateTokenizer()
        {
            return new CssTokenizer();
        }
    }
}
