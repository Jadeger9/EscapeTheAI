using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollapsWall : MonoBehaviour
{
    [SerializeField] private Rigidbody _wall; // Reference to the wall Rigidbody components
    [SerializeField] private float _fallForce = 10f; // The force applied to make the walls fall
    [SerializeField] private Vector3 _fallDirection;

    private void OnEnable()
    {
        // Subscribe to the OnQuestComplete event to handle finishing quests
        EventBus<WallCollapseEvent>.Subscribe(TriggerCollapse);
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnQuestComplete event when disabled to avoid memory leaks
        EventBus<WallCollapseEvent>.UnSubscribe(TriggerCollapse);
    }

    private void Start()
    {
        TryGetComponent<Rigidbody>(out _wall);
    }

    private void TriggerCollapse(WallCollapseEvent wallCollapseEvent)
    {
        _wall.isKinematic = false; // Disable kinematic behavior
        _wall.AddForce(_fallDirection * _fallForce, ForceMode.Impulse); // Apply backward force to make it fall
    }
}
