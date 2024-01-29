using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class VRHeadBlocking : MonoBehaviour
{
    public LayerMask wallLayer; // Assign the layer of the walls in the Inspector
    public Transform player; // Reference to the player's XR rig

    void Update()
    {
        if (IsPlayerApproachingWall())
        {
            // Restrict the player's movement
            //Debug.Log("Player is approaching the wall!");
        }
    }

    bool IsPlayerApproachingWall()
    {
        RaycastHit hit;

        // Create a ray from the player's position towards the movement direction
        Ray ray = new Ray(player.position, player.forward); // Example using the player's forward direction

        // Check for collision with the wallLayer
        if (Physics.Raycast(ray, out hit, 1.5f, wallLayer))
        {
            // A wall was hit, meaning the player is approaching the wall
            return true;
        }

        // Otherwise, the player is not approaching the wall
        return false;
    }
}
