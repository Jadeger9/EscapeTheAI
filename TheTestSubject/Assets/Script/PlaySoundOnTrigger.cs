using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    private bool _hasTriggered = false;

    [SerializeField] private string _soundName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_hasTriggered == false)
            {
                AudioManager.Instance.PlaySound(_soundName);
                _hasTriggered = true;
            }
        }        
    }
}
