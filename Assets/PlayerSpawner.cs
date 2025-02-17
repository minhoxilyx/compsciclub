using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab
    public CameraFollow camera;

    public EnemySpawner enemySpawner;
    public ScoreTracker scoreTracker;
    public GameOverManager gameOverManager;

    // Method to spawn the player at the center of the screen
    public void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned in the Inspector!");
            return;
        }

        // Calculate the center of the screen in world coordinates
        Vector3 spawnPosition = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        spawnPosition.z = 0; // Ensure the Z position is 0 for 2D games

        // Instantiate the player prefab at the calculated position
        // Example: Instantiating the player and assigning references
        // Example: Instantiating the player and assigning references
        GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.SetGameManager(gameOverManager);
            playerMovement.SetEnemySpawner(enemySpawner);
        }
        player.SetActive(true);
        
        if (camera != null) {
            Debug.Log("Main camera set");
            camera.SetPlayer(player.transform);
        }
        if (scoreTracker != null) {
            scoreTracker.SetPlayer(player);
        }

        

        if (enemySpawner != null) {
            // Enable the enemy spawner and spawn enemies
            enemySpawner.gameObject.SetActive(true);
            enemySpawner.SetPlayer(player.transform);
        }
        if (gameOverManager != null) {
            gameOverManager.SetPlayer(player);
        }
        Debug.Log("Player spawned at: " + spawnPosition);
    }

    // Example usage: Call SpawnPlayer from another script or event
}