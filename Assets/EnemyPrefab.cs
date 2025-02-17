using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    private Transform player; // Reference to the player's Transform
    public int numberOfEnemies = 10; // Number of enemies to spawn
    public float spawnRadius = 5f; // Radius within which enemies can spawn
    public float minDistanceBetweenEnemies = 2f; // Minimum distance between enemies


    private List<GameObject> activeEnemies = new List<GameObject>(); // Track active enemies
    private bool GameOver = false;
    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("enemyPrefab is not assigned in the Inspector!");
            return;
        }

        SpawnEnemies();
    }

    void Update()
    {
        // Check if all enemies are dead
        if (activeEnemies.Count == 0 && !GameOver)
        {
            // Respawn enemies with +1 count
            numberOfEnemies++;
            SpawnEnemies();
        }
        if (GameOver) {
            numberOfEnemies = 2;
        }
    }

     public void SetPlayer(Transform playerTransform)
    {
        Debug.Log("player set");
        player = playerTransform;
        GameOver = false;
    }

    void SpawnEnemies()
    {
        Debug.Log("Spawning " + numberOfEnemies + " enemies.");

        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Instantiate the enemy prefab
            GameObject enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
            Debug.Log("Enemy spawned: " + enemy.name);

            // Set the player reference for the enemy
            EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.player = player;
                enemyMovement.spawner = this; // Assign the spawner reference
            }

            // Add the enemy to the active enemies list
            activeEnemies.Add(enemy);
            

            // Place the enemy at a random position
            PlaceEnemyRandomly(enemy);
        }
    }

    void PlaceEnemyRandomly(GameObject enemy)
    {
        Vector3 newPosition;
        bool positionValid;
        int attempts = 0;
        const int maxAttempts = 100; // Maximum attempts to find a valid position

        do
        {
            // Generate a random angle in radians
            float randomAngle = Random.Range(0f, 2f * Mathf.PI);

            // Calculate the position using the angle and spawn radius, relative to the player's current position
            float x = player.position.x + spawnRadius * Mathf.Cos(randomAngle);
            float y = player.position.y + spawnRadius * Mathf.Sin(randomAngle);
            newPosition = new Vector3(x, y, 0f);

            // Check if the new position is too close to other enemies
            positionValid = true;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, minDistanceBetweenEnemies);
            foreach (Collider2D collider in colliders)
            {
                if (collider.gameObject != enemy && collider.CompareTag("Enemy"))
                {
                    positionValid = false;
                    break;
                }
            }

            attempts++;
        } while (!positionValid && attempts < maxAttempts); // Repeat until a valid position is found or max attempts reached

        if (positionValid)
        {
            // Set the enemy's position
            enemy.transform.position = newPosition;
            Debug.Log("Enemy placed at: " + newPosition);
        }
        else
        {
            Debug.LogWarning("Failed to find a valid position for enemy spawning.");
            Destroy(enemy); // Destroy the enemy if no valid position is found
            activeEnemies.Remove(enemy); // Remove from the active enemies list
        }
    }

    // Call this method when an enemy is killed
    public void OnEnemyKilled(GameObject enemy)
    {
        Debug.Log("OnEnemyKilled called for: " + enemy.name);
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy); // Remove the enemy from the active list
            Debug.Log("Enemy killed. Remaining enemies: " + activeEnemies.Count);
        }
        else
        {
            Debug.LogWarning("Enemy not found in activeEnemies list!");
        }
    }

     // Method to kill all active enemies
    public void KillAllEnemies()
    {
        Debug.Log(activeEnemies.Count);
        GameOver = true;
        // Create a copy of the list to avoid modifying it while iterating
        List<GameObject> enemiesToKill = new List<GameObject>(activeEnemies);
        

        // Kill all existing enemies
        foreach (GameObject enemy in enemiesToKill)
        {
            if (enemy != null) // Check if the enemy is not already destroyed
            {
                Destroy(enemy);
            }
        }

        // Clear the active enemies list
        activeEnemies.Clear();
        
    }
    
}