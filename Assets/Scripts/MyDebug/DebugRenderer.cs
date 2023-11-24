using System;
using System.Collections.Generic;
using BinaryPartition;
using UnityEngine;

namespace MyDebug
{
    public class DebugRenderer : MonoBehaviour
    {
        private List<IDebugDrawable> _drawables = new();
        
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Running Debug Renderer");
            BinaryRoom room = new BinaryRoom(new Rectangle
            {
                MinX = -100, MinY = -100, MaxX = 100, MaxY = 100
            });
            room.RandomSplit();
            foreach (var rect in room.GetRects())
            {
                _drawables.Add(new DebugRect(rect));
            }
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var drawable in _drawables)
            {
                drawable.Draw(Color.blue);
            }
        }
    }
}
