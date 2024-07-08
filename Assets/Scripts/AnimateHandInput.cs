using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandInput : MonoBehaviour
{
    public InputActionProperty pinchAnimation;
    public InputActionProperty grabAnimation;

    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float grabValue = grabAnimation.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", grabValue);
    }
}
