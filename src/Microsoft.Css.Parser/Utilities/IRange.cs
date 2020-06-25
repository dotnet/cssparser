namespace Microsoft.WebTools.Languages.Css.Utilities
{
    /// <summary>
    /// This interface must be implented for items to be in a SortedRangeList container
    /// </summary>
    public interface IRange
    {
        int Start { get; }
        int Length { get; }
        int AfterEnd { get; } // this must always return Start + Length
    }
}
