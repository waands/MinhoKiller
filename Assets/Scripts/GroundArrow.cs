using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundArrow : MonoBehaviour
{
    public Vector2 position; // A posição da flecha no chão
    public Vector2 direction; // A direção da flecha no chão
    public float speed; // A velocidade da flecha no chão
    public bool isActive; // Para verificar se a flecha está ativa no chão
    public Animator arrowGround;
    public bool colliding = false;

    public void SetColliding(bool value)
    {
        colliding = value;
        arrowGround.SetBool("colliding", value);
        StartCoroutine(DisableCollidingAnimation());
    }
    IEnumerator DisableCollidingAnimation()
    {
        // Espere por um segundo
        yield return new WaitForSeconds(0.5f);

        // Desative a animação de iteração
        arrowGround.SetBool("colliding", false);
    }

}
