using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    [SerializeField] private string _newSubtitle = "";
    [SerializeField] private float _subtitleDelay = 0.1f;
    private bool _hasTriggered = false;

    [SerializeField] private string _soundName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_hasTriggered == false)
            {
                ResetSubtitle();
                Invoke("ChangeSubtitle", _subtitleDelay);
                AudioManager.Instance.PlayLongSound(_soundName);
                if (_newSubtitle != null) 
                _hasTriggered = true;
            }
        }        
    }

    private void ResetSubtitle()
    {
        EventBus<OnSubtitleChange>.Publish(new OnSubtitleChange(""));
    }

    private void ChangeSubtitle()
    {
        EventBus<OnSubtitleChange>.Publish(new OnSubtitleChange(_newSubtitle));
    }

    public void NextAssignmentSubtitle()
    {
        EventBus<OnSubtitleChange>.Publish(new OnSubtitleChange("Go the the next assignment."));
    }
}
