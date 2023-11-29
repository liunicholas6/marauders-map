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
        private BinaryRoom _root;

        public PartitionRunner(Builder builder, Rectangle rectangle)
        {
            Builder = builder;
            _root = new BinaryRoom(this, rectangle);
        }

        public void AddDivider(Divider divider)
        {
            Dividers.Add(divider);
        }

        public void Run()
        {
            _root.RandomSplit();
            foreach (var _ in from divider in Dividers from _ in divider.GetEdges() select divider)
            {}
        }

        public IEnumerable<Rectangle> GetRects()
        {
            return _root.GetRects();
        }

    }
}