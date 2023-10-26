using System.Collections.Generic;
using UnityEngine;



public class GridGraph : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float cellSize = 1f;
    public bool showGizmos = true;
    public Transform playerTransform;


    private Node[,] nodes;

    private void Start()
    {
        GenerateGrid();
        ConnectNodes();
    }

    private void GenerateGrid()
    {
        nodes = new Node[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                nodes[x, y] = new Node(x, y);
            }
        }
    }

    private void ConnectNodes()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x > 0)
                    nodes[x, y].neighbors.Add(nodes[x - 1, y]);
                if (x < width - 1)
                    nodes[x, y].neighbors.Add(nodes[x + 1, y]);
                if (y > 0)
                    nodes[x, y].neighbors.Add(nodes[x, y - 1]);
                if (y < height - 1)
                    nodes[x, y].neighbors.Add(nodes[x, y + 1]);
            }
        }
    }

    private void OnDrawGizmos()
{
    if (showGizmos && nodes != null)
    {
        Node playerNode = GetNodeFromWorldPosition(playerTransform.position);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                     float xOffset = width * cellSize * 0.5f;
                float yOffset = height * cellSize * 0.5f;
                                Vector3 center = new Vector3(x * cellSize - xOffset, y * cellSize - yOffset);


                // If this is the player's node, change the color to blue
                if (nodes[x, y] == playerNode)
                {
                    Gizmos.color = Color.blue;
                }
                else
                {
                    Gizmos.color = Color.white;
                }

                Gizmos.DrawWireCube(center, Vector3.one * cellSize);
            }
        }
    }
}

public Node GetNodeFromWorldPosition(Vector3 worldPosition)
{
    // Adjust according to the offset you've introduced for the grid drawing
    float xOffset = width * cellSize * 0.5f;
    float yOffset = height * cellSize * 0.5f;

    // Convert world position to grid position
    int x = Mathf.FloorToInt((worldPosition.x + xOffset) / cellSize);
    int y = Mathf.FloorToInt((worldPosition.y + yOffset) / cellSize);

    // Clamp the values to be within the grid
    x = Mathf.Clamp(x, 0, width - 1);
    y = Mathf.Clamp(y, 0, height - 1);

    return nodes[x, y];
}



}

