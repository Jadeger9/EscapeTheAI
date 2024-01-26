using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlottwistSounds : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus<OnPlottwistStart>.Subscribe(PlottwistStart);
        EventBus<OnPlottwistEnd>.Subscribe(PlottwistEnd);
    }

    private void OnDisable()
    {
        EventBus<OnPlottwistStart>.UnSubscribe(PlottwistStart);
        EventBus<OnPlottwistEnd>.UnSubscribe(PlottwistEnd);
    }

    private void PlottwistStart(OnPlottwistStart onPlotTwist)
    {
        AudioManager.Instance.PlaySound("GoBack");
        Invoke("CollapseWalls", 4.5f);
    }

    private void CollapseWalls()
    {
        EventBus<WallCollapseEvent>.Publish(new WallCollapseEvent());

        Invoke("PlayCrashSound", 1.25f);
        Invoke("PlayNewBackgroundMusic", 1.25f);
    }

    private void PlottwistEnd(OnPlottwistEnd onPlotEnd)
    {
        AudioManager.Instance.ChangeBackgrondMusic("BackgroundMusic");
        EventBus<OnFlickerEnd>.Publish(new OnFlickerEnd());
        Invoke("OnRevealSound", 0.5f);
        Invoke("StartStatSequence", 5);
    }

    private void StartStatSequence()
    {
        AudioManager.Instance.PlaySound("Stats");
    }

    private void OnRevealSound()
    {
        AudioManager.Instance.PlaySound("RevealSound");
    }

    private void PlayCrashSound()
    {
        AudioManager.Instance.PlayLongSound("Crash");
    }

    private void PlayNewBackgroundMusic()
    {
        EventBus<OnFlickerStart>.Publish(new OnFlickerStart());
        AudioManager.Instance.ChangeBackgrondMusic("Plottwist");
    }
}


