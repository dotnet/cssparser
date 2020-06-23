using System;

namespace Microsoft.WebTools.Languages.Css.Text
{
    [Flags]
    public enum TextTypes
    {
        None = 0,
        Razor = 1, // the text can contain Razor content, like @@import { }
    }
}
