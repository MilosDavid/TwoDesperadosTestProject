using System;
using System.Collections.Generic;
using UnityEngine;

public class Grid <TGridObject>
{
    private float cellSize;
    private TGridObject[,] gridArray;

    public Sprite walkableSpriteImage;
    public Sprite obstacleSpriteImage;

    private Sprite[,] spriteArray;

    private Transform gridParent;
    private Sprite walkableSprite;
    private Sprite obstacleSprite;
    private Sprite startSprite;
    private Sprite endSprite;

    private List<Coordinate> obstaclesCoordinatesList;

    public Grid(float cellSize, Transform gridParent, 
        Sprite walkableSprite, Sprite obstacleSprite, Sprite startSprite, Sprite endSprite,
        List<Coordinate> obstaclesCoordinatesList, Func<Grid<TGridObject>, int, int, TGridObject> CreateGridObject)
    {
        this.cellSize = cellSize;
        this.walkableSpriteImage = walkableSprite;
        this.obstacleSpriteImage = obstacleSprite;
        this.startSprite = startSprite;
        this.endSprite = endSprite;
        this.gridParent = gridParent;

        gridArray = new TGridObject[GameManagerData.GetBoardSize(), GameManagerData.GetBoardSize()];
        spriteArray = new Sprite[GameManagerData.GetBoardSize(), GameManagerData.GetBoardSize()];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                bool isWalkable = true;

                Coordinate current = new Coordinate(x, y);

                foreach (var item in obstaclesCoordinatesList)
                {
                    if (item.x == current.x && item.y == current.y)
                    {
                        isWalkable = false;
                    }    
                }

                gridArray[x, y] = CreateGridObject(this, x, y);
                spriteArray[x,y] = CreateWorldSprite(gridParent, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, isWalkable);
            }
        }

        SetStartAndEndPoint(gridParent);
        SetCameraBasedOnBoardSize();

        //SetValue(2, 1, 33);
    }

    internal void ColorPath(int x, int y)
    {
        int fieldToColor = x * GameManagerData.GetBoardSize() + y;
        gridParent.GetChild(fieldToColor).GetComponent<SpriteRenderer>().sprite = startSprite;
    }

    private void SetStartAndEndPoint(Transform gridParent)
    {
        var startNodePosition = (GameManagerData.GetStartPointX() * GameManagerData.GetBoardSize()) + GameManagerData.GetStartPointY();
        gridParent.GetChild(startNodePosition).GetComponent<SpriteRenderer>().sprite = startSprite;

        var endNodePosition = (GameManagerData.GetEndPointX() * GameManagerData.GetBoardSize()) + GameManagerData.GetEndPointY();
        gridParent.GetChild(endNodePosition).GetComponent<SpriteRenderer>().sprite = endSprite;
    }

    public Sprite CreateWorldSprite(Transform parent = null, Vector3 localPosition = default(Vector3), bool isWalkable = true)
    {
        return CreateWorldSprites(parent, localPosition, isWalkable);
    }

    public Sprite CreateWorldSprites(Transform parent, Vector3 localPosition, bool isWalkable = true)
    {
        string name = isWalkable ? "Path" : "Wall";
        GameObject gameObject = new GameObject(name);
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        var tempSprite = gameObject.AddComponent<SpriteRenderer>();
        tempSprite.sprite = isWalkable ? walkableSpriteImage : obstacleSpriteImage;

        return tempSprite.sprite;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    void SetCameraBasedOnBoardSize()
    {
        Camera.main.orthographicSize = GameManagerData.GetBoardSize() * 1.1f;
        Camera.main.transform.localPosition = new Vector3(GameManagerData.GetBoardSize() / (float)2, Camera.main.transform.localPosition.y, Camera.main.transform.localPosition.z);
    }

    public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt(worldPosition.x / cellSize);
        y = Mathf.FloorToInt(worldPosition.y / cellSize);

        /////////////////////////
        //x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        //y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    void SetGridObject(int x, int y, TGridObject value)
    {
        if (x >= 0 && y >= 0 && x < GameManagerData.GetBoardSize() && y < GameManagerData.GetBoardSize())
        {
            gridArray[x, y] = value;
            //if (OnGridValueChanged != null) OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, y = y });
        }
    }

    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);
    }

    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < GameManagerData.GetBoardSize() && y < GameManagerData.GetBoardSize())
        {
            return gridArray[x, y];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}
