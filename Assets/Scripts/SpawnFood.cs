using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnFood : MonoBehaviour {
    // Food Prefab
    public GameObject foodPrefab;

    // Camera reference
    private Camera mainCamera;

    private List<Vector3> foodPositions = new List<Vector3>();

    // Use this for initialization
    void Awake () {
        mainCamera = Camera.main; // Assuming the camera with the "MainCamera" tag is the one you're using
        Spawn();

        // Spawn food every 4 seconds, starting in 3
        //InvokeRepeating("Spawn", 3, 4);
    }

    // Spawn one piece of food
    Vector3 Spawn() {
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = halfHeight * mainCamera.aspect;

        float leftBorder = mainCamera.transform.position.x - halfWidth;
        float rightBorder = mainCamera.transform.position.x + halfWidth;
        float topBorder = mainCamera.transform.position.y + halfHeight;
        float bottomBorder = mainCamera.transform.position.y - halfHeight;

        // x position between left & right border
        float x = Random.Range(leftBorder, rightBorder);

        // y position between top & bottom border
        float y = Random.Range(bottomBorder, topBorder);


        Vector3 v3 = new Vector3(x, y);
        // add to food positions
        foodPositions.Add(v3);

        // Instantiate the food at (x, y)
        Instantiate(foodPrefab,
                    new Vector2(x, y),
                    Quaternion.identity); // default rotation

        return v3;
    }

    public Vector3 getFruta() {
        // check if has food
        if (foodPositions.Count == 0) {
            Vector3 v3 = Spawn();
            foodPositions.RemoveAt(0);
            return v3;
        }
        Vector3 v = foodPositions[0];
        foodPositions.RemoveAt(0);
        return v;
    }
}

