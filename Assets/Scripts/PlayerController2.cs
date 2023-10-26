using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public int playerId = 0;
    public Animator bottomAnimator;
    public Animator topAnimator;
    //public GameObject crosshair;
    //public GameObject arrowPrefab;



    bool isAiming; // Para rastrear se o botão do mouse esquerdo está pressionado

    //private float crosshairAngle = 0.0f; // Ângulo da crosshair em relação ao jogador
    //public float crosshairRadius = 0.5f; // Raio da crosshair

    Vector3 movement;
    Vector3 mouseMovement;
    Vector3 mousePosition;
    Vector3 crosshairDirection;

    void Awake() {
        //player = ReInput.players.GetPlayer(playerId);
        //Cursor.lockState = CursorLockMode.Locked;
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
        //AimAndShoot();      
    }

    private void ProcessInputs() {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        // Obtenha a posição do mouse no mundo
     /*   mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calcule o vetor da crosshair a partir do jogador para o mouse
        crosshairDirection = (mousePosition - transform.position).normalized;

        // Verifique se o botão do mouse esquerdo foi pressionado
        if (Input.GetMouseButtonDown(0)){   
            isAiming = true;    
        }
        // Verifique se o botão do mouse esquerdo foi solto
        if (Input.GetMouseButtonUp(0)) {   
            isAiming = false;
            Shoot();            
        }*/
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

        //atirando
/*        topAnimator.SetFloat("AimHorizontal", crosshairDirection.x);
        topAnimator.SetFloat("AimVertical", crosshairDirection.y);
        topAnimator.SetFloat("AimMagnitude", crosshairDirection.magnitude);
        topAnimator.SetBool("Aim", isAiming);*/
    }

    private void Move() {
        transform.position = transform.position + movement * Time.deltaTime;
        if (movement.magnitude > 1)
        {   movement.Normalize();    }
    }    
/*
    private void AimAndShoot()
    {
        // Calcule o ângulo em radianos
        crosshairAngle = Mathf.Atan2(crosshairDirection.y, crosshairDirection.x);

        // Calcule as coordenadas X e Y da crosshair com base no ângulo e no raio
        float crosshairX = transform.position.x + crosshairRadius * Mathf.Cos(crosshairAngle);
        float crosshairY = transform.position.y + crosshairRadius * Mathf.Sin(crosshairAngle);

        // Defina a posição da crosshair
        crosshair.transform.position = new Vector3(crosshairX, crosshairY, 0.0f);

        Vector3 crosshairPosition = new Vector3(crosshairX, crosshairY, 0.0f);
    }
    *//*
    private void Shoot()
    {
        Vector3 crosshairDirection = (crosshair.transform.position - transform.position).normalized;
        Vector2 arrowDirection = new Vector2(crosshairDirection.x, crosshairDirection.y).normalized;
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = arrowDirection * 2.5f;
        arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg);
        Destroy(arrow, 2.0f);
    }
    *//*
    void OnCollisionEnter2D(Collision2D collision)
    {
    if (collision.gameObject.CompareTag("GroundArrow")) // Verifique a tag ou outra forma de identificar as flechas fincadas no chão
        {
            Debug.Log("encostou");
            // Calcule a direção de ricochete (direção simétrica em relação à normal da colisão)
            Vector2 incomingDirection = GetComponent<Rigidbody2D>().velocity.normalized;
            Vector2 normal = collision.contacts[0].normal;
            Vector2 newDirection = Vector2.Reflect(incomingDirection, normal).normalized;

            // Aplique a nova direção à flecha no ar
            GetComponent<Rigidbody2D>().velocity = newDirection * 2.5f;
        }
    }*/

}

