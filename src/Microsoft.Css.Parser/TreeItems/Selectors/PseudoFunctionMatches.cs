using Microsoft.WebTools.Languages.Css.Parser;
using Microsoft.WebTools.Languages.Css.TreeItems.Functions;

namespace Microsoft.WebTools.Languages.Css.TreeItems.Selectors
{
    /// <summary>
    /// CSS :matches() pseudo class function
    /// </summary>
    internal sealed class PseudoFunctionMatches : Function
    {
        public PseudoFunctionMatches()
        {
        }

        protected override ParseItem CreateArgumentObject(ComplexItem parent, ItemFactory itemFactory, int argumentNumber)
        {
            return itemFactory.Create<PseudoSelectorArgument>(this);
        }
    }
}
