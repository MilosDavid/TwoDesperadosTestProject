using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridGenerator : MonoBehaviour
{
    public Node[] nodesArray;

    public List<Node> nodeList = new List<Node>();

    List<Node> tempList = new List<Node>();

    public Sprite StartNodeSprite;
    public Sprite EndNodeSprite;
    public Sprite PathSprite;
    public Sprite Player1PathSprite;
    public Sprite Player2PathSprite;
    public Sprite MultiPathSprite;
    public Sprite WallSprite;
    public Sprite Player1;
    public Sprite Player2;

    private float cellSize = 1f;

    [HideInInspector]
    public Node StartNode;
    [HideInInspector]
    public Node EndNode;

    int gPathCost = 0;

    public Transform TilesHolder;

    public void OnEnable()
    {
        CustomEvents.createTableEvent.AddListener(StartNewGame);
        CustomEvents.colorPathEvent.AddListener(ColorPath);
    }

    private void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    private void Start()
    {
        CreateTable();
    }

    public void OnDisable()
    {
        CustomEvents.createTableEvent.RemoveListener(StartNewGame);
        CustomEvents.colorPathEvent.RemoveListener(ColorPath);
    }

    void ClearTable()
    {
        for (int i = TilesHolder.childCount; i > 0; i--)
        {
            Debug.Log(TilesHolder.GetChild(i).name);
            DestroyImmediate(TilesHolder.GetChild(i));
        }
    }

    public void CreateTable()
    {
        for (int x = 0; x < GameManagerData.GetBoardSize(); x++)
        {
            for (int y = 0; y < GameManagerData.GetBoardSize(); y++)
            {
                var localPosition = GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f;
                GenerateNodeObject(TilesHolder, localPosition, x, y);
            }
        }

        nodeList.AddRange(tempList);
        //remove start node and end node
        tempList.RemoveAll(node => IsStartNode(node.coorX, node.coorY) || IsEndNode(node.coorX, node.coorY));

        nodesArray = tempList.ToArray();

        var listOfObstacles = GetComponent<ObstaclesGenerator>().StartObstaclesGenerator(nodesArray, StartNode, EndNode);

        SetObstaclesToBoard(listOfObstacles);

        CreatePlayers();
    }

    private void GenerateNodeObject(Transform gridParent, Vector3 localPosition, int x, int y)
    {
        bool startNode = IsStartNode(x, y);
        bool endNode = IsEndNode(x, y);

        string nodeName = startNode ? "Start" : endNode ? "End" : "Path";

        GameObject nodeObject = new GameObject(nodeName + x.ToString() + y.ToString());
        Transform transform = nodeObject.transform;
        transform.SetParent(gridParent, false);
        transform.localPosition = localPosition;
        var tempSprite = nodeObject.AddComponent<SpriteRenderer>();
        tempSprite.sprite = startNode ? StartNodeSprite : endNode ? EndNodeSprite : PathSprite;

        var tempNode = nodeObject.AddComponent<Node>();

        tempList.Add(GenerateNodeData(tempNode, x, y, gPathCost));

        gPathCost++;

        if (startNode)
        {
            StartNode = tempNode;
        }

        if (endNode)
        {
            EndNode = tempNode;
        }
    }

    bool IsStartNode(int x, int y)
    {
        return x == GameManagerData.GetStartPointX() && y == GameManagerData.GetStartPointY();
    }

    bool IsEndNode(int x, int y)
    {
        return x == GameManagerData.GetEndPointX() && y == GameManagerData.GetEndPointY();
    }

    Node GenerateNodeData(Node nodeObject, int x, int y, int g)
    {
        Node node = new Node(x, y)
        {
            gCost = g,
            hCost = CalculateHCost(x, y),
            fCost = 0
        };

        nodeObject.coorX = node.coorX;
        nodeObject.coorY = node.coorY;
        nodeObject.gCost = node.gCost;
        nodeObject.hCost = node.hCost;
        nodeObject.fCost = node.CalculateFCost(node.gCost, node.hCost);

        nodeObject.isWalkable = true;

        return nodeObject;
    }

    private int CalculateHCost(int currentX, int currentY)
    {
        return Mathf.Abs(GameManagerData.GetEndPointX() - currentX)
             + Mathf.Abs(GameManagerData.GetEndPointY() - currentY);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    private void SetObstaclesToBoard(List<Coordinate> listOfObstacles)
    {
        foreach (var item in listOfObstacles)
        {
            for (int i = 0; i < TilesHolder.childCount; i++)
            {
                var node = TilesHolder.GetChild(i).GetComponent<Node>();

                if (item.x == node.coorX && item.y == node.coorY)
                {
                    node.isWalkable = false;
                    TilesHolder.GetChild(i).GetComponent<SpriteRenderer>().sprite = WallSprite;
                }
            }
        }
    }

    public Transform playerPrefab;

    private void CreatePlayers()
    {
        for (int i = 0; i < 2; i++)
        {
            var localPosition = GetWorldPosition(StartNode.coorX, StartNode.coorY) + new Vector3(cellSize, cellSize) * 0.5f;

            float xOffset = i == 0 ? localPosition.x - 0.15f : localPosition.x + 0.15f;
            float yOffset = i == 0 ? localPosition.y + 0.09f : localPosition.y - 0.05f;

            localPosition = new Vector3(xOffset, yOffset, localPosition.z);

            var player = Instantiate(playerPrefab, localPosition, Quaternion.identity);
            player.name = "Player" + (i+1).ToString();
            Transform transform = player.transform;
            transform.SetParent(TilesHolder.parent, false);
            var tempSprite = player.GetComponent<SpriteRenderer>();
            tempSprite.sprite = i == 0 ? Player1 : Player2;
            player.GetComponent<PlayerController>().playerAlgorithmType = i == 0 ? AlgorithmType.CurrentAlgorithmType.A_star : AlgorithmType.CurrentAlgorithmType.Greedy_suboptimal;
            player.gameObject.AddComponent<TimmerController>();
        }

    }

    private void ColorPath(string playerName, Node node, List<Node> playerNodesList)
    {
        for (int i = 0; i < TilesHolder.childCount; i++)
        {
            var currentNode = TilesHolder.GetChild(i).GetComponent<Node>();

            if (!IsStartNode(currentNode.coorX, currentNode.coorY) && !IsEndNode(currentNode.coorX, currentNode.coorY))
            {
                if (currentNode.coorX == node.coorX && currentNode.coorY == node.coorY)
                {
                    TilesHolder.GetChild(i).GetComponent<SpriteRenderer>().sprite = (playerName == "Player1") ? Player2PathSprite : Player1PathSprite;
                    if (CheckIfBouthPlayersGoOverThisNode(playerName, playerNodesList, currentNode))
                    {
                        TilesHolder.GetChild(i).GetComponent<SpriteRenderer>().sprite = MultiPathSprite;
                    }
                    return;
                }
            }
        }
    }

    List<Node> playerOneNodes = new List<Node>();
    List<Node> playerTwoNodes = new List<Node>();

    private bool CheckIfBouthPlayersGoOverThisNode(string playerName, List<Node> playerNodes, Node currentNode)
    {
        if (playerName == "Player1")
        {
            playerOneNodes.AddRange(playerNodes);
        }

        if (playerName == "Player2")
        {
            playerTwoNodes.AddRange(playerNodes);
        }

        var listFinal = playerOneNodes.Intersect(playerTwoNodes);

        return listFinal.Contains(currentNode);
    }
}
