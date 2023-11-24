using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Snake : MonoBehaviour
{

    public float speed;// Velocidade
    public float aggression; // Agressividade
    public int tailLengthToReproduce;

    public int startingSize = 1;

public bool shouldReproduce = true;

    Vector2 dir = Vector2.right;
     private int arrowHitCount = 0;
    private int hitsToShrink = 10; // Número de acertos necessários para encolher
    public ScoreManager scoreManager; // Referência para o script do temporizador

    //vidas
    int vida = 3;
    public Animator vidaAnimator;

    List<Transform> tail = new List<Transform>();

    bool ate = false;

    // Tail Prefab
    public GameObject victoryMenu;
    public GameObject tailPrefab;
    public GameObject tailEndPrefab; // Assign this in the Inspector
    public GameObject gameOverMenu;

    public Transform player;


    // public Vector3 initialPosition = new Vector3(0, 0);
    public SpawnFood spawnFood;
    public Testing batata;
    public Transform snakey;

    private Grid grid;
    private List<Node> path;
    private Vector3 closestFruta;

    // Use this for initialization
    void Start()
    {

        AddInitialTailSegments(startingSize);

        this.grid = batata.grid;
        // grid.GetPath(snakey.position, snakey.position);


        // Vector3 foodPosition = spawnFood.getFruta();
        // path = grid.GetPath(snakey.position, foodPosition);


        // Move the Snake every 150ms
        InvokeRepeating("Move", speed, speed);
    }

    public Snake Reproduce()
    {
        // Verifica se tem segmentos de cauda suficientes para a reprodução
        if (tail.Count < 2)
        {
            Debug.Log("Não há cauda suficiente para a reprodução.");
            return null;
        }

        // Determina o ponto de divisão da cauda
        int midPoint = tail.Count / 2;

        // Armazena a posição do último segmento de cauda que será removido
        Vector3 lastSegmentPosition = tail[midPoint].position;

        // Remove os segmentos de cauda da minhoca original, exceto o último
        for (int i = midPoint; i < tail.Count - 1; i++)
        {
            // Update the grid to mark the position as unoccupied
            Vector2 tailSegmentPosition = tail[i].position;
            int tailSegmentGridX, tailSegmentGridY;
            grid.GetXy(tailSegmentPosition, out tailSegmentGridX, out tailSegmentGridY);
            grid.SetOccupied(tailSegmentGridX, tailSegmentGridY, false);

            // Destroy the tail segment game object
            Destroy(tail[i].gameObject);
        }
        tail.RemoveRange(midPoint, tail.Count - midPoint - 1);

        // Cria a nova minhoca
        GameObject offspringObject = Instantiate(this.gameObject);
        Snake offspring = offspringObject.GetComponent<Snake>();

        // Usa a posição do último segmento removido como a posição de spawn da nova minhoca
        offspring.transform.position = lastSegmentPosition;

        // Ajusta genes
        offspring.speed = this.speed + Random.Range(-0.05f, 0.05f);
        if (offspring.speed < 0.05f) offspring.speed = 0.05f;
        offspring.aggression = this.aggression + Random.Range(-1f, 2f);
        offspring.tailLengthToReproduce = this.tailLengthToReproduce + Random.Range(-1, 2);
        if (offspring.tailLengthToReproduce < 2) offspring.tailLengthToReproduce = 2;

        return offspring;
    }






    private float RandomMutation()
    {
        // Retorna uma pequena variação aleatória
        return Random.Range(-1f, 1f);
    }

    void AddInitialTailSegments(int initialSize)
    {
        Vector2 position = transform.position; // Start at the current position of the snake
        for (int i = 0; i < initialSize; i++)
        {
            {
                // Subtract the direction to position the tail segment
                position -= dir;

                // Determine if this is the last segment
                GameObject prefabToInstantiate = (i == initialSize - 1) ? tailEndPrefab : tailPrefab;

                // Instantiate the correct prefab
                GameObject tailSegment = Instantiate(prefabToInstantiate, position, Quaternion.identity);

                // Add the new segment to the tail list
                tail.Add(tailSegment.transform);
            }
        }
    }


    void SetTail()
    {

        tail.Last().position = transform.position;

        // Add to front of list, remove from the back.
        tail.Insert(0, tail.Last());
        tail.RemoveAt(tail.Count - 1);

    }
    void FollowPath()
    {
        if (path != null && path.Count > 0)
        {
            Node nextNode = path[0];

            Vector2 v = transform.position;
            Vector3 nextNodePosition = grid.GetWorldPosition(nextNode.x, nextNode.y);
            dir = (new Vector2(nextNodePosition.x, nextNodePosition.y) - v).normalized;

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
            // if (path.Count == 0)
            // {
            //     Vector3 foodPosition = spawnFood.getFruta();
            //     path = grid.GetPath(transform.position, foodPosition);
            // }

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

                // If more than one Tail Element
                // get second last position



                // move second last to where head was
                if (tail.Count > 1)
                {
                    tail.Last().position = tail[tail.Count - 2].position;
                    tail[tail.Count - 2].position = v;
                    tail.Insert(0, tail[tail.Count - 2]);
                    tail.RemoveAt(tail.Count - 2);

                }
                else
                {



                    // Move last Tail Element to where the Head was.
                    tail.Last().position = v;

                    // Add to front of list, remove from the back.
                    tail.Insert(0, tail.Last());
                    tail.RemoveAt(tail.Count - 1);
                }



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
        // if (path != null && path.Count > 0)
        // {
        //     FollowPath();
        // }
        // else
        // {
        // Recalculate path to food if needed
        path = grid.GetPath(transform.position, closestFruta);
        FollowPath();
    }
    // }
    bool ShouldChasePlayer()
    {
        // Convert Vector3 to Vector2 (ignoring Z-axis)
        Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPosition2D = new Vector2(player.position.x, player.position.y);
        Vector2 foodPosition2D = new Vector2(closestFruta.x, closestFruta.y);

        // Calculate distances
        float distanceToPlayer = Vector2.Distance(position2D, playerPosition2D);
        float distanceToFood = Vector2.Distance(position2D, foodPosition2D);

        // Check if the player is within the grid bounds
        bool isPlayerInGrid = grid.IsInGrid(player.position);

        // Chase player if within grid bounds and closer than food
        return isPlayerInGrid && distanceToPlayer - aggression < distanceToFood;
    }

    void MoveSnakeBodyOnGrid(Vector2 newPosition)
    {
        // Convert the world position to grid coordinates
        int gridX, gridY;
        grid.GetXy(newPosition, out gridX, out gridY);

        // Update grid for the previous position of the last tail segment
        if (tail.Count > 0)
        {
            Vector2 lastTailPosition = tail.Last().position;
            int lastGridX, lastGridY;
            grid.GetXy(lastTailPosition, out lastGridX, out lastGridY);
            grid.SetOccupied(lastGridX, lastGridY, false);
        }

        // Update the grid for the new head position
        grid.SetOccupied(gridX, gridY, true);
    }


    void Move()
    {
        adjustRotation();
        this.closestFruta = spawnFood.getFruta(transform.position);
        // Decide whether to chase the player or go for the food
        if (ShouldChasePlayer())
        {
            ChasePlayer();
        }
        else
        {
            MoveTowardsFood();
        }
        MoveSnakeBodyOnGrid(transform.position);
        grid.LogGrid();

        if (tail.Count > this.tailLengthToReproduce && shouldReproduce) Reproduce();

    }
    void adjustRotation()
    {
        // Calculate the direction to the next node
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Round(angle / 90) * 90; // Round to the nearest multiple of 90
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Update the rotation of each tail segment
        for (int i = 0; i < tail.Count; i++)
        {
            Vector2 nextDir;
            if (i == 0)
            {
                nextDir = (Vector2)(transform.position - tail[i].position).normalized;
            }
            else
            {
                nextDir = (Vector2)(tail[i - 1].position - tail[i].position).normalized;
            }
            float nextAngle = Mathf.Atan2(nextDir.y, nextDir.x) * Mathf.Rad2Deg;
            // nextAngle = Mathf.Round(nextAngle / 90) * 90; // Round to the nearest multiple of 90
            tail[i].rotation = Quaternion.Euler(0, 0, nextAngle);
        }
    }





    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log(coll.name.StartsWith("flecha"));
        Debug.Log("Snake collided with " + coll.name);
        // Food?
        if (coll.name.StartsWith("FoodPrefab"))
        {
            // Get longer in next Move call
            ate = true;
            // Remove the Food
            Destroy(coll.gameObject);
            spawnFood.ate(coll.transform.position);
            // Reduce snake speed when eat
            // if (grid.SNAKE_SPEED < 0.3f) grid.SNAKE_SPEED += 0.05f;
            // Update the grid for the new tail segment
            if (tail.Count > 0)
            {
                Vector2 newTailPosition = tail.Last().position;
                grid.SetOccupied((int)newTailPosition.x, (int)newTailPosition.y, true);
            }
        }
        else if (coll.name.StartsWith("flecha")) // Verifica se a cabeça colidiu com uma flecha
        {
            Destroy(coll.gameObject);
             // Incrementa o contador de acertos
            arrowHitCount++;

            // Verifica se atingiu o número necessário de acertos
            if (arrowHitCount >= hitsToShrink)
            {
                arrowHitCount = 0; // Reseta o contador
                Debug.Log("AAAAAAA");
                if (tail.Count > 1)
                {
            // Remove o penúltimo segmento do corpo (mantendo a cauda)
            GameObject bodySegment = tail[tail.Count - 2].gameObject;
            tail.RemoveAt(tail.Count - 2);
            Destroy(bodySegment);
                }
            else {
             Destroy(tail[0].gameObject); // Destrói o GameObject da cauda
             Destroy(gameObject); // Destrói o GameObject da cobra
             scoreManager.StopTimer(); // Para o temporizador
             victoryMenu.SetActive(true);
            }
            }
        }
        else if (coll.name.StartsWith("Player")) // Check if it's the player
        {
            Debug.Log("Archer collided with snake");
            // Dá a tela de game over
            // Carrega a cena atual novamente para resetar o jogo
            if (vida > 1) {
                vida--;
                vidaAnimator.SetInteger("vidas", vida);
            }else {
                vida--;
                vidaAnimator.SetInteger("vidas", vida);
                gameOverMenu.SetActive(true);
                scoreManager.StopTimer(); // Para o temporizador
            }
            // Bump the player
            Rigidbody2D playerRigidbody = coll.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 bumpDirection = (coll.transform.position - transform.position).normalized;
                float bumpForce = 90f; // Adjust the force as needed
                playerRigidbody.AddForce(bumpDirection * bumpForce);

            }
        }
    }

}