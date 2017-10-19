using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.Pathfinding
{
    public class DepthFirstSearch : MonoBehaviour
    {
        public Node startNode;
        public Node endNode;

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
            Stack<Node> stack = new Stack<Node>();
            stack.Push(startNode);

            List<Node> visitedNodes = new List<Node>();

            while (stack.Count > 0)
            {
                Node n = stack.Pop();
                visitedNodes.Add(n);

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
                    if (!stack.Contains(neigh) && !visitedNodes.Contains(neigh))
                    {
                        stack.Push(neigh);

                        Edge e = graphMaker.GetEdge(n, neigh);
                        e.color = Color.green;
                        yield return new WaitForSeconds(drawDelay);
                    }
                }
            }

            yield return null;
        }
    }
}