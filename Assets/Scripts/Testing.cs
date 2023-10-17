using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class testing : MonoBehaviour
{
    private Grid grid;
    private void Start()
    {
        grid = new Grid(48, 27, 5f, new Vector3(-20, -20));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1)){
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
