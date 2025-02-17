using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject ballPrefab; // Reference to the ball prefab
    public float shootForce = 10f; // Force applied to the ball

    void Update()
    {
        // Check for player input (left mouse button)
        if (Input.GetMouseButtonDown(0)) // 0 is the left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Get the mouse position in world coordinates
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the player to the mouse position
        Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;

        // Calculate the fire point position (slightly offset from the player)
        Vector2 firePointPosition = (Vector2)transform.position + shootDirection * 0.5f; // Adjust offset as needed

        // Instantiate the ball at the calculated fire point
        GameObject ball = Instantiate(ballPrefab, firePointPosition, Quaternion.identity);

        // Apply force to the ball in the shoot direction
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset velocity to prevent weird behavior
            rb.AddForce(shootDirection * shootForce, ForceMode2D.Impulse);
        }
    }
}