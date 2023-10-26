using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Aqui você pode colocar o código a ser executado quando a colisão 2D ocorrer.
        Debug.Log("Colisão 2D detectada com: " + collision.gameObject.name);
    }
}
