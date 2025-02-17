using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // Speed of the background scroll
    public Transform player; // Reference to the player's Transform
    public float boundaryX = 10f; // X position where the boundary is reached

    private Renderer backgroundRenderer;
    private Vector2 initialOffset;

    void Start()
    {
        // Get the Renderer component of the background
        backgroundRenderer = GetComponent<Renderer>();

        // Store the initial texture offset
        initialOffset = backgroundRenderer.material.mainTextureOffset;
    }

    void Update()
    {
        if (player != null)
        {
            // Check if the player has reached the boundary
            if (player.position.x >= boundaryX)
            {
                // Reset the player's position
                player.position = new Vector3(-boundaryX, player.position.y, player.position.z);

                // Reset the background texture offset
                backgroundRenderer.material.mainTextureOffset = initialOffset;
            }
            else
            {
                // Calculate the offset based on time and scroll speed
                float offset = Time.time * scrollSpeed;

                // Set the texture offset to create the scrolling effect
                backgroundRenderer.material.mainTextureOffset = new Vector2(offset, 0);
            }
        }
    }
}