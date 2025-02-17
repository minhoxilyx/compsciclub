using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    public EnemySpawner enemySpawner; // Reference to the EnemySpawner
    public Button startButton; // Reference to the start button
    public GameObject gameStartCanvas; // Reference to the GameStartCanvas
    public GameObject inGameScoreCanvas; // Reference to the in-game score canvas

    public PlayerSpawner playerSpawner;
    private GameObject player; // Reference to the spawned player
    public GameObject gameOverCanvas;
    


    void Start()
    {
        // Ensure the button is assigned
        if (startButton != null)
        {
            // Add a listener to the button to call the StartGame method when clicked
            startButton.onClick.AddListener(StartGame);
        }
        else
        {
            Debug.LogError("Start Button is not assigned in the Inspector!");
        }

        // Disable the enemy spawner at the start
        if (enemySpawner != null)
        {
            enemySpawner.gameObject.SetActive(false); // Disable the spawner
        }
        else
        {
            Debug.LogError("EnemySpawner reference is not assigned in the Inspector!");
        }

        

        // Ensure the in-game score canvas is disabled at the start
        if (inGameScoreCanvas != null)
        {
            inGameScoreCanvas.SetActive(false);
        }
        else
        {
            Debug.LogError("inGameScoreCanvas reference is not assigned in the Inspector!");
        }

        if (gameOverCanvas != null) {
            gameOverCanvas.gameObject.SetActive(false);
        }
    }

    // Method to start the game
    void StartGame()
    {
        // Spawn the player
        if (playerSpawner != null) {
            playerSpawner.gameObject.SetActive(true);
            playerSpawner.SpawnPlayer();
        }

       


        // Hide the GameStartCanvas
        if (gameStartCanvas != null)
        {
            gameStartCanvas.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogError("GameStartCanvas reference is not assigned in the Inspector!");
        }

        // Display the inGameScoreCanvas
        if (inGameScoreCanvas != null)
        {
            inGameScoreCanvas.SetActive(true);
        }
        else
        {
            Debug.LogError("inGameScoreCanvas reference is not assigned in the Inspector!");
        }

        // Optionally, disable the start button after the game starts
        if (startButton != null)
        {
            startButton.gameObject.SetActive(false);
        }
    }


    
}