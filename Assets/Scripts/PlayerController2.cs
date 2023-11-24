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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, 0.1f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("PAREDE"))
            {
                return;  // Do not update position if there's a collision with an obstacle
            }
        }

        transform.position = newPosition;
    }
}