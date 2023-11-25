using System.Collections.Generic;
using UnityEditor.Sprites;
using UnityEngine;

namespace BinaryPartition
{
    public class BinaryRoom
    {
        private const float MinWidth = 8;
        private const float MaxWidth = 20;
        private const float MinHeight = 8;
        private const float MaxHeight = 20;
        private const float TrimSize = 1;

        private Rectangle _rectangle;

        private SplitAxis _splitAxis = SplitAxis.None;
        private BinaryRoom _leftChild;
        private BinaryRoom _rightChild;
        
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
            
            if (rand < 0.5f && _rectangle.GetWidth() >= 2 * MinWidth)
            {
                SplitVertical();
            }
            else if (_rectangle.GetHeight() >= 2 * MinHeight)
            {
                SplitHorizontal();
            }
            else if (_rectangle.GetWidth() > MaxWidth)
            {
                SplitVertical();
            }
            else if (_rectangle.GetHeight() > MaxHeight)
            {
                SplitHorizontal();
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
            if (_splitAxis == SplitAxis.None)
            {
                list.Add(new Rectangle
                {
                    MaxX = _rectangle.MaxX - TrimSize,
                    MinX = _rectangle.MinX + TrimSize,
                    MinY = _rectangle.MinY + TrimSize,
                    MaxY = _rectangle.MaxY - TrimSize
                });
                return;
            }
            _leftChild.AppendRects(list);
            _rightChild.AppendRects(list);
        }
        
        private void SplitHorizontal()
        {
            _splitAxis = SplitAxis.Horizontal;
            var splitY = Mathf.Lerp(_rectangle.MinY + MinHeight, _rectangle.MaxY - MinHeight, Random.value);

            var leftRect = _rectangle.Clone();
            leftRect.MaxY = splitY;
            _leftChild = new BinaryRoom(leftRect);
            
            _leftChild.RandomSplit();

            var rightRect = _rectangle.Clone();
            rightRect.MinY = splitY;
            _rightChild = new BinaryRoom(rightRect);
            
            _rightChild.RandomSplit();
        }

        private void SplitVertical()
        {
            _splitAxis = SplitAxis.Vertical;
            var splitX = Mathf.Lerp(_rectangle.MinX + MinWidth, _rectangle.MaxX - MinWidth, Random.value);
            
            var leftRect = _rectangle.Clone();
            leftRect.MaxX = splitX;
            _leftChild = new BinaryRoom(leftRect);
            _leftChild.RandomSplit();

            var rightRect = _rectangle.Clone();
            rightRect.MinX = splitX;
            _rightChild = new BinaryRoom(rightRect);
            _rightChild.RandomSplit();
        }
        
        

    }
}