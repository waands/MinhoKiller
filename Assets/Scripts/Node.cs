using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Node : IComparable<Node>
{
    public Node parentNode;
    public Vector3 worldPosition;
    public int gCost;
    public int hCost;
    public int fCost { get { return gCost + hCost; } }
    public int x;
    public int y;

    public Node(Vector3 _worldPos, int _x, int _y)
    {
        worldPosition = _worldPos;
        x = _x;
        y = _y;
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}