using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 15f; // Adjustable jump force in the Inspector

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component from the player
            if (playerRb != null)
            {
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce); // Apply jump force to the player
            }
        }
    }
}
