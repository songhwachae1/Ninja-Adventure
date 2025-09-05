using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int scoreValue = 10; // Value to increase the score
    public Animator animator; // Reference to the Animator component
		public float delay = 0.5f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ScoreManager.instance.AddScore(scoreValue); // Increase the player's score
            animator.SetTrigger("Collected"); // Trigger the "Collected" animation
            Destroy(gameObject, delay); // Destroy the collectible after a short delay
        }
    }
}
