using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using CodeMonkey.Utils;

public class Grid
{

    public int width;
    public int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;
    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width, height];
        debugTextArray = new TextMesh[width, height];   


        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                //debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize,cellSize) * .5f, 20, Color.white, TextAnchor.MiddleCenter);
                debugTextArray[x, y] = UtilsClass.CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f, 6, Color.white, TextAnchor.MiddleCenter);

                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white , 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white , 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white , 100f);
    }

    public Vector3 GetWorldPosition(int x, int y){
        return new Vector3(x, y) * cellSize + originPosition;
    }

    public void GetXy(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    public void SetValue(int x, int y, int value){
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }
    public void SetValue(Vector3 worldPosition, int value){
        int x, y;
        GetXy(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y){
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition){
        int x, y;
        GetXy(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    private Pathfinding pathfinding;
    // Start is called before the first frame update
    public void Awake() {
        pathfinding = new Pathfinding(this);
    }
    public void Start()
    {
        
    }

    public void GetPath(Vector3 startPos, Vector3 targetPos)
{

    if(pathfinding == null) {
        pathfinding = new Pathfinding(this);
    }
    List<Node> path = pathfinding.FindPath(startPos, targetPos);
    Debug.Log("pathfinding rseult");
    // for each path debug
    if (path != null) {
        for (int i = 0; i < path.Count - 1; i++) {
            Debug.DrawLine(GetWorldPosition(path[i].x, path[i].y) + new Vector3(cellSize, cellSize) * 0.5f, GetWorldPosition(path[i + 1].x, path[i + 1].y) + new Vector3(cellSize, cellSize) * 0.5f, Color.green, 100f);
        }
    }
}
}
