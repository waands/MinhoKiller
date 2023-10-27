using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Node {
    public int x;
    public int y;
    public bool isWalkable;
    public int gCost;
    public int hCost;
    public Node parent;

    public Node(int x, int y) {
        this.x = x;
        this.y = y;
        this.isWalkable = true;
        this.gCost = 0;
        this.hCost = 0;
    }

    public int fCost {
        get {
            return gCost + hCost;
        }
    }
       public override bool Equals(object obj) {
        if (obj == null || GetType() != obj.GetType())
            return false;

        Node other = (Node)obj;
        return x == other.x && y == other.y;
    }

    public override int GetHashCode() {
        // You can use a simple hash code combining method like this
        // or any other method you prefer.
        return x * 31 + y;
    }
}