using UnityEngine;

public class Arrow : MonoBehaviour
{

    public Animator arrowGround;
    public bool colliding;

    public void SetColliding(bool value)
    {
        colliding = value;
        arrowGround.SetBool("colliding", value);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GroundArrow"))
        {
            Debug.Log("Colisão 2D com GroundArrow detectada: " + other.gameObject.name);
            
            GroundArrow[] groundArrows = FindObjectsOfType<GroundArrow>();
            
            if (groundArrows.Length > 1)
            {
                // Se houver mais de uma Ground Arrow no chão, escolha uma delas aleatoriamente.
                GroundArrow randomGroundArrow = groundArrows[Random.Range(0, groundArrows.Length)];
                
                // Redirecione a Air Arrow para a Ground Arrow escolhida.
                RedirectAirArrow(randomGroundArrow);
            }
            else if (groundArrows.Length == 1)
            {
                // Se houver apenas uma Ground Arrow, redirecione a Air Arrow para ela.
                RedirectAirArrow(groundArrows[0]);
            }
        }
    }

    void RedirectAirArrow(GroundArrow targetGroundArrow)
    {
        // Verifique se a Air Arrow e o alvo (Ground Arrow) estão válidos
        if (targetGroundArrow != null && GetComponent<Rigidbody2D>() != null)
        {
            targetGroundArrow.SetColliding(true);
            // Calcule a direção da Air Arrow para o alvo
            Vector2 direction = (targetGroundArrow.transform.position - transform.position).normalized;
            
            // Ajuste a velocidade da Air Arrow para apontar na direção do alvo
            GetComponent<Rigidbody2D>().velocity = direction * 2.5f;

            // Rode a Air Arrow na direção da velocidade
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            // Defina o valor de colliding usando o método SetColliding da GroundArrow
        }
    }
}
