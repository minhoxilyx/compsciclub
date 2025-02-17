using UnityEngine;

public class BallProjectile : MonoBehaviour
{
    public int damage = 1; // Damage dealt to enemies

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the ball collided with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the EnemyMovement component from the collided object
            EnemyMovement enemy = collision.gameObject.GetComponent<EnemyMovement>();

            // Notify the enemy that it has been hit
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // Destroy the ball
            Destroy(gameObject);
        }

        


    }
}