using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Pathfinding
{
    public class Node : MonoBehaviour
    {

        public Color color = Color.white;

        private SpriteRenderer spriteRenderer;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            spriteRenderer.color = color;
        }

        // A* weights
        public float cost;
        public float heutistic;
    }
}