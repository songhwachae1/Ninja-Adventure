using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton instance
    public Text scoreText; // Reference to the UI Text component
    private int score; // Variable to keep track of the score

    void Awake()
    {
        if (instance == null)
        {
            instance = this; // Set the instance to this object
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    void Start()
    {
        score = 0; // Initialize score to 0
        UpdateScoreText(); // Update the score display at the start
    }

    public void AddScore(int value)
    {
        score += value; // Increase the score by the given value
        UpdateScoreText(); // Update the score display
    }

    public int GetScore()
    {
        return score; // Return the current score
    }

    public void ResetScore()
    {
        score = 0; // Reset the score to 0
        UpdateScoreText(); // Update the score display
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the score text
    }
}
