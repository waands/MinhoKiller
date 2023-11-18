using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Blocker : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.StartsWith("Player")) // Check if it's the player
        {
            // Bump the player
            Rigidbody2D playerRigidbody = coll.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                Vector2 bumpDirection = (coll.transform.position - transform.position).normalized;
                float bumpForce = 40f; // Adjust the force as needed
                playerRigidbody.AddForce(bumpDirection * bumpForce);

            }
        }
    }
}