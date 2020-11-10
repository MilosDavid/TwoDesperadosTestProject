using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AlgorithmType;

public class FindPathAlgorithm : MonoBehaviour
{
    Node current = null;

    public List<Node> FindPath(List<Node> allNodes, Node startNode, Node endNode, CurrentAlgorithmType playerAlgorithmType)
    {
        List<Node> correctPath = new List<Node>();

        var openList = new List<Node>();
        var closedList = new List<Node>();
        int g = 0;

        openList.Add(startNode);

        while (openList.Count > 0)
        {
            // get the square with the lowest F score
            if (playerAlgorithmType == CurrentAlgorithmType.A_star)
            {
                var lowest = openList.Min(node => node.fCost);
                current = openList.First(node => node.fCost == lowest);
            }
            else if (playerAlgorithmType == CurrentAlgorithmType.Greedy_suboptimal)
            {
                var highest = openList.Max(node => node.fCost);
                current = openList.First(node => node.fCost == highest);
            }

            // add the current square to the closed list
            closedList.Add(current);

            // remove it from the open list
            openList.Remove(current);

            // if we added the destination to the closed list, we've found a path
            if (closedList.FirstOrDefault(node => (node.coorX == endNode.coorX && node.coorY == endNode.coorY) && node.isWalkable) != null)
            {
                foreach (var node in closedList)
                {
                    if (node.isWalkable && !correctPath.Contains(node))
                    {
                        correctPath.Add(node);
                    }
                }

                return correctPath;
            }

            var neighbourNodes = GetComponent<GetWalkableAdjacentSquares>().GetWalkableNeighbours(current, allNodes);
            g++;

            foreach (var neighbourNode in neighbourNodes)
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                if (!neighbourNode.isWalkable)
                {
                    closedList.Add(neighbourNode);
                    continue;
                }

                // if it's not in the open list compute its score, set the parent and add it to the open list
                if (openList.FirstOrDefault(node => 
                node.coorX == neighbourNode.coorX && node.coorY == neighbourNode.coorY) == null)
                {
                    neighbourNode.gCost = g;
                    neighbourNode.hCost = CalculateHCost(neighbourNode.coorX, neighbourNode.coorY, playerAlgorithmType);
                    neighbourNode.fCost = neighbourNode.CalculateFCost(neighbourNode.gCost, neighbourNode.hCost);
                    neighbourNode.Parent = current;

                    openList.Insert(0, neighbourNode);
                }
                else
                {

                    //Debug.Log("<color=yellow>" + playerAlgorithmType.ToString() + "</color>");

                    if (playerAlgorithmType == CurrentAlgorithmType.A_star)
                    {
                        // test if using the current G score makes the adjacent square's F score
                        // lower, if yes update the parent because it means it's a better path
                        if (g + neighbourNode.hCost < neighbourNode.fCost)
                        {
                            neighbourNode.gCost = g;
                            neighbourNode.fCost = neighbourNode.CalculateFCost(neighbourNode.gCost, neighbourNode.hCost);
                            neighbourNode.Parent = current;
                        }
                    }
                    else if (playerAlgorithmType == CurrentAlgorithmType.Greedy_suboptimal)
                    {
                        if (g + neighbourNode.hCost > neighbourNode.fCost)
                        {
                            neighbourNode.gCost = g;
                            neighbourNode.fCost = neighbourNode.CalculateFCost(neighbourNode.gCost, neighbourNode.hCost);
                            neighbourNode.Parent = current;
                        }
                    }
                }
            }
        }

        return correctPath;
    }

    private int CalculateHCost(int currentX, int currentY, CurrentAlgorithmType playerAlgorithmType)
    {
        int dx = Mathf.Abs(GameManagerData.GetEndPointX() - currentX);
        int dy = Mathf.Abs(GameManagerData.GetEndPointY() - currentY);

        if (playerAlgorithmType == CurrentAlgorithmType.A_star)
        {
            int min = Mathf.Min(dx, dy);
            int max = Mathf.Max(dx, dy);

            int diagonalSteps = min;
            int straightSteps = max - min;

            return (int)(1.4f * diagonalSteps + straightSteps);
        }
        else
        {
            return dx + dy;
        }
    }
}
