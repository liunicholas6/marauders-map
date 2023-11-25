using System.Collections.Generic;
using UnityEngine;

namespace BinaryPartition
{
    public class BinaryRoom
    {
        private static readonly Vector2 MinDims = new Vector2(8, 8);
        private static readonly Vector2 MaxDims = new Vector2(20, 20);
        private const float TrimSize = 1;

        private Rectangle _rectangle;

        private BinaryRoom _leftChild;
        private BinaryRoom _rightChild;
        private bool IsLeaf
        {
            get => _leftChild == null && _rightChild == null; 
        }
        
        public BinaryRoom(Rectangle rectangle)
        {
            _rectangle = rectangle;
        }

        private BinaryRoom()
        {
            
        }

        public void RandomSplit()
        {
            var rand = Random.value;
            
            if (rand < 0.5f && _rectangle.GetDim(0) >= 2 * MinDims[0])
            {
                SplitPerpToAxis(0);
            }
            else if (_rectangle.GetDim(1) >= 2 * MinDims[1])
            {
                SplitPerpToAxis(1);
            }
            else if (_rectangle.GetDim(0) > MaxDims[0])
            {
                SplitPerpToAxis(0);
            }
            else if (_rectangle.GetDim(1) > MaxDims[1])
            {
                SplitPerpToAxis(1);
            }
        }

        public List<Rectangle> GetRects()
        {
            List<Rectangle> res = new ();
            AppendRects(res);
            return res;
        }

        public void AppendRects(ICollection<Rectangle> list)
        {
            if (IsLeaf)
            {
                list.Add(new Rectangle
                {
                    Max = _rectangle.Max - new Vector2(TrimSize, TrimSize),
                    Min = _rectangle.Min + new Vector2(TrimSize, TrimSize)
                });
                return;
            }
            _leftChild.AppendRects(list);
            _rightChild.AppendRects(list);
        }

        private void SplitPerpToAxis(int perpAxis)
        {
            var v = Mathf.Lerp(
                _rectangle.Min[perpAxis] + MinDims[perpAxis], 
                _rectangle.Max[perpAxis] - MinDims[perpAxis], 
                Random.value);

            var leftRect = _rectangle;
            leftRect.Max[perpAxis] = v;
            _leftChild = new BinaryRoom(leftRect);
            _leftChild.RandomSplit();

            var rightRect = _rectangle;
            rightRect.Min[perpAxis] = v;
            _rightChild = new BinaryRoom(rightRect);
            _rightChild.RandomSplit();
        }
    }
}