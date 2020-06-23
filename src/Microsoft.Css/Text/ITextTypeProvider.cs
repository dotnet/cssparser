namespace Microsoft.WebTools.Languages.Css.Text
{
    /// <summary>
    /// This can optionally be implemented by classes that implement ITextProvider
    /// </summary>
    public interface ITextTypeProvider
    {
        TextTypes TextTypes { get; }
    }
}
