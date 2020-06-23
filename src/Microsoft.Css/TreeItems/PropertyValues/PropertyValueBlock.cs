namespace Microsoft.WebTools.Languages.Css.TreeItems.PropertyValues
{
    internal sealed class PropertyValueBlock : UnknownBlock
    {
        public PropertyValueBlock()
        {
        }

        protected override bool IsBlockValid
        {
            get { return true; }
        }
    }
}
