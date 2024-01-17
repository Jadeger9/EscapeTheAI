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

// Event indicating the start of a quest with a provided value
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
    public readonly Color ballColor;
    public OnBallColorChange(int newNumber, Color newcolor)
    {
        ballNumber = newNumber;
        ballColor = newcolor;
    }
}