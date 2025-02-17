using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform
    public float moveSpeed = 2f; // Speed at which the enemy follows the player
    public float followDistance = 1f; // Minimum distance to stop following

    public EnemySpawner spawner; // Reference to the EnemySpawner
    public int health = 1; // Enemy health

    private static List<Vector3> enemyPositions = new List<Vector3>(); // Track all enemy positions
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Place the enemy at a random position, ensuring it doesn't overlap with other enemies

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.gravityScale = 0; // Disable gravity
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous; // Better collision detection
            rb.interpolation = RigidbodyInterpolation2D.Interpolate; // Smoother movement
        }
    }

    

    void Update()
    {
        if (player != null) // Check if the player reference is set
        {
            // Calculate the distance to the player
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Only move if the enemy is farther than the follow distance
            if (distanceToPlayer > followDistance)
            {
                // Calculate the direction to the player
                Vector2 direction = (player.position - transform.position).normalized;

                // Move the enemy toward the player using Rigidbody2D velocity
                rb.velocity = direction * moveSpeed;
            }
            else
            {
                // Stop moving if the enemy is close enough to the player
                rb.velocity = Vector2.zero;
            }
        }
    }

    // Called when the enemy takes damage
    public void TakeDamage(int damage)
    {
        health -= damage;

        // Check if the enemy is dead
        if (health <= 0)
        {
            Die();
        }
    }

    // Called when the enemy dies
    public void Die()
    {
        // Notify the spawner that this enemy was killed
        if (spawner != null)
        {
            spawner.OnEnemyKilled(gameObject);
        }

        // Destroy the enemy
        Destroy(gameObject);
    }
}