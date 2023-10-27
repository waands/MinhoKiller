using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    public int playerId = 0;
    public Animator bottomAnimator;
    public Animator topAnimator;
<<<<<<< HEAD
    //public GridGraph gridGraph;
=======
    public ContactFilter2D movementFilter;
    public float collisionOffset = 0.05f;
    public float moveSpeed = 1f;
>>>>>>> 1ceaa8d9d10ea41fad9dd55e30eb386b19a24a0e
    //public GameObject crosshair;
    //public GameObject arrowPrefab;



    bool isAiming; // Para rastrear se o botão do mouse esquerdo está pressionado

    //private float crosshairAngle = 0.0f; // Ângulo da crosshair em relação ao jogador
    //public float crosshairRadius = 0.5f; // Raio da crosshair

    Vector2 movement;
    Vector3 mouseMovement;
    Vector3 mousePosition;
    Vector3 crosshairDirection;

    Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        ProcessInputs();
        Animate();
        Move();
        //Node currentNode = gridGraph.GetNodeFromWorldPosition(transform.position);
        //AimAndShoot();      
    }

    private void ProcessInputs() {
    }

    private void Animate() {
        //andando
        bottomAnimator.SetFloat("Horizontal", movement.x);
        bottomAnimator.SetFloat("Vertical", movement.y);
        bottomAnimator.SetFloat("Magnitude", movement.magnitude);

        //andando
        topAnimator.SetFloat("MoveHorizontal", movement.x);
        topAnimator.SetFloat("MoveVertical", movement.y);
        topAnimator.SetFloat("MoveMagnitude", movement.magnitude);
    }

    private void Move() {
        // transform.position = transform.position + movement * Time.deltaTime;
        // if (movement.magnitude > 1)
        // {   movement.Normalize();    }

        if(movement != Vector2.zero){
            Debug.Log(movement);
            int count = rb.Cast(
                movement,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset
            );
            if(count == 0){
                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);    
            }
        }
    }

    void OnMove(InputValue movementValue){
        movement = movementValue.Get<Vector2>();
    }
}

