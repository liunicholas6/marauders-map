using System.Collections.Generic;
using GraphBuilder;

namespace Grammar
{
    public class GrammarRunner
    {
        private Builder _builder;
        private List<Symbol> _terminals;
        
        public void RunGrammar(int steps, Queue<Symbol> symbols)
        {
            for (int step = 0; step < steps; step++)
            {
                for (int i = symbols.Count; i > 0; i--)
                {
                    var curr = symbols.Dequeue();
                    foreach (var next in curr.ApplyRule())
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