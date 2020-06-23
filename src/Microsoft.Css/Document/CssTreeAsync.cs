using System;
using Microsoft.WebTools.Languages.Css.TreeItems;

namespace Microsoft.WebTools.Languages.Css.Document
{
    /// <summary>
    /// This is expected to be passed to background threads so that they can read a StyleSheet
    /// </summary>
    internal interface IStyleSheetLockFactory
    {
        IStyleSheetLock CreateReadLock();
        int TreeChangeStamp { get; }
    }

    /// <summary>
    /// This allows access to a StyleSheet while it is locked for reading (until Disposed)
    /// The StyleSheet is null if the read lock couldn't be acquired.
    /// </summary>
    public interface IStyleSheetLock : IDisposable
    {
        StyleSheet StyleSheet { get; }
        bool ShouldCancelReading { get; }
    }
}
