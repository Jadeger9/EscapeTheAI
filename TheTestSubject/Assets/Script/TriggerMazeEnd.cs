using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMazeEnd : MonoBehaviour
{
    private bool _hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_hasTriggered == false)
            {
                EventBus<OnMazeEnd>.Publish(new OnMazeEnd());
                _hasTriggered = true;
            }
        }            
    }
}
