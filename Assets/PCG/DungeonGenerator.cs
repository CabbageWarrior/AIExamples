using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AI.PCG
{
    public class DungeonGenerator : MonoBehaviour
    {
        public int mapSize = 11;
        public GameObject cellPrefab;
        public Transform cellContainer;

        Cell[,] grid;

        public enum Direction
        {
            N = 0,
            E = 1,
            S = 2,
            W = 3
        }

        void Start()
        {
            // Normalize to odd mapSize value
            if (mapSize > 0 && mapSize % 2 == 0) mapSize--;

            StartCoroutine(GenerateCO());
        }

        public List<Cell> GetNeighbours(Cell c, int step)
        {
            List<Cell> neighs = new List<Cell>();

            // N
            if (c.y < mapSize - step)
                neighs.Add(grid[c.x, c.y + step]);
            else
                neighs.Add(null);

            // E
            if (c.x < mapSize - step)
                neighs.Add(grid[c.x + step, c.y]);
            else
                neighs.Add(null);

            // S
            if (c.y > step - 1)
                neighs.Add(grid[c.x, c.y - step]);
            else
                neighs.Add(null);

            // W
            if (c.x > step - 1)
                neighs.Add(grid[c.x - step, c.y]);
            else
                neighs.Add(null);

            return neighs;
        }
        public Cell GetNeighbour(Cell c, Direction d)
        {
            return grid[c.x + -((int)d - 2) % 2, c.y + -((int)d - 1) % 2];
        }

        private IEnumerator GenerateCO()
        {
            // Create a full dungeon
            grid = new Cell[mapSize, mapSize];

            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    var cellGO = Instantiate(cellPrefab, cellContainer);
                    cellGO.transform.position = new Vector3(x, y, 0);

                    var cell = cellGO.GetComponent<Cell>();
                    cell.x = x;
                    cell.y = y;

                    grid[x, y] = cell;
                }
            }

            // Choose initial start cell
            int startX = UnityEngine.Random.Range(0, mapSize / 2) * 2;
            int startY = UnityEngine.Random.Range(0, mapSize / 2) * 2;
            grid[startX, startY].spriteRenderer.color = Color.blue;
            grid[startX, startY].visited = true;

            // Visit recursively adjacent cells
            Cell currentCell = grid[startX, startY];
            Stack<Cell> backtrackingCells = new Stack<Cell>();
            backtrackingCells.Push(currentCell);
            while (true)
            {
                // Get "room" cells
                List<Cell> neighs = GetNeighbours(currentCell, 2);

                // Keep unvisited cells
                List<Cell> unvisitedNeighs = neighs.Where(c => c != null && !c.visited).ToList();

                if (unvisitedNeighs.Count == 0)
                {
                    // Backtracking
                    currentCell = backtrackingCells.Pop();
                }
                else
                {
                    // Choose a random visited neigh
                    int rndIndex;
                    Cell rndNeigh;
                    do
                    {
                        rndIndex = UnityEngine.Random.Range(0, neighs.Count);
                        rndNeigh = neighs[rndIndex];
                    } while (!unvisitedNeighs.Contains(rndNeigh));

                    Direction rndDir = (Direction)rndIndex;

                    // Dig it
                    rndNeigh.spriteRenderer.color = Color.white;
                    rndNeigh.visited = true;

                    Cell wallNeigh = GetNeighbour(currentCell, rndDir);
                    wallNeigh.spriteRenderer.color = Color.white;
                    wallNeigh.visited = true;

                    // Move to the next "room" cell
                    currentCell = rndNeigh;
                    backtrackingCells.Push(currentCell);
                }

                if (backtrackingCells.Count == 0)
                {
                    Debug.Log("Fine!");
                    break;
                }

                yield return null;
            }

            yield return null;
        }
    }
}