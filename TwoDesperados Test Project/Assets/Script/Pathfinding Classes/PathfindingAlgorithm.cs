using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfindingAlgorithm : MonoBehaviour
{
    Node current = null;

    public bool CheckIfPathExcist(List<Node> allNodes, Node obstacleNode, Node startNode, Node endNode)
    {
        Node start = startNode;
        var openList = new List<Node>();        
        var closedList = new List<Node>();
        int g = 0;

        openList.Add(start);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            var lowest = openList.Min(node => node.fCost);
            current = openList.First(node => node.fCost == lowest);

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(node => node.coorX == endNode.coorX && node.coorY == endNode.coorY) != null)
            {
                return true;
            }

            var neighbourNodes = GetComponent<GetWalkableAdjacentSquares>().GetWalkableNeighbours(current, allNodes);
            g++;

            foreach (var neighbourNode in neighbourNodes)
            {
                if (neighbourNode.coorX == obstacleNode.coorX &&
                    neighbourNode.coorY == obstacleNode.coorY)
                {
                    if (!closedList.Contains(neighbourNode))
                    {
                        closedList.Add(neighbourNode);
                    }
                    continue;
                }

                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                // if it's not in the open list, compute its score, set the parent and add it to the open list
                if (openList.FirstOrDefault(node => node.coorX == neighbourNode.coorX
                        && node.coorY == neighbourNode.coorY) == null)
                {
                    neighbourNode.gCost = g;
                    neighbourNode.hCost = CalculateHCost(neighbourNode.coorX, neighbourNode.coorY);
                    neighbourNode.fCost = neighbourNode.CalculateFCost(neighbourNode.gCost, neighbourNode.hCost);
                    neighbourNode.Parent = current;

                    openList.Insert(0, neighbourNode);
                }
                else
                {
                    // test if using the current G score makes the adjacent square's F score lower
                    // if yes update the parent because it means it's a better path
                    if (g + neighbourNode.hCost < neighbourNode.fCost)
                    {
                        neighbourNode.gCost = g;
                        neighbourNode.fCost = neighbourNode.CalculateFCost(neighbourNode.gCost, neighbourNode.hCost);
                        neighbourNode.Parent = current;
                    }
                }
            }
        }
        return false;
    }
    
    private int CalculateHCost(int currentX, int currentY)
    {
        int dx = Mathf.Abs(GameManagerData.GetEndPointX() - currentX);
        int dy = Mathf.Abs(GameManagerData.GetEndPointY() - currentY);

        int min = Mathf.Min(dx, dy);
        int max = Mathf.Max(dx, dy);

        int diagonalSteps = min;
        int straightSteps = max - min;

        return (int)(1.4f * diagonalSteps + straightSteps);
    }
}
