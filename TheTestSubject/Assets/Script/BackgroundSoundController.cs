using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundController : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the OnQuestComplete event to handle finishing quests
        EventBus<WallCollapseEvent>.Subscribe(StartEndingSounds);
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnQuestComplete event when disabled to avoid memory leaks
        EventBus<WallCollapseEvent>.UnSubscribe(StartEndingSounds);
    }

    private void StartEndingSounds(WallCollapseEvent wallCollapseEvent)
    {
        Invoke("PlayCrashSound", 1.25f);
        Invoke("PlayNewBackgroundMusic", 1.25f);
        Invoke("PlayRevealSound", 2.25f);
    }

    private void PlayCrashSound()
    {
        AudioManager.Instance.PlayLongSound("Crash");
    }

    private void PlayRevealSound()
    {
        AudioManager.Instance.PlayLongSound("RevealSound");
    }

    private void PlayNewBackgroundMusic()
    {
        AudioManager.Instance.ChangeBackgrondMusic("TenseMusic");
    }
}
