using UnityEngine;

public class Node : MonoBehaviour
{
    public int coorX;
    public int coorY;

    public int gCost;   // walking cost from the start node
    public int hCost;   // heuristic cost to reach the end node |endX - currentX| + |endY - currentY|
    public int fCost;   // F = g + h

    public bool isWalkable;

    public Node Parent;

    public Node(int x, int y)
    {
        this.coorX = x;
        this.coorY = y;
    }

    public int CalculateFCost(int gCost, int hCost)
    {
        return fCost = gCost + hCost;
    }
}
