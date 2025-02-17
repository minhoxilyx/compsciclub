using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, -10); // Camera offset from the player
    private Transform player; // Reference to the player's Transform

    // Method to set the player reference dynamically
    public void SetPlayer(Transform playerTransform)
    {
        player = playerTransform;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Follow the player's position with an offset
            transform.position = player.position + offset;
        }
    }
}