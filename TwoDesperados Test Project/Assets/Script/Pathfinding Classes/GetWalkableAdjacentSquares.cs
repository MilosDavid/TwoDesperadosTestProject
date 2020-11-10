using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetWalkableAdjacentSquares : MonoBehaviour
{
    public List<Node> GetWalkableNeighbours(Node currentNode, List<Node> nodesArray)
    {
        List<Node> neighbourList = new List<Node>();

        if (currentNode.coorX - 1 >= 0)
        {
            //Left
            AddNodeToList(currentNode.coorX - 1, currentNode.coorY, nodesArray, neighbourList);
            //left down

            if (currentNode.coorY - 1 >= 0)
            {
                AddNodeToList(currentNode.coorX - 1, currentNode.coorY - 1, nodesArray, neighbourList);
            }
            //left up

            if (currentNode.coorY + 1 < GameManagerData.GetBoardSize())
            {
                AddNodeToList(currentNode.coorX - 1, currentNode.coorY + 1, nodesArray, neighbourList);
            }
        }

        if (currentNode.coorX + 1 < GameManagerData.GetBoardSize())
        {
            //Right
            AddNodeToList(currentNode.coorX + 1, currentNode.coorY, nodesArray, neighbourList);
            //right down
            if (currentNode.coorY - 1 >= 0)
            {
                AddNodeToList(currentNode.coorX + 1, currentNode.coorY - 1, nodesArray, neighbourList);

            }
            //right up
            if (currentNode.coorY + 1 < GameManagerData.GetBoardSize())
            {
                AddNodeToList(currentNode.coorX + 1, currentNode.coorY + 1, nodesArray, neighbourList);

            }
        }

        //Down
        if (currentNode.coorY - 1 >= 0)
        {
            AddNodeToList(currentNode.coorX, currentNode.coorY - 1, nodesArray, neighbourList);
        }
        //UP
        if (currentNode.coorY + 1 < GameManagerData.GetBoardSize())
        {
            AddNodeToList(currentNode.coorX, currentNode.coorY + 1, nodesArray, neighbourList);
        }

        return neighbourList;
    }

    private void AddNodeToList(int x, int y, List<Node> from, List<Node> to)
    {
        var node = GetNode(x, y, from);
        if (node != null)
        {
            to.Add(node);
        }
    }

    private Node GetNode(int x, int y, List<Node> nodesArray)
    {
        return nodesArray.FirstOrDefault(node => node.coorX == x && node.coorY == y);
    }
}
