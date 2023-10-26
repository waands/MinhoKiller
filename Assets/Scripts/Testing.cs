using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using GridPathfindingSystem;


public class testing : MonoBehaviour
{
    private GridPathfinding pathfinding;
    private void Start()
    {
        pathfinding = new GridPathfinding(10,10,2f, new Vector3(-5,-5));
    }

    private void Update()
    {
    
    }
}
