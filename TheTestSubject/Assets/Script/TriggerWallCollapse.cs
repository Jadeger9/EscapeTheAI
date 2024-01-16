using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWallCollapse : MonoBehaviour
{
    public void TriggerCollapse()
    {
        EventBus<WallCollapseEvent>.Publish(new WallCollapseEvent());
    }
}
