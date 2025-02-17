using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add this namespace for TextMeshPro

public class RestartButton : MonoBehaviour
{
    // Reference to the button (assign in the Inspector)
    public Button restartButton;

    // Reference to the EnemySpawner (assign in the Inspector)
    //public EnemySpawner enemySpawner;
    public GameObject GameOverCanvas;
    public PlayerSpawner playerSpawner;
    public TextMeshProUGUI scoreText;

    void Start()
    {
        if (restartButton != null)
        {
            // Add a listener to the button to call the RestartGame method when clicked
            restartButton.onClick.AddListener(RestartGame);
        }
        else
        {
            Debug.LogError("Restart Button or EnemySpawner is not assigned in the Inspector.");
        }
    }

    // Method to restart the game
    void RestartGame()
    {
        // Hide the GameStartCanvas
        if (GameOverCanvas != null)
        {
            GameOverCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("GameOverCanvas reference is not assigned in the Inspector!");
        }

        if (playerSpawner != null) {
            playerSpawner.SpawnPlayer();
        }
        if (scoreText != null) {
            scoreText.gameObject.SetActive(true);
        }
        
        
    }
}