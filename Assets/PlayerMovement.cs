using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the square
    private GameOverManager gameManager; // Reference to the GameManager
    private EnemySpawner enemySpawner; // Reference to the EnemySpawner

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Get input from WASD keys
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) // W key for up
        {
            moveY = 1f;
        }
        if (Input.GetKey(KeyCode.S)) // S key for down
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A)) // A key for left
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D)) // D key for right
        {
            moveX = 1f;
        }

        // Normalize diagonal movement to prevent faster movement
        Vector2 movement = new Vector2(moveX, moveY).normalized * moveSpeed * Time.deltaTime;

        // Move the square
        transform.Translate(movement);
    }
    public void SetGameManager(GameOverManager gm)
    {
        gameManager = gm;
    }

    public void SetEnemySpawner(EnemySpawner spawner)
    {
        enemySpawner = spawner;
    }

    // Handle collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collided with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (gameManager != null)
            {
                gameManager.SetEnemy(enemySpawner);
                gameManager.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameManager reference is not assigned!");
            }

            if (enemySpawner != null)
            {
                enemySpawner.KillAllEnemies();
            }

            // Hide the player object
            Destroy(gameObject);
        }
    }
}