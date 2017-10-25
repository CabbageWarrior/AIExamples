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

            List<Node> bestPath = new List<Node>(); ;
            StartCoroutine(Search(nodes, edges, startNode, endNode, bestPath));
        }

        public IEnumerator Search(List<Node> nodes, List<Edge> edges, Node startNode, Node endNode, List<Node> bestPath)
        {
            List<Node> openSet = new List<Node>();
            List<Node> closedSet = new List<Node>();

            startNode.cost = 0;
            startNode.heutistic = Vector2.Distance(startNode.transform.position, endNode.transform.position) * heuristicMultiplier;
            
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node n = GetBestNode(openSet, true);
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

            // Find best path
            bestPath.Add(endNode);
            var currentNode = endNode;
            while (currentNode != startNode)
            {
                // Get the neighbours of the current node
                List<Node> neighs = graphMaker.GetNeighbours(currentNode);

                // Find the best neighbour
                Node bestNeigh = GetBestNode(neighs, false);

                Edge e = graphMaker.GetEdge(currentNode, bestNeigh);

                bestPath.Add(bestNeigh);
                currentNode = bestNeigh;

                bestNeigh.color = Color.cyan;
                e.color = Color.yellow;
                yield return new WaitForSeconds(drawDelay);
            }

            yield return null;
        }

        private Node GetBestNode(List<Node> set, bool useHeuristic)
        {
            Node bestNode = null;
            float bestTotal = float.MaxValue;

            foreach (Node n in set)
            {
                var totalCost = (useHeuristic ? n.cost + n.heutistic : n.cost);
                if (totalCost < bestTotal)
                {
                    bestTotal = totalCost;
                    bestNode = n;
                }
            }

            return bestNode;
        }
    }
}