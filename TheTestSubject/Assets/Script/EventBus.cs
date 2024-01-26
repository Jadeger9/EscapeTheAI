using System;
using UnityEngine;
using System.Collections.Generic;

public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;

    public static void Subscribe(Action<T> handler)
    {
        OnEvent += handler;
    }

    public static void UnSubscribe(Action<T> handler)
    {
        OnEvent -= handler;
    }

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

public class OnPlottwistStart : Event
{
    public OnPlottwistStart()
    {
    }
}

public class OnPlottwistEnd : Event
{
    public OnPlottwistEnd()
    {
    }
}

public class OnFlickerStart : Event
{
    public OnFlickerStart()
    {
    }
}

public class OnFlickerEnd : Event
{
    public OnFlickerEnd()
    {
    }
}

public class WallCollapseEvent : Event
{
    public WallCollapseEvent()
    {
    }
}

public class OnBallClick : Event
{
    public readonly int value;
    public OnBallClick(int newValue)
    {
        value = newValue;
    }
}

public class OnBallColorChange : Event
{
    public readonly int ballNumber;
    public readonly Material ballColor;
    public OnBallColorChange(int newNumber, Material newcolor)
    {
        ballNumber = newNumber;
        ballColor = newcolor;
    }
}

// Event indicating the start of a quest with a provided value
public class OnMazeEnd : Event
{
    public OnMazeEnd()
    {
    }
}