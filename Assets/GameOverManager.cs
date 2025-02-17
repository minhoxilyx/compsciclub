using UnityEngine;
using TMPro; // Add this namespace for TextMeshPro

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro UI element for the current score
    public TextMeshProUGUI highScoreText; // Reference to the TextMeshPro UI element for the high score
    private EnemySpawner enemySpawner; // Reference to the EnemySpawner
    private GameObject player;

    private int highScore = 0; // Variable to store the high score

    void Start()
    {
        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("High score loaded: " + highScore);
    }

    public void SetEnemy(EnemySpawner enemy)
    {
        enemySpawner = enemy;
        Debug.Log("EnemySpawner reference set: " + (enemySpawner != null));
    }

    public void SetPlayer(GameObject play)
    {
        player = play;
        Debug.Log("Player reference set: " + (player != null));
    }

    public void ShowGameOver()
    {
        Debug.Log("ShowGameOver called. Attempting to activate canvas.");

        // Check if the GameObject is active
        if (gameObject == null)
        {
            Debug.LogError("GameOverManager GameObject is null!");
            return;
        }

        // Activate the canvas
        gameObject.SetActive(true);
        Debug.Log("Canvas activated. Active state: " + gameObject.activeSelf);

        // Check if enemySpawner is assigned
        if (enemySpawner == null)
        {
            Debug.LogError("EnemySpawner reference is not assigned!");
            return;
        }
        else
        {
            Debug.Log("EnemySpawner reference is valid.");
        }

        // Check if scoreText is assigned
        if (scoreText == null)
        {
            Debug.LogError("ScoreText reference is not assigned!");
            return;
        }
        else
        {
            Debug.Log("ScoreText reference is valid.");
        }

        // Check if highScoreText is assigned
        if (highScoreText == null)
        {
            Debug.LogError("HighScoreText reference is not assigned!");
            return;
        }
        else
        {
            Debug.Log("HighScoreText reference is valid.");
        }

        // Get the score from the EnemySpawner
        int currentScore = enemySpawner.numberOfEnemies - 3;
        Debug.Log("Current score calculated: " + currentScore);

        // Update the score text
        scoreText.text = "Score: " + currentScore;
        Debug.Log("Score text updated.");

        // Check if the current score is higher than the high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore); // Save the new high score
            PlayerPrefs.Save(); // Ensure the data is saved immediately
            Debug.Log("New high score set: " + highScore);
        }

        // Update the high score text
        highScoreText.text = "High Score: " + highScore;
        Debug.Log("High score text updated.");
    }
}