using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform playerTransform;
    public Animator topAnimator;
    public GameObject crosshair;
    public float crosshairRadius = 0.5f;

    bool isAiming; // Para rastrear se o botão do mouse esquerdo está pressionado

    private float crosshairAngle = 0.0f;
    Vector3 crosshairDirection;

    void Update()
    {
        ProcessInputs();
        AimAndShoot();
    }

    private void ProcessInputs()
    {
        // Obtenha a posição do mouse no mundo
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calcule o vetor da crosshair a partir do jogador para o mouse
        crosshairDirection = (mousePosition - playerTransform.position).normalized;

        // Verifique se o botão do mouse esquerdo foi pressionado
        if (Input.GetMouseButtonDown(0))
        {
            isAiming = true;
        }
        // Verifique se o botão do mouse esquerdo foi solto
        if (Input.GetMouseButtonUp(0))
        {
            isAiming = false;
            Shoot();
        }
    }

    private void AimAndShoot()
    {
        // Calcule o ângulo em radianos
        crosshairAngle = Mathf.Atan2(crosshairDirection.y, crosshairDirection.x);

        // Calcule as coordenadas X e Y da crosshair com base no ângulo e no raio
        float crosshairX = playerTransform.position.x + crosshairRadius * Mathf.Cos(crosshairAngle);
        float crosshairY = playerTransform.position.y + crosshairRadius * Mathf.Sin(crosshairAngle);

        // Defina a posição da crosshair
        crosshair.transform.position = new Vector3(crosshairX, crosshairY, 0.0f);

        topAnimator.SetFloat("AimHorizontal", crosshairDirection.x);
        topAnimator.SetFloat("AimVertical", crosshairDirection.y);
        topAnimator.SetFloat("AimMagnitude", crosshairDirection.magnitude);
        topAnimator.SetBool("Aim", isAiming);
    }

    private void Shoot()
    {
        Vector2 arrowDirection = new Vector2(crosshairDirection.x, crosshairDirection.y).normalized;
        Vector2 playerCenter = new Vector2(
       playerTransform.position.x,
       playerTransform.position.y + playerTransform.GetComponent<Collider2D>().bounds.extents.y
   );

        GameObject arrow = Instantiate(arrowPrefab, playerCenter, Quaternion.identity);
        arrow.GetComponent<Rigidbody2D>().velocity = arrowDirection * 2.5f;
        arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg);
        Destroy(arrow, 2.0f);
    }


}
