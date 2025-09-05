using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;


public class UIManager : MonoBehaviour
{
    public GameObject startScreen; // Reference to the Start screen UI
    public InputField playerNameInput; // Reference to the Input Field for player name
    public Button playButton; // Reference to the Play Button
    public GameObject gameOverScreen; // Reference to the Game Over screen UI
    public Text gameOverScoreText; // Reference to the score text on the Game Over screen
    public Button restartButton; // Reference to the restart button UI

    void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClicked); // Add listener to the play button
        restartButton.onClick.AddListener(RestartGame); // Add listener to the restart button
        ShowStartScreen(); // Display the start screen at the beginning
    }

    void ShowStartScreen()
    {
        startScreen.SetActive(true); // Display the start screen
        gameOverScreen.SetActive(false); // Hide the game over screen
        Time.timeScale = 0f; // Pause the game
    }

    void OnPlayButtonClicked()
    {
        if (string.IsNullOrEmpty(playerNameInput.text))
        {
            Debug.Log("Player name is required."); // Do nothing if the input field is empty
        }
        else
        {
            StartGame(); // Start the game if the input field has a value
        }
    }

    void StartGame()
    {
        startScreen.SetActive(false); // Hide the start screen
        Time.timeScale = 1f; // Resume the game
        // Optionally, store the player name for later use
        PlayerPrefs.SetString("PlayerName", playerNameInput.text);
    }

    public void DisplayGameOver()
    {
        gameOverScreen.SetActive(true); // Display the Game Over screen
        gameOverScoreText.text = "Score: " + ScoreManager.instance.GetScore(); // Display the player's score
        Time.timeScale = 0f; // Pause the game
				StartCoroutine(SendGameResult(PlayerPrefs.GetString("PlayerName", "No name"), ScoreManager.instance.GetScore())); // Send the game result
    }

    void RestartGame()
    {
        Time.timeScale = 1f; // Resume the game
        ScoreManager.instance.ResetScore(); // Reset the score
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }

		
		IEnumerator SendGameResult(string username, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post("https://songhwachae.com/ninja_frog/api/game_score.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Game result sent successfully.");
            }
            else
            {
                Debug.LogError("Error sending game result: " + www.error);
            }
        }
    }

}
