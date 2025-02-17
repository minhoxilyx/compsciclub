using UnityEngine;
using TMPro; // Add this namespace for TextMeshPro

public class ScoreTracker : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro UI element
    public EnemySpawner enemySpawner; // Reference to the EnemySpawner
    private GameObject player; // Reference to the player GameObject

     public void SetPlayer(GameObject thePlayer)
    {
        Debug.Log("Player score set");
        player = thePlayer;
    }
    void Update()
    {
        // Check if the player is alive
        if (player != null)
        {
            // Update the score text in real-time
            if (enemySpawner != null && scoreText != null)
            {
                int score = enemySpawner.numberOfEnemies - 3;
                scoreText.text = "Score: " + score;
            }
        }
        else
        {
            // Player is dead, hide the canvas
            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(false); // Hide the score text
            }
        }
    }
}