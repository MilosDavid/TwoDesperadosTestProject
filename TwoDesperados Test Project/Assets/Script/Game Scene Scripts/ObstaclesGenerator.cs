using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    Node startNode;
    Node endNode;

    public List<Coordinate> StartObstaclesGenerator(Node[] validChoices, Node startNode, Node endNode)
    {
        this.startNode = startNode;
        this.endNode = endNode;

        return GenerateObstaclesCoordinates(validChoices);
    }

    List<Coordinate> GenerateObstaclesCoordinates(Node[] validChoices)
    {
        var allNodes = validChoices.ToList();
        allNodes.Insert(0, startNode);
        allNodes.Insert(allNodes.Count, endNode);

        var obstaclesCoordinatesList = new List<Coordinate>();

        int numberOfObstacles = GameManagerData.GetNumberOfObstacles();

        for (int index = 0; index < numberOfObstacles; index++)
        {
            var obstacleCoordiateIsValid = false;
            while (!obstacleCoordiateIsValid && validChoices.Any())
            {
                var node = GetRandomNode(validChoices);

                obstacleCoordiateIsValid = IsPathExcist(allNodes, node, startNode, endNode);

                if (obstacleCoordiateIsValid)
                {
                    obstaclesCoordinatesList.Add(GetCoordinates(node));
                    node.isWalkable = false;
                }

                validChoices = validChoices.Except(new Node[] { node }).ToArray();
            }
        }

        return obstaclesCoordinatesList;
    }

    //get random value from available array of nodes
    private Node GetRandomNode(Node[] validChoices)
    {
        return validChoices[UnityEngine.Random.Range(0, validChoices.Length)];
    }

    //get coordinates from index
    private Coordinate GetCoordinates(Node currentNode)
    {
        //int x = (int)(index / GameManagerData.GetBoardSize());
        //int y = index - GameManagerData.GetBoardSize() * x;

        return new Coordinate(currentNode.coorX, currentNode.coorY);
    }

    //get index from coordinates
    int GetIndex(int x, int y)
    {
        return (x * GameManagerData.GetBoardSize()) + y + 1;
    }


    /// <summary>
    /// Check if Path still exist
    /// </summary>
    /// <param name="validChoices"></param>
    /// <param name="obstacleIndex"></param>
    /// <param name="startNode"></param>
    /// <param name="endNode"></param>
    /// <returns></returns>
    private bool IsPathExcist(List<Node> allChoices, Node obstacleNode, Node startNode, Node endNode)
    {
        return GetComponent<PathfindingAlgorithm>().CheckIfPathExcist(allChoices, obstacleNode, startNode, endNode);
    }

}