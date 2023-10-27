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
    private List<Node> path;

    // Use this for initialization
    void Start()
    {
        
        // Add 4 initial tail segments
        AddInitialTailSegments(1);

        this.grid = batata.grid;
        // grid.GetPath(snakey.position, snakey.position);

        path = grid.GetPath(snakey.position, fruta.position);


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



    void Move() {
        // If there's a path and nodes left in it, follow the path.
        if (path != null && path.Count > 0) {
            Node nextNode = path[0];

            Vector2 v = transform.position;

            Vector3 nextNodePosition = grid.GetWorldPosition(nextNode.x, nextNode.y);
            bool movingY = nextNodePosition.y != transform.position.y;

            Debug.Log(movingY);
            if (movingY)
                nextNodePosition.y += grid.cellSize * 0.5f; 
            else 
                nextNodePosition.x -= grid.cellSize * 0.5f; 

            transform.position = nextNodePosition;

            path.RemoveAt(0); // remove the node we just moved to

            // If snake reaches the fruit, get a new path.
            if (path.Count == 0) {
                path = grid.GetPath(transform.position, fruta.position);
            }

            // Handle eating and tail movement.
            if (ate) {
                // Load Prefab into the world.
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

                // Keep track of it in our tail list.
                tail.Insert(0, g.transform);

                // Reset the flag.
                ate = false;
            }
            // Do we have a Tail?
            else if (tail.Count > 0) {
                // Move last Tail Element to where the Head was.
                tail.Last().position = v;

                // Add to front of list, remove from the back.
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
        else {
            // Original move code if there's no path to follow.
            Vector2 v = transform.position;
            transform.Translate(dir);

            if (ate) {
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
                tail.Insert(0, g.transform);
                ate = false;
            }
            else if (tail.Count > 0) {
                tail.Last().position = v;
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
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