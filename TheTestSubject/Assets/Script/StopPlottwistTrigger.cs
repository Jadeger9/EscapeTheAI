using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPlottwistTrigger : MonoBehaviour
{
    private bool _active = false;
    private bool _hasTriggered = false;

    private void OnEnable()
    {
        EventBus<OnPlottwistStart>.Subscribe(ActivateTrigger);
    }

    private void OnDisable()
    {
        EventBus<OnPlottwistStart>.UnSubscribe(ActivateTrigger);
    }

    private void ActivateTrigger(OnPlottwistStart onPlottwistStart)
    {
        _active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_hasTriggered == false && _active == true)
            {
                EventBus<OnPlottwistEnd>.Publish(new OnPlottwistEnd());
                _hasTriggered = true;
            }
        }
    }
}
