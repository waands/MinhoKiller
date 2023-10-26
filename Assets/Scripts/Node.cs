using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Vector2Int gridPosition;
    public List<Node> neighbors = new List<Node>();

    public Node(int x, int y)
    {
        this.gridPosition = new Vector2Int(x, y);
    }
}


