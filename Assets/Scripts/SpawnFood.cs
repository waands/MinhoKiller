using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnFood : MonoBehaviour
{
    // Food Prefab
    public GameObject foodPrefab;

    public Testing batata;
    private Grid grid;


    private List<Vector3> foodPositions = new List<Vector3>();

    // Use this for initialization
    void Awake()
    {
        this.grid = batata.grid;
        Spawn();

        // Spawn food every 4 seconds, starting in 3
        //InvokeRepeating("Spawn", 3, 4);
    }

    // Spawn one piece of food
    Vector3 Spawn()
    {
        int xCell, yCell;
        Vector3 worldPosition;

        while (true)
        {
            // Generate a random position within the grid
            xCell = Random.Range(0, grid.width);
            yCell = Random.Range(0, grid.height);

            // Check if the selected cell is empty
            if (grid.GetValue(xCell, yCell) == 0)
            {
                // Use Grid's method to get the world position of the center of the cell
                worldPosition = grid.GetWorldPosition(xCell, yCell) + new Vector3(grid.cellSize, grid.cellSize) * 0.5f;

                // Exit the loop if an empty cell is found
                break;
            }
        }

        // Add to food positions
        foodPositions.Add(worldPosition);

        // Instantiate the food at the world position
        Instantiate(foodPrefab, worldPosition, Quaternion.identity);

        return worldPosition;
    }



    public void ate()
    {
        if (foodPositions.Count > 0) foodPositions.RemoveAt(0);
    }
    public Vector3 getFruta()
    {
        // check if has food
        if (foodPositions.Count == 0)
        {
            Vector3 v3 = Spawn();
            // foodPositions.RemoveAt(0);
            return v3;
        }
        Vector3 v = foodPositions[0];
        // foodPositions.RemoveAt(0);
        return v;
    }
}

