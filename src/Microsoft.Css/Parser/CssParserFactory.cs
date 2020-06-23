namespace Microsoft.WebTools.Languages.Css.Parser
{
    public interface ICssParserFactory
    {
        ICssParser CreateParser();
    }

    public class DefaultParserFactory : ICssParserFactory
    {
        public ICssParser CreateParser()
        {
            return new CssParser();
        }
    }
}
