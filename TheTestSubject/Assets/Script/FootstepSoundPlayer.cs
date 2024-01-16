using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSoundPlayer : MonoBehaviour
{
    public float stepThreshold = 1.0f; // Set a distance threshold for footstep sounds
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position; // Record initial position
    }

    void Update()
    {
        // Calculate the distance moved since the last frame
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        // Check if the distance moved exceeds the threshold
        if (distanceMoved >= stepThreshold)
        {
            AudioManager.Instance.PlaySound("Footstep"); // Play footstep sound
            lastPosition = transform.position; // Update last position
        }
    }
}