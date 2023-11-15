using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CueController : MonoBehaviour
{
    public float intensity = 0.5f; // Adjust the haptic feedback intensity
    public float pulseDuration = 0.15f; // Adjust the haptic feedback duration
    public float pulseInterval = 0.5f; // Adjust the interval between pulses

    private XRBaseController controller;
    private Collider cueHandleCollider;
    private bool isInRange;
    private bool isPulsing;

    private bool isLeft = false;
    public HapticFeedbackManager manager; // Reference to the manager script


    private void Start()
    {
        controller = GetComponent<XRBaseController>();

        if (controller == null)
        {
            Debug.LogError("XRBaseController component not found on this GameObject.");
        }

        if (controller.name == "Left Controller")
        {
            isLeft = true;
        }

        // Find the cue handle collider by tag (adjust the tag as needed)
        cueHandleCollider = GameObject.FindGameObjectWithTag("Cue Handle").GetComponent<Collider>();

        // Ensure the cue handle collider is set to be a trigger in the Unity Inspector.
        if (cueHandleCollider != null)
        {
            cueHandleCollider.isTrigger = true;
        }

        StartCoroutine(PulseHapticFeedback());
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other == cueHandleCollider)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == cueHandleCollider)
        {
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange && controller.selectInteractionState.active)
        {
            if (isLeft)
            {
                manager.setIsGrabbedLeft();
            }
            else
            {
                manager.setIsGrabbedRight();
            }
        }
        else
        {
            if (isLeft)
            {
                manager.unsetIsGrabbedLeft();
            }
            else
            {
                manager.unsetIsGrabbedRight();
            }
        }

        if (isInRange && !isPulsing && !controller.selectInteractionState.active && !manager.isGrabbedByAny())
        {
            isPulsing = true;
            controller.SendHapticImpulse(intensity, pulseDuration);
            StartCoroutine(StopPulsing());
        }
    }

    private IEnumerator StopPulsing()
    {
        yield return new WaitForSeconds(pulseInterval);
        isPulsing = false;
    }

    private IEnumerator PulseHapticFeedback()
    {
        while (true)
        {
            yield return new WaitForSeconds(pulseInterval);
            if (isInRange && !isPulsing && !controller.selectInteractionState.active && !manager.isGrabbedByAny())
            {
                isPulsing = true;
                controller.SendHapticImpulse(intensity, pulseDuration);
                StartCoroutine(StopPulsing());
            }
        }
    }
}
