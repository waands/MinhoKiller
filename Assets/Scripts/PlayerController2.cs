using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public int playerId = 0;
    public Animator bottomAnimator;
    public Animator topAnimator;
    public float speed = 1f;  // Adjust the speed as needed
    Rigidbody2D rb;

    bool isAiming;
    Vector3 movement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
        Move();
    }

    private void ProcessInputs()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
    }

    private void Animate()
    {
        bottomAnimator.SetFloat("Horizontal", movement.x);
        bottomAnimator.SetFloat("Vertical", movement.y);
        bottomAnimator.SetFloat("Magnitude", movement.magnitude);

        topAnimator.SetFloat("MoveHorizontal", movement.x);
        topAnimator.SetFloat("MoveVertical", movement.y);
        topAnimator.SetFloat("MoveMagnitude", movement.magnitude);
    }

    private void Move()
    {
        Vector3 newPosition = transform.position + movement * speed * Time.deltaTime;

        // Obtenha o Collider do Player
        Collider2D[] playerColliders = transform.GetChild(1).GetComponentsInChildren<Collider2D>(); // Ajuste o índice 0 se houver apenas um filho

        foreach (Collider2D playerCollider in playerColliders)
        {
            RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, movement.normalized, movement.magnitude * speed * Time.deltaTime);
            
            if (hit.collider != null && hit.collider.CompareTag("PAREDE"))
            {
                // Obter a direção oposta ao movimento atual
                Vector3 oppositeDirection = -movement.normalized;

                // Aplicar um pequeno deslocamento para fora da parede na direção oposta
                newPosition += oppositeDirection * 0.1f;
            }
        }

        transform.position = newPosition;
    }
}