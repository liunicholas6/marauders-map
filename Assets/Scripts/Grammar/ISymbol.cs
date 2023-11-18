using System.Collections.Generic;

namespace Grammar
{
    public interface ISymbol
    {
        bool IsTerminal { get; }
        List<ISymbol> ApplyRule(GraphBuilder.GraphBuilder builder);
    }
}