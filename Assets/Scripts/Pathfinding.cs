using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Pathfinding {
    private Grid grid;

    public Pathfinding(Grid grid) {
        this.grid = grid;
    }

    public List<Node> FindPath(Vector3 startPos, Vector3 targetPos) {
        grid.GetXy(startPos, out int startX, out int startY);
        grid.GetXy(targetPos, out int endX, out int endY);

        Node startNode = new Node(startX, startY);
        Node endNode = new Node(endX, endY);

        List<Node> openList = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();
        openList.Add(startNode);

        while (openList.Count > 0) {
            Node currentNode = openList[0];
            for (int i = 1; i < openList.Count; i++) {
                if (openList[i].fCost < currentNode.fCost || 
                    openList[i].fCost == currentNode.fCost && 
                    openList[i].hCost < currentNode.hCost) {
                    currentNode = openList[i];
                }
            }

            openList.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode.x == endNode.x && currentNode.y == endNode.y) {
                endNode = currentNode; // Update endNode's properties (like parent)
                return RetracePath(startNode, endNode);
            }

            Debug.Log("neighbors");
            Debug.Log(GetNeighbors(currentNode));
            foreach (Node neighbor in GetNeighbors(currentNode)) {
                if (grid.GetValue(neighbor.x, neighbor.y) != 0 || closedSet.Contains(neighbor)) {
                    continue;
                }

                int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                if (newMovementCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor)) {
                    Debug.Log("newMovementCostToNeighbor");
                    Debug.Log(newMovementCostToNeighbor);
                    neighbor.gCost = newMovementCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, endNode);
                    neighbor.parent = currentNode;

                    if (!openList.Contains(neighbor))
                        openList.Add(neighbor);
                }
            }
        }

        return null; // No path found
    }

    private int GetDistance(Node a, Node b) {
        int dstX = Mathf.Abs(a.x - b.x);
        int dstY = Mathf.Abs(a.y - b.y);
        return dstX + dstY;
    }

    private List<Node> GetNeighbors(Node node) {
        List<Node> neighbors = new List<Node>();
        
        // Define possible movements: Up, Down, Left, Right
        int[] dx = { 0, 0, 1, -1 };
        int[] dy = { 1, -1, 0, 0 };
        
        for (int i = 0; i < 4; i++) {
            int checkX = node.x + dx[i];
            int checkY = node.y + dy[i];
    
            if (checkX >= 0 && checkX < grid.width && checkY >= 0 && checkY < grid.height) {
                neighbors.Add(new Node(checkX, checkY));
            }
        }
    
        return neighbors;
    }

private List<Node> RetracePath(Node startNode, Node endNode) {
    List<Node> path = new List<Node>();
    Node currentNode = endNode;

    while (currentNode != null && (currentNode.x != startNode.x || currentNode.y != startNode.y)) {
        path.Add(currentNode);
        currentNode = currentNode.parent;
    }

    // If we exited the loop because currentNode became null and not because we reached startNode, 
    // then there's an issue with the pathfinding logic.
    if (currentNode == null) {
        Debug.Log("Failed to retrace path. A node's parent was not set.");
        return path;
    }

    path.Reverse();
    return path;
}
}
