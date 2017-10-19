using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Pathfinding
{
    public class AStarSearch : MonoBehaviour
    {
        public Node startNode;
        public Node endNode;
        public float heuristicMultiplier = 1f;

        public float drawDelay = .2f;

        private GraphMaker graphMaker;

        void Start()
        {
            graphMaker = FindObjectOfType<GraphMaker>();

            var nodes = graphMaker.nodes;
            var edges = graphMaker.edges;

            startNode.transform.localScale = Vector3.one * 2;
            endNode.transform.localScale = Vector3.one * 2;

            StartCoroutine(Search(nodes, edges, startNode, endNode));
        }

        private IEnumerator Search(List<Node> nodes, List<Edge> edges, Node startNode, Node endNode)
        {
            List<Node> openSet = new List<Node>();
            List<Node> closedSet = new List<Node>();

            startNode.cost = 0;
            startNode.heutistic = Vector2.Distance(startNode.transform.position, endNode.transform.position) * heuristicMultiplier;
            
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node n = GetBestNode(openSet);
                openSet.Remove(n);
                closedSet.Add(n);

                n.color = Color.red;
                yield return new WaitForSeconds(drawDelay);

                if (n == endNode)
                {
                    Debug.Log("Found!");
                    break;
                }

                List<Node> neighs = graphMaker.GetNeighbours(n);

                foreach (Node neigh in neighs)
                {
                    if (!closedSet.Contains(neigh) && !openSet.Contains(neigh))
                    {
                        Edge e = graphMaker.GetEdge(n, neigh);
                        e.color = Color.green;
                        yield return new WaitForSeconds(drawDelay);

                        neigh.cost = n.cost + Vector2.Distance(neigh.transform.position, n.transform.position);
                        neigh.heutistic = Vector2.Distance(neigh.transform.position, endNode.transform.position) * heuristicMultiplier;

                        openSet.Add(neigh);
                    }
                }
            }

            yield return null;
        }

        private Node GetBestNode(List<Node> set)
        {
            Node bestNode = null;
            float bestTotal = float.MaxValue;

            foreach (Node n in set)
            {
                if (n.cost + n.heutistic < bestTotal)
                {
                    bestTotal = n.cost + n.heutistic;
                    bestNode = n;
                }
            }

            return bestNode;
        }
    }
}