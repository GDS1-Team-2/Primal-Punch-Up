using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapIcon : MonoBehaviour
{
    public Transform player; // Player's Transform
    public float heightAbovePlayer = 100f; // Height of the icon above the player

    void Update()
    {
        // Ensure the player variable is not null
        if (player != null)
        {
            // Update the icon's position so that it follows only the player's x and z coordinates, adjusting the y coordinate according to the specified height
            transform.position = new Vector3(player.position.x, player.position.y + heightAbovePlayer, player.position.z);

            // Set the icon's rotation to face upwards, while adjusting based on the player's y-axis rotation
            transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0);
        }
    }
}
