using System.Collections.Generic;

namespace Grammar
{
    public class GrammarRunner
    {
        private GraphBuilder.GraphBuilder _graphBuilder;
        private List<ISymbol> _terminals;
        
        public void RunGrammar(int steps, Queue<ISymbol> symbols)
        {
            for (int step = 0; step < steps; step++)
            {
                for (int i = symbols.Count; i > 0; i--)
                {
                    var curr = symbols.Dequeue();
                    foreach (var next in curr.ApplyRule(_graphBuilder))
                    {
                        if (next.IsTerminal)
                        {
                            _terminals.Add(next);
                        }
                        else
                        {
                            symbols.Enqueue(curr);
                        }
                    }
                }
            }
        }
    }
}