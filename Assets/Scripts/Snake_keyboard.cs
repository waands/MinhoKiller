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

    public Transform player;


    // public Vector3 initialPosition = new Vector3(0, 0);
    public SpawnFood spawnFood;
    public Testing batata;
    public Transform snakey;

    private Grid grid;
    private List<Node> path;

    // Use this for initialization
    void Start()
    {

        // Add 4 initial tail segments
        AddInitialTailSegments(1);

        this.grid = batata.grid;
        // grid.GetPath(snakey.position, snakey.position);


        // Vector3 foodPosition = spawnFood.getFruta();
        // path = grid.GetPath(snakey.position, foodPosition);


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


    void FollowPath()
    {
        if (path != null && path.Count > 0)
        {
            Node nextNode = path[0];

            Vector2 v = transform.position;
            Vector3 nextNodePosition = grid.GetWorldPosition(nextNode.x, nextNode.y);
            grid.GetXy(transform.position, out int currentX, out int currentY);

            // Determine if the snake is moving vertically based on grid y-values
            bool movingY = nextNode.y != currentY;

            if (movingY)
            {
                nextNodePosition.y += grid.cellSize * 0.5f;
                nextNodePosition.x += grid.cellSize * 0.5f;
            }
            else
            {
                nextNodePosition.x += grid.cellSize * 0.5f;
                nextNodePosition.y += grid.cellSize * 0.5f;
            }

            transform.position = nextNodePosition;

            path.RemoveAt(0); // remove the node we just moved to

            // If snake reaches the fruit, get a new path.
            if (path.Count == 0)
            {
                Vector3 foodPosition = spawnFood.getFruta();
                path = grid.GetPath(transform.position, foodPosition);
            }

            // Handle eating and tail movement.
            if (ate)
            {
                // Load Prefab into the world.
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

                // Keep track of it in our tail list.
                tail.Insert(0, g.transform);

                // Reset the flag.
                ate = false;
            }
            // Do we have a Tail?
            else if (tail.Count > 0)
            {
                // Move last Tail Element to where the Head was.
                tail.Last().position = v;

                // Add to front of list, remove from the back.
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
        else
        {
            // Original move code if there's no path to follow.
            Vector2 v = transform.position;
            transform.Translate(dir);

            if (ate)
            {
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
                tail.Insert(0, g.transform);
                ate = false;
            }
            else if (tail.Count > 0)
            {
                tail.Last().position = v;
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
    }

    void ChasePlayer()
    {
        // Calculate path to the player
        path = grid.GetPath(transform.position, player.position);
        FollowPath();
    }
    void MoveTowardsFood()
    {
        // Existing logic to move towards food
        if (path != null && path.Count > 0)
        {
            FollowPath();
        }
        else
        {
            // Recalculate path to food if needed
            Vector3 foodPosition = spawnFood.getFruta();
            path = grid.GetPath(transform.position, foodPosition);
        }
    }
    bool ShouldChasePlayer()
    {
        // Convert Vector3 to Vector2 (ignoring Z-axis)
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition2D = new Vector2(player.position.x, player.position.y);
        Vector2 foodPosition2D = new Vector2(spawnFood.getFruta().x, spawnFood.getFruta().y);

        // Calculate distances
        float distanceToPlayer = Vector2.Distance(position2D, playerPosition2D);
        float distanceToFood = Vector2.Distance(position2D, foodPosition2D);

        // Chase player if closer than food
        return distanceToPlayer < distanceToFood;
    }

    void Move()
    {
        // Decide whether to chase the player or go for the food
        if (ShouldChasePlayer())
        {
            ChasePlayer();
        }
        else
        {
            MoveTowardsFood();
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
            spawnFood.ate();
        }
        // Collided with Tail or Border
        else
        {
            // ToDo 'You lose' screen
        }
    }
}