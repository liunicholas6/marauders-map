using System;
using System.Collections.Generic;
using BinaryPartition;
using Geom;
using GraphBuilder;
using UnityEngine.VFX;
using UnityEngine;
using MyDebug;


namespace Navigation {
    public class WandererManager : MonoBehaviour {

        private List<IDebugDrawable> _drawables = new();
        private List<Wanderer> wanderers = new List<Wanderer>();
        private Graph navGraph;
        private PathFinder pathFinder;
        private bool isInitialized = false;

        public VisualEffect vfx;

        public void Initialize(Graph graph, GameObject vfx) {
                navGraph = graph;
                isInitialized = true;
                this.vfx = vfx.GetComponent<VisualEffect>();
            }

        void Start() {

            if (!isInitialized) {
                return;
            }
            this.navGraph = navGraph;
            pathFinder = new PathFinder(navGraph);
            for (int i = 0; i < 1; i++) {
                var start = navGraph.GetRandomEdge();
                var end = navGraph.GetRandomEdge();
                string gameObjectName = "Wanderer_" + i.ToString();
                var wander = new GameObject(gameObjectName);
                var wander_component = wander.AddComponent<Wanderer>();
                VisualEffectAsset footstepVFX = Resources.Load<VisualEffectAsset>("Footsteps");
                var vfxComponent = wander.AddComponent<VisualEffect>();
                vfxComponent.visualEffectAsset = footstepVFX; // Assign the VFX asset
                wander_component.Initialize(navGraph, start, end, pathFinder, vfx);
                wanderers.Add(wander_component);
                _drawables.Add(new DebugSquare() {position = wander_component.Position});
            }
        }

        void Update() {
            if (!isInitialized) {
                return;
            }
            _drawables = new();
            foreach (var wanderer in wanderers) {
                _drawables.Add(new DebugSquare() {position = wanderer.Position});
            }
            foreach (var drawable in _drawables) {
                drawable.Draw();
            }
            }
        }

        
    }


