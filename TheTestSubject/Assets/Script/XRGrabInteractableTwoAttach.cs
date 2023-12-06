using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    [SerializeField] private Transform _leftHandTransform;
    [SerializeField] private Transform _rightHandTransform;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("RightHand"))
        {
            attachTransform = _rightHandTransform;
        }

        if (args.interactorObject.transform.CompareTag("LeftHand"))
        {
            attachTransform = _leftHandTransform;
        }

        base.OnSelectEntered(args);
    }
}
