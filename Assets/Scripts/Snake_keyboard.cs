using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour
{
    // Current Movement Direction
    // (by default it moves to the right)
    Vector2 dir = Vector2.right;

    // Keep Track of Tail
    List<Transform> tail = new List<Transform>();

    // Did the snake eat something?
    bool ate = false;

    // Tail Prefab
    public GameObject tailPrefab;

    public Vector3 initialPosition = new Vector3(0, 0);
    public Testing batata;
    public Transform snakey;
    public Transform fruta;
    private Grid grid;

    // Use this for initialization
    void Start()
    {
        
        // Add 4 initial tail segments
        AddInitialTailSegments(4);

        this.grid = batata.grid;
        Debug.Log(grid);
        grid.GetPath(snakey.position, snakey.position);

        Debug.Log(grid.GetPath(snakey.position, snakey.position));


        // Move the Snake every 300ms
        InvokeRepeating("Move", 0.3f, 0.3f);
    }


    // Update is called once per frame
    void Update()
    {
        // Move in a new Direction?
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
    }

    void AddInitialTailSegments(int initialSize)
    {
        Vector2 position = transform.position; // Start at the current position of the snake
        for (int i = 0; i < initialSize; i++)
        {
            position -= dir; // Subtract the direction to position the tail segment
            GameObject tailSegment = (GameObject)Instantiate(tailPrefab, position, Quaternion.identity);
            tail.Add(tailSegment.transform);
        }
    }



    void Move()
    {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(dir);

        // Ate something? Then insert new Element into gap
        if (ate)
        {
            // Load Prefab into the world
            GameObject g = (GameObject)Instantiate(tailPrefab,
                                                  v,
                                                  Quaternion.identity);

            // Keep track of it in our tail list
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        // Food?
        if (coll.name.StartsWith("FoodPrefab"))
        {
            // Get longer in next Move call
            ate = true;

            // Remove the Food
            Destroy(coll.gameObject);
        }
        // Collided with Tail or Border
        else
        {
            // ToDo 'You lose' screen
        }
    }
}