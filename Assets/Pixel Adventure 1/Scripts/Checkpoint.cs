using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private UIManager uiManager; // Reference to the UIManager component

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>(); // Find the UIManager component in the scene
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            uiManager.DisplayGameOver(); // Display the game over screen
        }
    }
}
