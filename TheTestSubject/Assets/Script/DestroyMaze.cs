using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMaze : MonoBehaviour
{
    private bool _hasTriggered = false;

    private void OnEnable()
    {
        EventBus<OnMazeEnd>.Subscribe(MazeDestroy);
    }

    private void OnDisable()
    {
        EventBus<OnMazeEnd>.UnSubscribe(MazeDestroy);
    }

    private void MazeDestroy(OnMazeEnd onMazeEnd)
    {
        if (_hasTriggered == false)
        {
            AudioManager.Instance.PlaySound("SlidingDoor");
            _hasTriggered = true;
            Destroy(gameObject);
        }
    }
}
