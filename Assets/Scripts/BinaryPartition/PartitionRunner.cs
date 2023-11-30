using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GraphBuilder;

namespace BinaryPartition
{
    public class PartitionRunner
    {
        public readonly List<Divider> Dividers = new();
        public readonly Builder Builder;
        private BinaryBlock _root;

        public PartitionRunner(Builder builder, Rectangle rectangle)
        {
            Builder = builder;
            _root = new BinaryBlock(this, rectangle);
        }

        public void AddDivider(Divider divider)
        {
            Dividers.Add(divider);
        }

        public void Run()
        {
            _root.RandomSplit();
            foreach (var divider in Dividers)
            {
                divider.MakeEdges();
            }
        }

    }
}