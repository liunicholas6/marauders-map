using System.Collections.Generic;
using GraphBuilder;

namespace Grammar
{
    public abstract class Symbol
    {
        protected Builder Builder;
        public abstract bool IsTerminal { get;  }
        protected Symbol(Builder builder)
        {
            Builder = builder;
        }
        public abstract List<Symbol> ApplyRule();
        
    }
}